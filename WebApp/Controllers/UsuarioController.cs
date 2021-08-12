using Aplicacion.Seguridad;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    // esta [AllowAnonymous] permite no pedir authorization para entrar al login,
    // ya ue todos los demas metodos del controlador si reuieren authorization.
    [AllowAnonymous]
    public class UsuarioController : MiControllerBase
    {
        //el endpoint para ejecutar este codigo es http://localhost:5000/api/Usuario/login
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros)
        {
            return await MediadorHerencia.Send(parametros);
        }

        // la url es http://localhost:5000/api/Usuario/registrar
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioData>> Registrar(Registrar.Ejecuta parametros)
        {
            return await MediadorHerencia.Send(parametros);
        }

        // ruta para obtener el usuario actualmente sesion http://localhost:5000/api/Usuario
        [HttpGet]
        public async Task<ActionResult<UsuarioData>> DevolverUsuario()
        {
            return await MediadorHerencia.Send(new UsuarioActual.Ejecutar());
        }

        // actualizar los datos de un usuario ya registrado 
        [HttpPut]
        public async Task<ActionResult<UsuarioData>> Actualizar(UsuarioActualizar.Ejecuta parametros)
        {
            return await MediadorHerencia.Send(parametros);
        }
    }
}
