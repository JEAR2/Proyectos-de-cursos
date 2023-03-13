using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace DrivenAdapters.Mongo
{
    /// <summary>
    /// Context is an implementation of <see cref="IContext"/>
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Context : IContext
    {
        private readonly IMongoDatabase _database;

        /// <summary>
        /// crea una nueva instancia de la clase <see cref="Context"/>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        public Context(string connectionString, string databaseName)
        {
            MongoClient _mongoClient = new MongoClient(connectionString);
            _database = _mongoClient.GetDatabase(databaseName);
        }

        /// <summary>
        /// User Entity
        /// </summary>
        public IMongoCollection<UserEntity> Users => _database.GetCollection<UserEntity>("User");
        /// <summary>
        /// Credit Entity
        /// </summary>
        public IMongoCollection<CreditEntity> Credits => _database.GetCollection<CreditEntity>("Credit");
    }
}