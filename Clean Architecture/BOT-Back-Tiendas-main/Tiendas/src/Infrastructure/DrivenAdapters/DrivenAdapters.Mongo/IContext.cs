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
        /// Almacenes
        /// </summary>
        IMongoCollection<TiendaEntity> Tiendas { get; }

        /// <summary>
        /// Tipos
        /// </summary>
        IMongoCollection<TipoEntity> Tipos { get; }
    }
}