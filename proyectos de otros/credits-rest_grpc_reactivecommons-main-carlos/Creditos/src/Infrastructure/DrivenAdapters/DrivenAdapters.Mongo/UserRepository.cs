using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo
{
    /// <summary>
    /// Class
    /// </summary>
    public class UserRepository : IUserRepository
    {

        private readonly IMongoCollection<UserEntity> _collectionUsers;
        /// <summary>
        /// Ctor with collection
        /// </summary>
        /// <param name="mongodb"></param>
        public UserRepository(IContext mongodb)
        {
            _collectionUsers = mongodb.Users;
        }
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> CreateUserAsync(User user)
        {
            UserEntity userEntity = new(user.Name, user.Email);
            await _collectionUsers.InsertOneAsync(userEntity);

            return userEntity.AsEntity();
        }       
        /// <summary>
        /// Find all users
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<User>> FindAllAsync()
        {
            IAsyncCursor<UserEntity> usersEntity =
                await _collectionUsers.FindAsync(Builders<UserEntity>.Filter.Empty);

            List<User> users = usersEntity.ToEnumerable()
                .Select(userEntity => userEntity.AsEntity()).ToList();

            return users;
        }
        /// <summary>
        /// Find one user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> FindByIdAsync(string id)
        {
            IAsyncCursor<UserEntity> usersEntity = 
                await _collectionUsers.FindAsync(Builders<UserEntity>.Filter.Eq(user => user.Id, id));
            User user = usersEntity.FirstOrDefault().AsEntity();
            return user;
        }

    }
}
