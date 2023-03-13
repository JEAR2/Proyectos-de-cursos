using Domain.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Commands
{
    /// <summary>
    /// Interface of user command usecase
    /// </summary>
    public interface IUserCommandUseCase
    {
        /// <summary>
        /// Show command 
        /// </summary>
        /// <returns></returns>
        Task ShowCommand(UserRequest user);
    }
}
