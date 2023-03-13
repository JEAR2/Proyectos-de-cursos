using Domain.Model.Entities.Request;
using Domain.UseCase.Common;
using System;
using System.Threading.Tasks;

namespace Domain.UseCase.TiendaCommand
{
    /// <summary>
    /// <see cref="ITiendaCommandUseCase"/>
    /// </summary>
    public class TiendaCommandUseCase : ITiendaCommandUseCase
    {
        private readonly IManageEventsUseCase _manageEventsUseCase;

        /// <summary>
        /// Constructor
        /// </summary>
        public TiendaCommandUseCase(IManageEventsUseCase manageEventsUseCase)
        {
            _manageEventsUseCase = manageEventsUseCase;
        }

        /// <summary>
        ///  <see cref="ITiendaCommandUseCase.NotificarTiendaCreada(TiendaRequest)"/>
        /// </summary>
        /// <param name="tienda"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task NotificarTiendaCreada(TiendaRequest tienda)
        {
            await Task.Run(() =>
            {
                //TODO: Aqui se podria llamar un driven adapter si es necesario.
                _manageEventsUseCase.ConsoleInfoLog("Notificar via email la creación de la tienda.", tienda.Nombre);
            });
        }
    }
}