using Domain.Model.Entities;
using Domain.Model.Interfaces;
using Domain.UseCase.Clientes;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entities;
using Helpers.ObjectsUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace EntryPoints.ReactiveWeb.Controllers
{
    /// <summary>
    /// EntityController
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/[controller]/[action]")]
    public class ClienteController : AppControllerBase<ClienteController>
    {
        private readonly IClienteUseCase _clienteUseCase;
        private readonly ILogger<ClienteController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClienteController"/> class.
        /// </summary>
        /// <param name="clienteUseCase"></param>
        /// <param name="eventService"></param>
        public ClienteController(IClienteUseCase clienteUseCase,
            IManageEventsUseCase eventService, IOptions<ConfiguradorAppSettings> appSettings) :
            base(eventService, appSettings)
        {
            _clienteUseCase = clienteUseCase;
        }

        /// <summary>
        /// Método para registrar un cliente
        /// </summary>
        /// <param name="clienteRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CrearClientes([FromBody] ClienteRequest clienteRequest) =>
              await HandleRequest(
                  async () =>
                  {
                      Cliente cliente = clienteRequest.AsEntity();
                      return await _clienteUseCase.CrearCliente(cliente);
                  }, "");

        /// <summary>
        /// Método para asignar un crédito a un cliente
        /// </summary>
        /// <param name="crearCreditoRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AsignarCredito([FromBody] CrearCreditoRequest crearCreditoRequest) =>
              await HandleRequest(
                  async () =>
                  {
                      return await _clienteUseCase.AsignarCredito(crearCreditoRequest.IdCliente, crearCreditoRequest.Monto, crearCreditoRequest.Concepto, crearCreditoRequest.Cuotas);
                  }, "");

        /// <summary>
        /// Método para cancelar un cuota del crédito
        /// </summary>
        /// <param name="cancelarCredito"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CancelarCuota([FromBody] CancelarCredito cancelarCredito) =>
              await HandleRequest(
                  async () =>
                  {
                      return await _clienteUseCase.CancelarCuota(cancelarCredito.IdCliente, cancelarCredito.IdCredito);
                  }, "");

        /// <summary>
        /// Método para ver todos los créditos que tiene un cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<IActionResult> VerCreditosCLiente(string id)
        {
            return await HandleRequest(
                async () =>
                {
                    return await _clienteUseCase.ObtenerCreditosCliente(id);
                }, "");
        }

        /// <summary>
        /// Método para ver los créditos pendientes de un cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<IActionResult> VerCreditosPendientesCLiente(string id)
        {
            return await HandleRequest(
                async () =>
                {
                    return await _clienteUseCase.ObtenerCreditosPendientesCliente(id);
                }, "");
        }
    }
}