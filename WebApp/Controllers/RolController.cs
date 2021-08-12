using Aplicacion.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    // acciones disponibles para los Roles de los usuarios
    public class RolController : MiControllerBase
    {
        // operaciones directamente sobre los roles
        [HttpPost("crear")]
        public async Task<ActionResult<Unit>> Crear(RolNuevo.Ejecuta parametros)
        {
            return await MediadorHerencia.Send(parametros);
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<Unit>> Eliminar(RolEliminar.Ejecuta parametros)
        {
            return await MediadorHerencia.Send(parametros);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<IdentityRole>>> lista()
        {
            return await MediadorHerencia.Send(new RolLista.Ejecuta());
        }

        // operaciones para enrutar acciones de los usuario con los roles 
        [HttpPost("agregarRoleUsuario")]
        public async Task<ActionResult<Unit>> AgregarRoleUsuario(UsuarioRolAgregar.Ejecuta parametros)
        {
            return await MediadorHerencia.Send(parametros);
        }

        [HttpDelete("elimiarRoleUsuario")]
        public async Task<ActionResult<Unit>> ElimiarRolUsuario(UsuarioRolEliminar.Ejecuta parametros)
        {
            return await MediadorHerencia.Send(parametros);
        }

        [HttpGet("{username}")] // obtener todos los roles asociados a un usuario
        public async Task<ActionResult<List<string>>> ObtenerRolesPorUsuario(string username)
        {
            return await MediadorHerencia.Send(new ObtenerRolesPorUsuario.Ejecuta { Username = username });
        }
    }
}
