using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class MiControllerBase : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator MediadorHerencia => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
