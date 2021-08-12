using Aplicacion.Contratos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Seguridad.TokenSeguridad
{
    // obtener datos del usuario en sesion actual
    public class UsuarioSesion : IUsuarioSesion
    {
        // para poder tener acceso al usuario actual en sesion
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuarioSesion(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        // busqueda del usuario en sesion
        public string ObtenerUsuarioSesion()
        {
            // se preunta si User es nulo, si Claims es nulo y asi hasta asignar un valor
            var userName = this._httpContextAccessor.HttpContext.User?.Claims?.
                FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            // se devuelve el usuario en sesion
            return userName;
        }
    }
}
