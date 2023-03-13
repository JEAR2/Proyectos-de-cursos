
using Domain.Model.Entities;
using Domain.Model.Tests.Entity;
using DrivenAdapters.Mongo.Entities;
using DrivenAdapters.Mongo.Tests.Entity;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DrivenAdapters.Mongo.Tests.Mongo
{
    public class CreditRepositoryTest
    {
        private readonly Mock<IContext> _mockContext;
        private readonly Mock<IMongoCollection<CreditEntity>> _mockCreditCollection;
        private readonly Mock<IAsyncCursor<CreditEntity>> _creditCursor;
        public CreditRepositoryTest() {
            _mockContext = new();
            _mockCreditCollection = new();
            _creditCursor = new();

            _mockCreditCollection.Object.InsertMany(GetCreditsTest());

            _creditCursor.SetupSequence(user => user.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _creditCursor.SetupSequence(user => user.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));
        }
        [Fact]
        public async Task CreateCreditAsyncTest_Success()
        {
            var expectedCredit = GetCredit();

            _mockCreditCollection.Setup(collection => collection
            .InsertOneAsync(
                It.IsAny<CreditEntity>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()))
             .Returns(Task.FromResult(GetCreditWithId()));

            _mockContext.Setup(c => c.Credits).Returns(_mockCreditCollection.Object);

            var creditRepo = new CreditRepository(_mockContext.Object);

            var result = await creditRepo.CreateCreditAsync(GetCredit());
            

            Assert.NotNull(result);
            Assert.Equivalent(expectedCredit, result);
            _mockCreditCollection.Verify(c => c.InsertOneAsync(It.IsAny<CreditEntity>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()), Times.Once());
            

        }

        [Fact]
        public async Task GetAllCreditsWithUserIdTest_Success()
        {
            _creditCursor.Setup(cursor => cursor.Current).Returns(GetCreditsTest().Where(c=>c.UserId=="1u"));

            _mockCreditCollection.Setup(c => c.FindAsync(
                It.IsAny<FilterDefinition<CreditEntity>>(),
                It.IsAny<FindOptions<CreditEntity, CreditEntity>>(),
                It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(_creditCursor.Object);

            _mockContext.Setup(c => c.Credits).Returns(_mockCreditCollection.Object); 

            var creditRepo = new CreditRepository(_mockContext.Object);

            var result = await creditRepo.GetAllCreditsWithUserId("1u");

            Assert.NotNull(result);
            Assert.Collection(result,
                c1 => Assert.Equal(5000, c1.CapitalDebt),
                c2 => Assert.Equal(10000, c2.CapitalDebt));
        }

        [Fact]
        public async Task GetCreditByIdTest_Success()
        {
            _creditCursor.Setup(cursor => cursor.Current).Returns(GetCreditsTest().Where(c => c.Id == "1"));

            _mockCreditCollection.Setup(c => c.FindAsync(
                It.IsAny<FilterDefinition<CreditEntity>>(),
                It.IsAny<FindOptions<CreditEntity, CreditEntity>>(),
                It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(_creditCursor.Object);

            _mockContext.Setup(c => c.Credits).Returns(_mockCreditCollection.Object);

            var creditRepo = new CreditRepository(_mockContext.Object);

            var result = await creditRepo.GetCreditById(It.IsAny<string>());

            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
            Assert.Equal(10000, result.CapitalDebt);
        }

        [Fact]
        public async Task UpdateCreditAsyncTest_Success()
        {
            
            _mockCreditCollection.Setup(c => c.FindOneAndReplaceAsync<CreditEntity>(
                It.IsAny<FilterDefinition<CreditEntity>>(),
                It.IsAny<CreditEntity>(),
                It.IsAny<FindOneAndReplaceOptions<CreditEntity, CreditEntity>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetCreditEntityWithId());



            _mockContext.Setup(c => c.Credits).Returns(_mockCreditCollection.Object);

            var creditRepo = new CreditRepository(_mockContext.Object);

            var result = await creditRepo.UpdateCreditAsync(GetCreditWithId());

            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
            Assert.Equal(24, result.NumberOfInstallments);
            _mockCreditCollection.Verify(c => c.FindOneAndReplaceAsync(It.IsAny<FilterDefinition<CreditEntity>>(),
                It.IsAny<CreditEntity>(),
                It.IsAny<FindOneAndReplaceOptions<CreditEntity, CreditEntity>>(),
                It.IsAny<CancellationToken>()), Times.Once());
        }

        private Credit GetCredit() => new CreditBuilder()
            .WithCapitalBase(10000)
            .WithEffectiveAnnualInterestRate(15)
            .WithNumberOfInstallments(24)
            .WithEffectiveAnnualInterestRate(15)
            .WithUserId("1u")
            .Build();

        private Credit GetCreditWithId() => new CreditBuilder()
            .WithId("1")
            .WithCapitalBase(10000)
            .WithEffectiveAnnualInterestRate(15)
            .WithNumberOfInstallments(24)
            .WithEffectiveAnnualInterestRate(15)
            .WithUserId("1u")
            .Build();

        private CreditEntity GetCreditEntityWithId() => new CreditEntityBuilder()
           .WithId("1")
           .WithCapitalBase(10000)
           .WithEffectiveAnnualInterestRate(15)
           .WithNumberOfInstallments(24)
           .WithEffectiveAnnualInterestRate(15)
           .WithUserId("1u")
           .Build();


        private List<CreditEntity> GetCreditsTest() => new()
        {
            
            new CreditEntityBuilder()
                .WithId("2")
                .WithNumberOfInstallments(12)
                .WithCapitalDebt(5000)
                .WithIsActive(false)
                .WithEffectiveAnnualInterestRate(10)
                .WithUserId("1u")
                .Build(),
            new CreditEntityBuilder()
                .WithId("1")
                .WithNumberOfInstallments(24)
                .WithCapitalDebt(10000)
                .WithIsActive(true)
                .WithEffectiveAnnualInterestRate(15)
                .WithUserId("1u")
                .Build(),
            new CreditEntityBuilder()
                .WithId("3")
                .WithNumberOfInstallments(12)
                .WithCapitalDebt(5000)
                .WithIsActive(false)
                .WithEffectiveAnnualInterestRate(10)
                .WithUserId("2u")
                .Build()

        };
    }



}
