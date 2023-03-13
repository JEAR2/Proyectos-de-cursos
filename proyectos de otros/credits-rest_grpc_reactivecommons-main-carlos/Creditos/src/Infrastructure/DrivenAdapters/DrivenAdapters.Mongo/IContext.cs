using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;

namespace DrivenAdapters.Mongo
{
    /// <summary>
    /// Interfaz Mongo context contract.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Collection of Users
        /// </summary>
        public IMongoCollection<UserEntity> Users { get; }
        /// <summary>
        /// Collection of Credits
        /// </summary>
        public IMongoCollection<CreditEntity> Credits { get; }
    }
}