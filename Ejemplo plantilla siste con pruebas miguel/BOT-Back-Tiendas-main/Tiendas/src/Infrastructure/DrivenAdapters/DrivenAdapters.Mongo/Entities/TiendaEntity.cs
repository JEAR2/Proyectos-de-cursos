using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DrivenAdapters.Mongo.Entities
{
    /// <summary>
    /// Tienda DTO
    /// </summary>
    public class TiendaEntity : EntityBase, IDomainEntity<Tienda>
    {
        /// <summary>
        /// Valor Nombre
        /// </summary>
        [BsonElement(elementName: "nombre")]
        public string Nombre { get; set; }

        /// <summary>
        /// Tipo
        /// </summary>
        [BsonElement(elementName: "tipo")]
        public TipoEntity Tipo { get; set; }

        /// <summary>
        /// Dirección
        /// </summary>
        [BsonElement(elementName: "direccion")]
        public string Direccion { get; set; }

        /// <summary>
        /// Ubicación
        /// </summary>
        [BsonElement(elementName: "ubicacion")]
        public UbicacionEntity Ubicacion { get; set; }

        /// <summary>
        /// Valor Numero del Celular
        /// </summary>
        [BsonElement(elementName: "celular")]
        public string Celular { get; set; }

        /// <summary>
        /// Valor de el Correo
        /// </summary>
        [BsonElement(elementName: "correo")]
        public string Correo { get; set; }

        /// <summary>
        /// Valor Fecha Creación
        /// </summary>
        [BsonElement(elementName: "fechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Convertir a entidad de dominio
        /// </summary>
        /// <returns></returns>
        public Tienda AsEntity() =>
            new(Id, Nombre, Tipo.AsEntity(), Direccion, Ubicacion.AsEntity(),
                Celular, Correo, FechaCreacion);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        /// <param name="direccion"></param>
        /// <param name="ubicacion"></param>
        /// <param name="celular"></param>
        /// <param name="correo"></param>
        /// <param name="fechaCreacion"></param>
        public TiendaEntity(string nombre, TipoEntity tipo, string direccion, UbicacionEntity ubicacion, string celular,
            string correo, DateTime fechaCreacion)
        {
            Nombre = nombre;
            Tipo = tipo;
            Direccion = direccion;
            Ubicacion = ubicacion;
            Celular = celular;
            Correo = correo;
            FechaCreacion = fechaCreacion;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        /// <param name="direccion"></param>
        /// <param name="ubicacion"></param>
        /// <param name="celular"></param>
        /// <param name="correo"></param>
        /// <param name="fechaCreacion"></param>
        public TiendaEntity(string id, string nombre, TipoEntity tipo, string direccion, UbicacionEntity ubicacion, string celular,
            string correo, DateTime fechaCreacion)
        {
            Id = id;
            Nombre = nombre;
            Tipo = tipo;
            Direccion = direccion;
            Ubicacion = ubicacion;
            Celular = celular;
            Correo = correo;
            FechaCreacion = fechaCreacion;
        }
    }
}