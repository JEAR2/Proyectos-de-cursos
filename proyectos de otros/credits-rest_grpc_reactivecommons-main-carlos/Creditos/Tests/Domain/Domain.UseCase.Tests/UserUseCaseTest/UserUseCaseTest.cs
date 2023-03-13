using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using Domain.Model.Tests.Entity;
using Domain.UseCase.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.UseCase.Tests.UserUseCaseTest
{

    public class UserUseCaseTest
    {
        private readonly Mock<ICreditRepository> _creditRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserEventRepository> _userEventRepositoryMock;

        public UserUseCaseTest() { 
            _creditRepositoryMock= new Mock<ICreditRepository>();
            _userRepositoryMock= new Mock<IUserRepository>();
            _userEventRepositoryMock= new Mock<IUserEventRepository>();
        }
        [Fact]
        public async Task CreateUser_Success()
        {
            _userRepositoryMock.Setup(ur => ur.CreateUserAsync(It.IsAny<User>()))
                .ReturnsAsync(GetUserWithId());

            var useCase = new UserUseCase(_userRepositoryMock.Object, _creditRepositoryMock.Object, _userEventRepositoryMock.Object);

            var result = await useCase.CreateUser(GetUser());

            Assert.NotNull(result);
            Assert.Equal("1u", result.Id);
            Assert.Equal("Carlos Romero", result.Name);
            Assert.Equal("carlos@gmail.com", result.Email);

        }
        [Fact]
        public async Task AssignCredit()
        {
            double firstStep = (double)1 + (double)GetCreditWithId().EffectiveAnnualInterestRate / 100;
            double secondStep = (double)1 / (double)12;
            double monthlyInterestRateStep = Math.Pow(firstStep, secondStep);
            var monthlyInterestRate = Math.Round(monthlyInterestRateStep - 1, 4);
            var expectedInstallment = Math.Round(
                (GetCreditWithId().CapitalBase * (monthlyInterestRate * Math.Pow((1 + monthlyInterestRate), GetCreditWithId().NumberOfInstallments))) / ((Math.Pow((1 + monthlyInterestRate), GetCreditWithId().NumberOfInstallments) - 1))
                ,2);

            _userRepositoryMock.Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserWithId());

            _creditRepositoryMock.Setup(c => c.CreateCreditAsync(It.IsAny<Credit>()))
                .ReturnsAsync(GetCreditWithId());

            var useCase = new UserUseCase(_userRepositoryMock.Object, _creditRepositoryMock.Object, _userEventRepositoryMock.Object);

            var result = await useCase.AssignCredit(GetCredit());

            Assert.NotNull(result);
            Assert.Collection(result.Credits,
                c1 =>
                {
                    Assert.Equal(10000, c1.CapitalBase);
                    Assert.Equal(expectedInstallment, c1.InstallmentAmount);
                });
        }
        [Fact]
        public async Task AssignCreditFail()
        {
      
            _userRepositoryMock.Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            _creditRepositoryMock.Setup(c => c.CreateCreditAsync(It.IsAny<Credit>()))
                .ReturnsAsync(GetCreditWithId());

            var useCase = new UserUseCase(_userRepositoryMock.Object, _creditRepositoryMock.Object, _userEventRepositoryMock.Object);


            await Assert.ThrowsAsync<BusinessException>(async() =>await useCase.AssignCredit(GetCredit()));
        }

        [Fact]
        public async Task GetUserCreditsTEst_Success()
        {
            _userRepositoryMock.Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserWithId());

            _creditRepositoryMock.Setup(c => c.GetAllCreditsWithUserId(It.IsAny<string>()))
                .ReturnsAsync(GetCreditsList().Where(c => c.UserId == "1u"));

            var useCase = new UserUseCase(_userRepositoryMock.Object, _creditRepositoryMock.Object, _userEventRepositoryMock.Object);

            var result = await useCase.GetUserCredits("1u");

            Assert.Collection(result.Credits,
                c1 => Assert.Equal(5000, c1.CapitalBase),
                c2 => Assert.Equal(15, c2.EffectiveAnnualInterestRate));

        }

        [Fact]
        public async Task GetCreditStateTest_Success()
        {
            _userRepositoryMock.Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserWithId());
            _creditRepositoryMock.Setup(c => c.GetCreditById(It.IsAny<string>()))
                .ReturnsAsync(GetCreditWithId());

            var useCase = new UserUseCase(_userRepositoryMock.Object, _creditRepositoryMock.Object, _userEventRepositoryMock.Object);

            var result = await useCase.GetCreditState("1", "1");

            Assert.NotEmpty(result.Credits);
            Assert.Equal("Carlos Romero", result.Name);
        }

        [Fact]
        public async Task GetUsers()
        {
            _userRepositoryMock.Setup(u => u.FindAllAsync())
                .ReturnsAsync(GetUsersList());

            _creditRepositoryMock.Setup(c => c.GetAllCreditsWithUserId("1u"))
                .ReturnsAsync(GetCreditsList().Where(c => c.UserId == "1u"));
            _creditRepositoryMock.Setup(c => c.GetAllCreditsWithUserId("2u"))
                .ReturnsAsync(GetCreditsList().Where(c => c.UserId == "2u"));

            var useCase = new UserUseCase(_userRepositoryMock.Object, _creditRepositoryMock.Object, _userEventRepositoryMock.Object);

            var result = await useCase.GetUsers();

            Assert.Collection(result,
                u1 =>
                {
                    Assert.Equal("Carlos Romero", u1.Name);
                    Assert.Collection(u1.Credits,
                        c1 =>
                        {
                            Assert.Equal(5000, c1.CapitalBase);
                        },
                        c2 =>
                        {
                            Assert.Equal(10000, c2.CapitalBase);
                        });
                },
                u2 =>
                {
                    Assert.Equal("Isabela Gil", u2.Name);
                    Assert.Collection(u2.Credits,
                        c1 =>
                        {
                            Assert.Equal(2000, c1.CapitalBase);
                        });
                });
            _creditRepositoryMock.Verify(c => c.GetAllCreditsWithUserId(It.IsAny<string>()), Times.Exactly(2));
        }
        //9636.68
        //480.32
        //Falta testear el caso final (1 cuota)
        [Fact]
        public async Task PayInstallment()
        {
            double firstStep = (double)1 + (double)GetCreditWithId().EffectiveAnnualInterestRate / 100;
            double secondStep = (double)1 / (double)12;
            double monthlyInterestRateStep = Math.Pow(firstStep, secondStep);
            double monthlyInterestRate = Math.Round(monthlyInterestRateStep - 1, 4);
            double capitalExpected = Math.Round(Math.Round(GetCreditWithId().CapitalDebt, 2) - (GetCreditWithId().InstallmentAmount - (Math.Round(GetCreditWithId().CapitalDebt, 2) * monthlyInterestRate)), 2);
            _creditRepositoryMock.Setup(c => c.GetCreditById(It.IsAny<string>()))
                .ReturnsAsync(GetCreditWithId());

            _creditRepositoryMock.Setup(c => c.UpdateCreditAsync(It.IsAny<Credit>()))
                .ReturnsAsync(GetCreditWithIdUpdated());

            _userRepositoryMock.Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserWithId());

            var useCase = new UserUseCase(_userRepositoryMock.Object, _creditRepositoryMock.Object, _userEventRepositoryMock.Object);

            var result = await useCase.PayInstallment("1");

            Assert.Equal(23, result.Credits.ToList()[0].NumberOfInstallments);
            Assert.Equal(capitalExpected, result.Credits.ToList()[0].CapitalDebt);

        }

        private User GetUser() =>
            new UserBuilder()
            .WithName("Carlos Romero")
            .WithEmail("carlos@gmail.com")
            .Build();
        private User GetUserWithId() =>
            new UserBuilder()
            .WithId("1u")
            .WithName("Carlos Romero")
            .WithEmail("carlos@gmail.com")
            .Build();
        private Credit GetCredit() => new CreditBuilder()
            .WithCapitalBase(10000)
            .WithEffectiveAnnualInterestRate(15)
            .WithNumberOfInstallments(24)
            .WithUserId("1u")
            .Build();
        private Credit GetCreditWithId() => new CreditBuilder()
            .WithId("1")
            .WithCapitalBase(10000)
            .WithEffectiveAnnualInterestRate(15)
            .WithNumberOfInstallments(24)
            .WithUserId("1u")
            .Build();
        private Credit GetCreditWithIdUpdated() => new CreditBuilder()
            .WithId("1")
            .WithCapitalBase(1000)
            .WithCapitalDebt(9636.68)
            .WithInstallmentAmount(480.32)
            .WithEffectiveAnnualInterestRate(15)
            .WithNumberOfInstallments(23)
            .WithUserId("1u")
            .WithIsActive(true)
            .TotalBuild();
        private List<Credit> GetCreditsList() => new()
        {
            new CreditBuilder()
            .WithId("2")
            .WithCapitalBase(5000)
            .WithEffectiveAnnualInterestRate(12)
            .WithNumberOfInstallments(12)
            .WithUserId("1u")
            .Build(),
            new CreditBuilder()
            .WithId("3")
            .WithCapitalBase(2000)
            .WithEffectiveAnnualInterestRate(20)
            .WithNumberOfInstallments(12)
            .WithUserId("2u")
            .Build(),
            new CreditBuilder()
            .WithId("1")
            .WithCapitalBase(10000)
            .WithEffectiveAnnualInterestRate(15)
            .WithNumberOfInstallments(24)
            .WithUserId("1u")
            .Build()

        };
        private List<User> GetUsersList() => new()
        {
            new UserBuilder()
            .WithId("1u")
            .WithName("Carlos Romero")
            .WithEmail("carlos@gmail.com")
            .Build(),
            new UserBuilder()
            .WithId("2u")
            .WithName("Isabela Gil")
            .WithEmail("isabela@gmail.com")
            .Build()
        };
    }
}
