using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities.Base;

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
        public int Id { get; private set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; private set; }

        /// <summary>
        /// Descripcion
        /// </summary>
        public string Descripcion { get; private set; }

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