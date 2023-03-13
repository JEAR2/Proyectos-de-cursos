using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace DrivenAdapters.Mongo.Entities
{
    /// <summary>
    /// Departamento DTO
    /// </summary>
    public class DepartamentoEntity : IDomainEntity<Departamento>
    {
        /// <summary>
        /// Id
        /// </summary>
        ///

        public int Id { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        public DepartamentoEntity(int id, string nombre, string descripcion)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
        }

        /// <summary>
        /// Convertir a una entidad de Dominio
        /// </summary>
        /// <returns></returns>
        public Departamento AsEntity()
        {
            return new(Id, Nombre, Descripcion);
        }
    }
}