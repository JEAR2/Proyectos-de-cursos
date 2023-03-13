using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Tests.Entity;
using Domain.UseCase.Common;
using Domain.UseCase.Users;
using EntryPoints.ReactiveWeb.Controllers;
using EntryPoints.ReactiveWeb.Entity;
using EntryPoints.ReactWeb.Tests.Entity;
using Helpers.ObjectsUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EntryPoints.ReactWeb.Tests.Controller
{
    public class UserControllerTest
    {

        private readonly Mock<IUserUseCase> _userUseCaseMock;
        private readonly Mock<IManageEventsUseCase> _manageEventsUseCase;
        private readonly Mock<IOptions<ConfiguradorAppSettings>> _appSettings;

        private readonly UserController _userController;

        public UserControllerTest()
        {
            _appSettings = new();
            _manageEventsUseCase = new();
            _userUseCaseMock = new();

            _appSettings.Setup(settings => settings.Value)
               .Returns(new ConfiguradorAppSettings
               {
                   DefaultCountry = "co",
                   DomainName = "Credits"
               });

            _userController = new(_userUseCaseMock.Object, _manageEventsUseCase.Object, _appSettings.Object);

            _userController.ControllerContext.HttpContext = new DefaultHttpContext();
            //_userController.ControllerContext.HttpContext.Request.Headers["Location"] = "1,1";
            _userController.ControllerContext.RouteData = new RouteData();
            _userController.ControllerContext.RouteData.Values.Add("controller", "User");
        }

        [Fact]
        public async Task CreateAnUserTest_Status200()
        {
            UserRequest userRequest = GetUserRequest();

            _userUseCaseMock.Setup(u => u.CreateUser(It.IsAny<User>()))
                .ReturnsAsync(GetUser());

            _userController.ControllerContext.RouteData.Values.Add("action", " CreateAUser");

            var result = await _userController.CreateAnUser(userRequest);

            var okObjectResult = result as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateACreditTest_Status200()
        {
            CreditRequest creditRequest = GetCreditRequest();
            User userExpected = GetUserWithIdAndCredits1();

            _userUseCaseMock.Setup(u => u.AssignCredit(It.IsAny<Credit>()))
                .ReturnsAsync(userExpected);

            _userController.ControllerContext.RouteData.Values.Add("action", " CreateAUser");

            var result = await _userController.CreateACredit(creditRequest);

            var okObjectResult = result as OkObjectResult;
            var responseEntity = okObjectResult.Value as ResponseEntity;
            var data = responseEntity.data as User;

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Andres", data.Name);
            Assert.Collection(data.Credits,
                c1 => Assert.Equal(24, c1.NumberOfInstallments));

        }
        [Fact]
        public async Task CreateACreditTest_Status400()
        {
            CreditRequest creditRequest = GetCreditRequestWithoutUserId();
            User userExpected = GetUserWithIdAndCredits1();

            _userUseCaseMock.Setup(u => u.AssignCredit(It.IsAny<Credit>()))
                .Throws(new BusinessException("El credito no puede ser null", 566));

            _userController.ControllerContext.RouteData.Values.Add("action", " CreateAUser");

            BusinessException exception =
                await Assert.ThrowsAsync<BusinessException>(async () => await _userController.CreateACredit(creditRequest));
            Assert.Equal(566, exception.code);
            //try
            //{
            //    await _userController.CreateACredit(creditRequest);
            //}
            //catch(BusinessException ex)
            //{
            //    Assert.Equal(566, ex.code);
            //}
            
            
        }
        [Fact]
        public async Task GetCreditStateTest_Status200()
        {
            User userMock = GetUserWithIdAndCredits2();
            _userUseCaseMock.Setup(u => u.GetCreditState(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(userMock);

            _userController.ControllerContext.RouteData.Values.Add("action", " CreateAUser");

            var result = await _userController.GetCreditState("1u", "1");

            var okObjectResult = result as OkObjectResult;
            var responseEntity = okObjectResult.Value as ResponseEntity;
            var data = responseEntity.data as User;
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Carlos", data.Name);
            Assert.Collection(data.Credits,
                c1 => Assert.Equal(24, c1.NumberOfInstallments),
                c2 => Assert.Equal(12, c2.NumberOfInstallments));
        }
        [Fact]
        public async Task GetAllUsersTest_Status200()
        {
            List<User> mockList = new() { GetUserWithIdAndCredits2(), GetUserWithIdAndCredits1() };
            _userUseCaseMock.Setup(u => u.GetUsers());

            _userController.ControllerContext.RouteData.Values.Add("action", " CreateAUser");

            var result = await _userController.GetAllUsers();

            var okObjectResult = result as OkObjectResult;
            var responseEntity = okObjectResult.Value as ResponseEntity;
            var data = responseEntity.data as User;

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task GetUserCreditsTest_Status200()
        {
            _userUseCaseMock.Setup(u => u.GetUserCredits(It.IsAny<string>()))
                .ReturnsAsync(GetUserWithIdAndCredits2());

            _userController.ControllerContext.RouteData.Values.Add("action", " CreateAUser");

            var result = await _userController.GetUserCredits(It.IsAny<string>());

            Assert.IsType<OkObjectResult>(result);


        }
        [Fact]
        public async Task PayInstallmentTest_Status200()
        {
            _userUseCaseMock.Setup(u => u.PayInstallment(It.IsAny<string>()))
                .ReturnsAsync(GetUserWithIdAndCredits1());

            _userController.ControllerContext.RouteData.Values.Add("action", " CreateAUser");

            var result = await _userController.PayInstallment(It.IsAny<string>());

            Assert.IsType<OkObjectResult>(result);

        }
        private UserRequest GetUserRequest()
            => new UserRequestBuilder()
            .WithEmail("carlos@gmail.com")
            .WithName("Carlos")
            .Build();

        private User GetUser()
            => new UserBuilder()
                .WithName("Carlos")
                .WithEmail("carlos@gmail.com")
                .Build();
        private CreditRequest GetCreditRequest()
            => new CreditRequestBuilder()
            .WithCapitalBase(10000)
            .WithEffectiveAnnualInterestRate(15)
            .WithUserId("1u")
            .WithNumberOfInstallments(24)
            .Build();
        private CreditRequest GetCreditRequestWithoutUserId()
           => new CreditRequestBuilder()
           .WithCapitalBase(10000)
           .WithEffectiveAnnualInterestRate(15)
           .WithUserId("")
           .WithNumberOfInstallments(24)
           .Build();
        private User GetUserWithIdAndCredits1()
           => new UserBuilder()
               .WithId("1u")
               .WithName("Andres")
               .WithEmail("andres@gmail.com")
                .WithCredits(GetCreditList1())
               .Build();
        private User GetUserWithIdAndCredits2()
           => new UserBuilder()
               .WithId("2u")
               .WithName("Carlos")
               .WithEmail("carlos@gmail.com")
                .WithCredits(GetCreditList2())
               .Build();
        private Credit GetCredit()
            => new(10000, 15, 24, "1u");
        private List<Credit> GetCreditList1()
            => new() { new CreditBuilder()
            .WithCapitalBase(10000)
            .WithNumberOfInstallments(24)
            .WithEffectiveAnnualInterestRate(15)
            .WithUserId("2u")
            .Build(), };

        private List<Credit> GetCreditList2()
            => new() { new CreditBuilder()
            .WithCapitalBase(10000)
            .WithNumberOfInstallments(24)
            .WithEffectiveAnnualInterestRate(15)
            .WithUserId("1u")
            .Build(),
            new CreditBuilder()
            .WithCapitalBase(5000)
            .WithNumberOfInstallments(12)
            .WithEffectiveAnnualInterestRate(20)
            .WithUserId("1u")
            .Build()};




    }
}
