using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DrivenAdapters.Mongo.Entities
{
    /// <summary>
    /// Clase ClienteEntity
    /// </summary>
    public class ClienteEntity : EntityBase, IDomainEntity<Cliente>
    {
        /// <summary>
        /// Nombre
        /// </summary>
        [BsonElement("nombre")]
        public string Nombre { get; set; }

        /// <summary>
        /// Apellido
        /// </summary>
        [BsonElement("apellido")]
        public string Apellido { get; set; }

        /// <summary>
        /// Correo
        /// </summary>
        [BsonElement("correo")]
        public string Correo { get; set; }

        /// <summary>
        /// País
        /// </summary>
        [BsonElement("pais")]
        public string Pais { get; set; }

        /// <summary>
        /// Créditos
        /// </summary>
        [BsonElement("creditos")]
        public List<Credito> Creditos { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="correo"></param>
        /// <param name="pais"></param>
        /// <param name="creditos"></param>
        public ClienteEntity(string id, string nombre, string apellido, string correo, string pais, List<Credito> creditos)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Pais = pais;
            Creditos = creditos;
        }

        /// <summary>
        /// Constructor sin Id
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="correo"></param>
        /// <param name="pais"></param>
        /// <param name="creditos"></param>
        public ClienteEntity(string nombre, string apellido, string correo, string pais, List<Credito> creditos)
        {
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Pais = pais;
            Creditos = creditos;
        }

        /// <summary>
        /// <see cref="IDomainEntity{T}.AsEntity()"/>
        /// </summary>
        /// <returns></returns>
        public Cliente AsEntity()
        {
            return new Cliente(Id, Nombre, Apellido, Correo, Pais, Creditos);
        }
    }
}