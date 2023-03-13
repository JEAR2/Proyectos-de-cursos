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
        /// Colección de Clientes
        /// </summary>
        public IMongoCollection<ClienteEntity> Clientes { get; }

        /// <summary>
        /// Colección de Créditos
        /// </summary>
        public IMongoCollection<CreditoEntity> Creditos { get; }
    }
}