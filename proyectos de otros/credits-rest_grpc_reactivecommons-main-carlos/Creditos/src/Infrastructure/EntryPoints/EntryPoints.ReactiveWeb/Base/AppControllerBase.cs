using credinet.comun.api;
using credinet.exception.middleware.enums;
using credinet.exception.middleware.models;
using Domain.UseCase.Common;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils;
using Helpers.ObjectsUtils.Extensions;
using Helpers.ObjectsUtils.ResponseObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static credinet.comun.negocio.RespuestaNegocio<credinet.exception.middleware.models.ResponseEntity>;
using static credinet.exception.middleware.models.ResponseEntity;

namespace EntryPoints.ReactiveWeb.Base
{
    /// <summary>
    /// AppControllerBase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="BaseController{T}" />
    public class AppControllerBase<T> : BaseController<T>
    {
        private readonly IManageEventsUseCase _eventsService;
        private readonly ConfiguradorAppSettings _appSettings;

        private string Country => EnvironmentHelper.GetCountryOrDefault(_appSettings.DefaultCountry);

        /// <summary>
        /// Creates new instance of <see cref="AppControllerBase{T}"/>
        /// </summary>
        /// <param name="eventsService"></param>
        /// <param name="appSettings"></param>
        public AppControllerBase(IManageEventsUseCase eventsService,
            IOptions<ConfiguradorAppSettings> appSettings)
        {
            _eventsService = eventsService;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles a controller request.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="requestHandler"></param>
        /// <param name="logId"></param>
        /// <returns></returns>
        public async Task<IActionResult> HandleRequestAsync<TResult>(Func<Task<TResult>> requestHandler, string logId)
        {
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            string eventName = $"{controllerName}.{actionName}";

            _eventsService.ConsoleProcessLog(eventName, logId, data: null);

            try
            {
                TResult result = await requestHandler();
                return await ProcesarResultado(Exito(Build(Request.Path.Value, 0, "", Country, result)));
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _eventsService.ConsoleErrorLog(ex.Message, ex);
                throw new BusinessException(ex.Message, (int)TipoExcepcionNegocio.ExceptionNoControlada);
            }
        }
    }
}