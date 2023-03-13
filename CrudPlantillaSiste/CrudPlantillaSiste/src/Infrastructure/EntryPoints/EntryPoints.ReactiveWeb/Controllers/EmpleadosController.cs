using Domain.Model.Entities;
using Domain.Model.Interfaces;
using Domain.UseCase.Empleados;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static credinet.comun.negocio.RespuestaNegocio<credinet.exception.middleware.models.ResponseEntity>;
using static credinet.exception.middleware.models.ResponseEntity;

namespace EntryPoints.ReactiveWeb.Controllers
{
    /// <summary>
    /// EntityController
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class EmpleadosController : AppControllerBase<EmpleadosController>
    {
        private readonly IEmpleadoUseCase _empleadoUseCase;
        private readonly ILogger<EmpleadosController> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="empleadoUseCase"></param>
        /// <param name="eventsService"></param>
        public EmpleadosController(IEmpleadoUseCase empleadoUseCase,
            IManageEventsUseCase eventsService) : base(eventsService)
        {
            _empleadoUseCase = empleadoUseCase;
        }

        /*
            /// <summary>
            /// Obtiene todos los objetos de tipo <see cref="Entity"/>
            /// </summary>
            /// <returns></returns>
            /// <response code="200">Retorna la lista</response>
            /// <response code="400">Si existe algun problema al consultar</response>
            /// <response code="406">Si no se envia el ambiente correcto</response>
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]
            [ProducesResponseType(406)]
            [HttpGet()]
            [ProducesResponseType(200, Type = typeof(IEnumerable<Entity>))]
            public async Task<IActionResult> Get()
            {
                _logger.LogInformation("Entro al controlador en: {time}", DateTimeOffset.Now);
                var respuestaNegocio = testNegocio.GetAllUsers();
                return await ProcesarResultado(Exito(Build(Request.Path.Value, 0, "", "co", respuestaNegocio)));
            }

            /// <summary>
            /// Create
            /// </summary>
            /// <param name="entity"></param>
            /// <returns></returns>
            [HttpPost()]
            [ProducesResponseType(200, Type = typeof(IEnumerable<Entity>))]
            public async Task<IActionResult> Create([FromBody] Entity entity)
            {
                var respuestaNegocio = testNegocio.GetAllUsers(entity);
                return await ProcesarResultado(Exito(Build(Request.Path.Value, 0, "", "co", respuestaNegocio)));
            }*/

        [HttpPost]
        public async Task<IActionResult> CrearEmpleados(EmpleadoRequest empleadoRequest) =>
            await HandleRequest(
                async () =>
                {
                    Empleado empleado = empleadoRequest.AsEntity();
                    return await _empleadoUseCase.InsertarEmpleadoAsync(empleado);
                }, ""
                );

        [HttpGet]
        public async Task<IActionResult> ObtenerEmpleados() =>
            await HandleRequest(
                async () =>
                {
                    return await _empleadoUseCase.ObtenerEmpleadosAsync();
                }, ""
                );
    }
}