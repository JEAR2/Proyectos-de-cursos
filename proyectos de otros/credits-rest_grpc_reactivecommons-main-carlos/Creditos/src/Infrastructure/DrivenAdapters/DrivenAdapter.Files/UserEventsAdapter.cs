using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using Domain.UseCase.Common;
using DrivenAdapter.ServiceBus.Base;
using DrivenAdapter.ServiceBus.Entities;
using Helpers.ObjectsUtils;
using Microsoft.Extensions.Options;
using org.reactivecommons.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapter.ServiceBus
{
    public class UserEventsAdapter : AsyncGatewayAdapterBase, IUserEventRepository
    {
        private readonly IDirectAsyncGateway<UserEntityBus> _directAsyncGatewayUser;
        private readonly IOptions<ConfiguradorAppSettings> _appSettings;

        public UserEventsAdapter(IDirectAsyncGateway<UserEntityBus> directAsyncGatewayUser,
            IManageEventsUseCase manageEventsUseCase,
            IOptions<ConfiguradorAppSettings> appSettings) : base(manageEventsUseCase, appSettings)
        {
            _directAsyncGatewayUser = directAsyncGatewayUser;
            _appSettings = appSettings;
        }

        public async Task AssignedCreditEmailNotification(User user)
        {
            string commandName = "User.AssignCreditNotificationEmail";
            ICollection<CreditEntityBus> credits = user.Credits.Select(
                c => new CreditEntityBus(c.Id, c.CapitalBase, c.CapitalDebt, c.EffectiveAnnualInterestRate, c.InstallmentAmount, c.NumberOfInstallments, c.IsActive, c.UserId))
                .ToList();
            UserEntityBus userWithCredit = new(
                user.Id,
                user.Name,
                user.Email,
                credits);

            await HandleSendCommandAsync(_directAsyncGatewayUser,
                user.Id,
                userWithCredit,
                _appSettings.Value.QueueUserCredit,
                commandName,
                MethodBase.GetCurrentMethod()!);
        }
        public async Task NotifyUserCreated(User user)
        {
            string eventName = "User.NotifyUserCreated";
            ICollection<CreditEntityBus> credits = user.Credits.Select(
                c => new CreditEntityBus(c.Id, c.CapitalBase, c.CapitalDebt, c.EffectiveAnnualInterestRate, c.InstallmentAmount, c.NumberOfInstallments, c.IsActive, c.UserId)
                ).ToList();

            UserEntityBus userWithCredit = new(
                user.Id,
                user.Name,
                user.Email,
                credits);

            await HandleSendEventAsync(_directAsyncGatewayUser,
                user.Id, userWithCredit, _appSettings.Value.TopicUserCredit, eventName,MethodBase.GetCurrentMethod()!);

        }
    }
}
