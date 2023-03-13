using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace DrivenAdapters.Mongo.Entities
{
    /// <summary>
    /// Ubicación Entity
    /// </summary>
    public class UbicacionEntity : IDomainEntity<Ubicacion>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="latitud"></param>
        /// <param name="longitud"></param>
        public UbicacionEntity(decimal latitud, decimal longitud)
        {
            Latitud = latitud;
            Longitud = longitud;
        }

        /// <summary>
        /// Latitud
        /// </summary>
        [BsonElement("latitud")]
        public decimal Latitud { get; set; }

        /// <summary>
        /// Longitud
        /// </summary>
        [BsonElement("longitud")]
        public decimal Longitud { get; set; }

        /// <summary>
        /// Convertir a entidad de dominio
        /// </summary>
        /// <returns></returns>
        public Ubicacion AsEntity() =>
            new(Latitud, Longitud);
    }
}