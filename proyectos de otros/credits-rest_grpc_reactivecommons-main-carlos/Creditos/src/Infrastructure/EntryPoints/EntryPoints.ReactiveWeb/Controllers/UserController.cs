using Domain.Model.Entities;
using Domain.UseCase.Common;
using Domain.UseCase.Users;
using EntryPoints.ReactiveWeb.Base;
using EntryPoints.ReactiveWeb.Entity;
using Helpers.ObjectsUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    [Route("api/[controller]/[action]")]
    public class UserController : AppControllerBase<UserController>
    {
        private readonly IUserUseCase _userUseCase;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userUseCase"></param>
        /// <param name="eventsService"></param>
        /// <param name="appSettings"></param>
        public UserController(IUserUseCase userUseCase, IManageEventsUseCase eventsService, IOptions<ConfiguradorAppSettings> appSettings) :
            base(eventsService, appSettings)
        {
            _userUseCase = userUseCase;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateAnUser(UserRequest userRequest) =>
            await HandleRequestAsync(
                async () =>
                {
                    User newUser = userRequest.AsEntity();
                    return await _userUseCase.CreateUser(newUser);
                }, "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creditRequest"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateACredit(CreditRequest creditRequest) =>
            await HandleRequestAsync(
                async () =>
                {
                    Credit newCredit = creditRequest.AsEntity();
                    return await _userUseCase.AssignCredit(newCredit);
                }, ""
                );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="creditId"></param>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetCreditState([FromQuery]string userId, [FromQuery]string creditId) =>
            await HandleRequestAsync(
                async () =>
                {
                    return await _userUseCase.GetCreditState(userId, creditId);
                }, ""
                );
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetAllUsers() =>
            await HandleRequestAsync(
                async () =>
                {
                    return await _userUseCase.GetUsers();
                }, ""
                );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetUserCredits(string userId) =>
            await HandleRequestAsync(
                async () =>
                {
                    return await _userUseCase.GetUserCredits(userId);
                }, ""
                );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> PayInstallment(string creditId) =>
            await HandleRequestAsync(
                async () =>
                {
                    return await _userUseCase.PayInstallment(creditId);
                }, ""
                );
    }
}