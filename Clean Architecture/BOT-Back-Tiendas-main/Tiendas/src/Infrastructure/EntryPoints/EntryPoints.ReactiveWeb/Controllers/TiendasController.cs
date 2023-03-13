using Domain.Model.Entities;
using Domain.UseCase.Common;
using Domain.UseCase.Tiendas;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entity;
using Helpers.ObjectsUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace EntryPoints.ReactiveWeb.Controllers
{
    /// <summary>
    /// TiendasController
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class TiendasController : AppControllerBase<TiendasController>
    {
        private readonly ITiendaUseCase _tiendaUseCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="TiendasController"/> class.
        /// </summary>
        /// <param name="tiendaUseCase">The test negocio.</param>
        /// <param name="eventsService">The logger.</param>
        /// <param name="appSettings"></param>
        public TiendasController(ITiendaUseCase tiendaUseCase,
            IManageEventsUseCase eventsService,
            IOptions<ConfiguradorAppSettings> appSettings)
            : base(eventsService, appSettings)
        {
            _tiendaUseCase = tiendaUseCase;
        }

        /// <summary>
        /// CrearAlmacenes
        /// </summary>
        /// <param name="tiendaRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CrearAlmacenes(TiendaRequest tiendaRequest) =>
                await HandleRequestAsync(
                    async () =>
                    {
                        Tienda nuevaTienda = tiendaRequest.AsEntity();
                        return await _tiendaUseCase.CrearTienda(nuevaTienda);
                    }, "");

        /// <summary>
        /// Método para Obtener los Asesores
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerTiendas() =>
            await HandleRequestAsync(
                    async () =>
                    {
                        return await _tiendaUseCase.ObtenerTiendas();
                    }, "");
    }
}