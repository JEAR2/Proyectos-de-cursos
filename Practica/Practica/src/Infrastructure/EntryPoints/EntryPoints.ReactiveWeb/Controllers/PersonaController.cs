/*using credinet.comun.negocio;
using Domain.Model.Entities;
using Domain.UseCase.Interfaces;
using EntryPoints.ReactiveWeb.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoints.ReactiveWeb.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/[controller]/[action]")]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaUseCases _personaUseCases;

        public PersonaController(IPersonaUseCases personaUseCases)
        {
            _personaUseCases = personaUseCases;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get()
        {
            List<PersonaEntity> personas = _personaUseCases.FindAll();
            return Ok(personas);
            // return await ProcesarResultado(Exito(Build(Request.Path.Value, 0, "", "co", respuestaNegocio))); ;
        }
    }
}*/