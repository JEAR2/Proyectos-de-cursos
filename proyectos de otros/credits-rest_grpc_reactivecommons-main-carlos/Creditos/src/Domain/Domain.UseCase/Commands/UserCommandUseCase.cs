using Domain.Model.Request;
using Domain.UseCase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Commands
{


    /// <summary>
    /// Implementation of user command use case
    /// </summary>
    public class UserCommandUseCase : IUserCommandUseCase
    {
        private readonly IManageEventsUseCase _manageEventsUseCase;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserCommandUseCase(IManageEventsUseCase manageEventsUseCase)
        {
            _manageEventsUseCase = manageEventsUseCase;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task ShowCommand(UserRequest user)
        {
            await Task.Run(() =>
            {
                //TODO: Aqui se podria llamar un driven adapter si es necesario.
                _manageEventsUseCase.ConsoleInfoLog("Notificar via email la creación de la tienda.", user.Email);
            });
        }
    }
}
