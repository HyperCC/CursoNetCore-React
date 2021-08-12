using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Newtonsoft.Json;
using System.Net;

namespace WebApp.Middleware
{
    public class ManejadorErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorErrorMiddleware> _logger;

        public ManejadorErrorMiddleware(RequestDelegate next, ILogger<ManejadorErrorMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // verificar si los datos estan realmente buenos o hay errores en la validacion
            try
            {
                await this._next(context);
            }
            catch (Exception ex)
            {
                await ManejadorExcepcionAsincrono(context, ex, this._logger);
            }
        }

        // evalua el tipo de excepcion ue pueden surgir y al respuesta
        private async Task ManejadorExcepcionAsincrono(HttpContext context, Exception ex, ILogger<ManejadorErrorMiddleware> logger)
        {
            object errores = null;

            // verificar el tipo de excepcion
            switch (ex)
            {
                // si se lanza ManejadorExcepcion (excepcion personalizada), error de validacion 
                case ManejadorExcepcion me:
                    logger.LogError(ex, "Manejador Error en clase ManejadorExcepcion");
                    errores = me.Errores;
                    context.Response.StatusCode = (int)me.Codigo;
                    break;

                // error generico o desconocido
                case Exception e:
                    logger.LogError(ex, "Error del servior, clase Exception");
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "Error Exception - mensaje generico se Exception" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            // transformacion de los errores encontrados a datos en json, serializacion
            context.Response.ContentType = "application/json";
            if (errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errores });
                await context.Response.WriteAsync(resultados);
            }
        }

    }
}