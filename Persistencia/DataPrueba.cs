using Dominio;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task InsertarData(CursosOnlineContext context, UserManager<Usuario> usuarioManager)
        {
            if (usuarioManager.Users.Any() == false)
            {
                var usuario = new Usuario
                {
                    NombreCompleto = "Domain Belchello",
                    UserName = "domainB",
                    Email = "belchello@gmail.com"
                };

                await usuarioManager.CreateAsync(usuario, "P@ssw0rd");
            }
        }
    }
}
