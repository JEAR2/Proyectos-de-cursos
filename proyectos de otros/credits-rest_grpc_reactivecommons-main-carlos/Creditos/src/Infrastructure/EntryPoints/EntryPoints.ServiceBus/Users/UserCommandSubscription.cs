using Domain.Model.Request;
using Domain.UseCase.Commands;
using Domain.UseCase.Common;
using EntryPoints.ServiceBus.Base;
using Helpers.ObjectsUtils;
using Microsoft.Extensions.Options;
using org.reactivecommons.api;
using org.reactivecommons.api.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoints.ServiceBus.Users
{
    public class UserCommandSubscription : SubscriptionBase, IUserCommandSubscription
    {
        private readonly IOptions<ConfiguradorAppSettings> _appSettings;
        private readonly IDirectAsyncGateway<UserRequest> _directAsyncGateway;
        private readonly IUserCommandUseCase _userCommandUseCase;

        public UserCommandSubscription(IOptions<ConfiguradorAppSettings> appSettings,
            IManageEventsUseCase manageEventsUseCase,
            IDirectAsyncGateway<UserRequest> directAsyncGateway,
            IUserCommandUseCase userCommandUseCase)
            :base(manageEventsUseCase, appSettings)
        {
            _appSettings = appSettings;
            _directAsyncGateway = directAsyncGateway;
            _userCommandUseCase = userCommandUseCase;
        }

        public async Task SubscribeAsync()
        {
            await SubscribeOnCommandAsync(
                _directAsyncGateway,
                _appSettings.Value.QueueUserCredit,
                EmailNotificationCommand,
                MethodBase.GetCurrentMethod()!,
                maxConcurrentCalls: 1);
                
        }

        public async Task EmailNotificationCommand(Command<UserRequest> userRequest) =>
            await HandleRequestAsync(async (user) =>
            {
                await _userCommandUseCase.ShowCommand(user);
            },
             MethodBase.GetCurrentMethod(),
             Guid.NewGuid().ToString(),
             ///Este parámetro tipa la función
             userRequest
             );
    }
}
