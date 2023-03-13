using Domain.UseCase.Users;
using Grpc.Core;
using Service.GrpcCreditService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Service.GrpcCreditService.CreditService;

namespace DrivenAdapter.grpc.Service
{
    public class CreditService : CreditServiceBase
    {
        private readonly IUserUseCase _userUseCase;

        public CreditService(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        public override async Task<UsersListReply> GetAllUsers(EmptyRequest request, ServerCallContext context)
        {
            UsersListReply reply = new UsersListReply(); 
            var usersList = await _userUseCase.GetUsers();
            var users = usersList.Select(u =>
            {
                var creditsRepeated = new Credits();

                var credits = from c in u.Credits select
                        new Credit()
                        {
                            Id= c.Id,
                            CapitalBase= c.CapitalBase,
                            CapitalDebt= c.CapitalDebt,
                            InstallmentAmount= c.InstallmentAmount,
                            EffectiveAnnualInterestRate =c.EffectiveAnnualInterestRate,
                            NumberOfInstallments =c.NumberOfInstallments,
                            UserId=c.UserId,
                            IsActive =c.IsActive
                        };

                creditsRepeated.Credits_.AddRange(credits);


                return new User()
                {

                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Credits = creditsRepeated

                };
            });

            reply.Users.AddRange(users);
            return reply; 
        }

        public override async Task<User> CreateUser(User request, ServerCallContext context)
        {

            Domain.Model.Entities.User userDomain = new(request.Name, request.Email);
            var userCreated = await _userUseCase.CreateUser(userDomain);
            var creditsRepeated = new Credits();
            var rangeCreditGrpc = userCreated.Credits.Select(c => new Credit()
            {
                Id = c.Id,
                NumberOfInstallments = c.NumberOfInstallments,
                InstallmentAmount = c.InstallmentAmount,
                CapitalBase = c.CapitalBase,
                CapitalDebt = c.CapitalDebt,
                IsActive = c.IsActive,
                UserId = c.UserId,
                EffectiveAnnualInterestRate = c.EffectiveAnnualInterestRate
            });
            creditsRepeated.Credits_.AddRange(rangeCreditGrpc);
            User user = new ()
            {
             
                 Id = userCreated.Id,
                Name = userCreated.Name,
                Email = userCreated.Email,
                Credits = creditsRepeated
            };
            return user;
        }

        public override async Task<User> AssignCredit(NewCredit request, ServerCallContext context)
        {
            Domain.Model.Entities.Credit creditDomain = new(request.CapitalBase, request.EffectiveAnnualInterestRate, request.NumberOfInstallments, request.UserId);
            Domain.Model.Entities.User userDomain =  await _userUseCase.AssignCredit(creditDomain);

            var credits = userDomain.Credits.Select(c => new Credit()
            {
                Id = c.Id,
                NumberOfInstallments = c.NumberOfInstallments,
                InstallmentAmount = c.InstallmentAmount,
                CapitalBase = c.CapitalBase,
                CapitalDebt = c.CapitalDebt,
                IsActive = c.IsActive,
                UserId = c.UserId,
                EffectiveAnnualInterestRate = c.EffectiveAnnualInterestRate
            });

            User userResult = new();
            userResult.Id = userDomain.Id;
            userResult.Name = userDomain.Name;
            userResult.Email = userDomain.Email;
            userResult.Credits.Credits_.AddRange(credits);
            return userResult;
        }

        public override async Task<User> GetUserCredits(UserId request, ServerCallContext context)
        {
            var userDomain = await _userUseCase.GetUserCredits(request.Id);

            var credits = userDomain.Credits.Select(c => new Credit()
            {
                Id = c.Id,
                NumberOfInstallments = c.NumberOfInstallments,
                InstallmentAmount = c.InstallmentAmount,
                CapitalBase = c.CapitalBase,
                CapitalDebt = c.CapitalDebt,
                IsActive = c.IsActive,
                UserId = c.UserId,
                EffectiveAnnualInterestRate = c.EffectiveAnnualInterestRate
            });

            User userResult = new();
            userResult.Id = userDomain.Id;
            userResult.Name = userDomain.Name;
            userResult.Email = userDomain.Email;
            userResult.Credits.Credits_.AddRange(credits);
            return userResult;
        }

        public override async Task<User> GetCreditState(UserAndCreditId request, ServerCallContext context)
        {
            var userDomain = await _userUseCase.GetCreditState(request.UserId, request.UserId);

            var credits = userDomain.Credits.Select(c => new Credit()
            {
                Id = c.Id,
                NumberOfInstallments = c.NumberOfInstallments,
                InstallmentAmount = c.InstallmentAmount,
                CapitalBase = c.CapitalBase,
                CapitalDebt = c.CapitalDebt,
                IsActive = c.IsActive,
                UserId = c.UserId,
                EffectiveAnnualInterestRate = c.EffectiveAnnualInterestRate
            });

            User userResult = new();
            userResult.Id = userDomain.Id;
            userResult.Name = userDomain.Name;
            userResult.Email = userDomain.Email;
            userResult.Credits.Credits_.AddRange(credits);
            return userResult;
           
        }

        public override async Task<User> PayInstallment(CreditId request, ServerCallContext context)
        {
            var userDomain = await _userUseCase.PayInstallment(request.Id);

            var credits = userDomain.Credits.Select(c => new Credit()
            {
                Id = c.Id,
                NumberOfInstallments = c.NumberOfInstallments,
                InstallmentAmount = c.InstallmentAmount,
                CapitalBase = c.CapitalBase,
                CapitalDebt = c.CapitalDebt,
                IsActive = c.IsActive,
                UserId = c.UserId,
                EffectiveAnnualInterestRate = c.EffectiveAnnualInterestRate
            });

            User userResult = new();
            userResult.Id = userDomain.Id;
            userResult.Name = userDomain.Name;
            userResult.Email = userDomain.Email;
            userResult.Credits.Credits_.AddRange(credits);
            return userResult;
        }

    }
}


