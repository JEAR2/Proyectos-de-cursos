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
    public class UserRepositoryTest
    {
        private readonly Mock<IContext> _mockContext;
        private readonly Mock<IMongoCollection<UserEntity>> _mockUsersCollection;
        private readonly Mock<IAsyncCursor<UserEntity>> _userCursor;

        public UserRepositoryTest()
        {
            _mockContext = new();
            _mockUsersCollection = new();
            _userCursor = new();

            _mockUsersCollection.Object.InsertMany(GetUsersTest());

            _userCursor.SetupSequence(user => user.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _userCursor.SetupSequence(user => user.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));
        }

        [Fact]
        public async Task CreateUserAsyncTest_Success()
        {
            _mockUsersCollection.Setup(userCollection => userCollection
            .InsertOneAsync(
                It.IsAny<UserEntity>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()));

            _mockContext.Setup(c => c.Users).Returns(_mockUsersCollection.Object);

            var userRepo = new UserRepository(_mockContext.Object);

            User result = await userRepo.CreateUserAsync(GetUser("Andrea Rodriguez", "andrea.rodriguez@gmail.com"));

            Assert.NotNull(result);
            Assert.Equal("Andrea Rodriguez", result.Name);
            Assert.Equal("andrea.rodriguez@gmail.com", result.Email);
            
        }
        [Fact]
        public async Task GetAllUsersAsyncTest_Success()
        {
            _userCursor.Setup(user => user.Current).Returns(GetUsersTest());

            _mockUsersCollection.Setup(userCollection => userCollection
            .FindAsync(
                It.IsAny<FilterDefinition<UserEntity>>(),
                It.IsAny<FindOptions<UserEntity, UserEntity>>(),
                It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(_userCursor.Object);

            _mockContext.Setup(c => c.Users).Returns(_mockUsersCollection.Object);

            var userRepo = new UserRepository(_mockContext.Object);

            var result = await userRepo.FindAllAsync();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Collection(result,
                user => Assert.Equivalent(GetUsersTest()[0], user),
                user2 => Assert.Equivalent(GetUsersTest()[1], user2));
        }

        [Fact]
        public async Task FindUserByIdAsync()
        {
            _userCursor.Setup(c => c.Current).Returns(GetUsersTest().Where(u => u.Id == "1"));

            _mockUsersCollection.Setup(userCollection => userCollection
            .FindAsync(
                It.IsAny<FilterDefinition<UserEntity>>(),
                It.IsAny<FindOptions<UserEntity, UserEntity>>(),
                It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(_userCursor.Object);

            _mockContext.Setup(c => c.Users).Returns(_mockUsersCollection.Object);

            var userRepo = new UserRepository(_mockContext.Object);

            var result = await userRepo.FindByIdAsync("1");
            Assert.Equivalent(GetUsersTest()[0].Email, result.Email);
            Assert.Equivalent(GetUsersTest()[0].Name, result.Name);

        }

        #region Utils
        private User GetUser(string name, string email) => new UserBuilder()
            .WithName(name)
            .WithEmail(email)
            .Build();

        private List<UserEntity> GetUsersTest() => new()
        {
            new UserEntityBuilder()
                .WithEmail("carlos@gmail.com")
                .WithName("Carlos Romero")
                .WithId("1")
                .Build(),
            new  UserEntityBuilder()
                .WithEmail("antonio@gmail.com")
                .WithName("Antonio Franco")
                .WithId("2")
                .Build()
        };

        #endregion Utils
    }
}
