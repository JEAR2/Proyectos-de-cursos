using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DrivenAdapters.Mongo.Entities
{
    /// <summary>
    /// Tienda DTO
    /// </summary>
    public class TipoEntity : IDomainEntity<Tipo>
    {
        /// <summary>
        /// Id
        /// </summary>
        [BsonId]
        public int Id { get; set; }

        /// <summary>
        /// Valor Nombre
        /// </summary>
        [BsonElement("nombre")]
        public string Nombre { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        public TipoEntity(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        /// <summary>
        /// Convertir a entidad de dominio
        /// </summary>
        /// <returns></returns>
        public Tipo AsEntity() =>
            new(Id, Nombre);
    }
}