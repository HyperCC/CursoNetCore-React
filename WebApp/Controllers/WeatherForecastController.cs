using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistencia;
using Dominio;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly CursosOnlineContext context;

        /// <summary>
        /// the constructor
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(CursosOnlineContext _context)
        {
            this.context = _context;
        }

        [HttpGet]
        public IEnumerable<Curso> Get()
        {
            return this.context.Curso.ToList();
        }

    }
}
