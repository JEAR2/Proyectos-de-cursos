using Domain.Model.Entities;
using Domain.Model.Interfaces;
using Domain.UseCase.Empleados;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EntryPoints.ReactiveWeb.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class EmpleadoController : AppControllerBase<EmpleadoController>
    {
        private readonly IEmpleadoUseCase _empleadoUseCase;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="empleadoUseCase"></param>
        /// <param name="eventsService"></param>
        public EmpleadoController(IEmpleadoUseCase empleadoUseCase,
            IManageEventsUseCase eventsService) : base(eventsService)
        {
            _empleadoUseCase = empleadoUseCase;
        }

        /// <summary>
        /// Método para registrar un empleado
        /// </summary>
        /// <param name="empleadoRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CrearEmpleados([FromBody] EmpleadoRequest empleadoRequest) =>
               await HandleRequest(
                   async () =>
                   {
                       Empleado empleado = empleadoRequest.AsEntity();
                       return await _empleadoUseCase.InsertarEmpleadoAsync(empleado);
                   }, "");

        /// <summary>
        /// Método para obtener todos los empleados
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerEmpleados() =>
            await HandleRequest(
                    async () =>
                    {
                        return await _empleadoUseCase.ObtenerEmpleadosAsync();
                    }, "");

        /// <summary>
        /// Método para eliminar un empleado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("id")]
        public async Task<IActionResult> EliminarEmpleados(string id) =>
            await HandleRequest(
                    async () =>
                    {
                        await _empleadoUseCase.EliminarEmpleado(id);
                        return NoContent();
                    }, "");

        /// <summary>
        /// Método para actualizar un empleado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empleadoRequest"></param>
        /// <returns></returns>
        [HttpPut("id")]
        public async Task<IActionResult> ActualizarEmpleado(string id, EmpleadoRequest empleadoRequest)
        {
            return await HandleRequest(
                async () =>
                {
                    Empleado empleado = empleadoRequest.AsEntity();
                    return await _empleadoUseCase.ActualizarEmpleado(id, empleado);
                }, "");
        }
    }
}