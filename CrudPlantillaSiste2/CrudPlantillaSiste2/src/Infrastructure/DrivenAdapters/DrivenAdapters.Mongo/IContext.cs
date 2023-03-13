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
        /// Empleados
        /// </summary>
        IMongoCollection<EmpleadoEntity> Empleados { get; }

        /// <summary>
        /// Departamentos
        /// </summary>
        IMongoCollection<DepartamentoEntity> Departamentos { get; }
    }
}