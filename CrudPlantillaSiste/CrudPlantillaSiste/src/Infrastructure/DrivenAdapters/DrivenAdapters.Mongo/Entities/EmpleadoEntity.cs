using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities.Base;

namespace DrivenAdapters.Mongo.Entities
{
    /// <summary>
    /// Empleado DTO
    /// </summary>
    public class EmpleadoEntity : EntityBase, IDomainEntity<Empleado>
    {
        /// <summary>
        /// Id
        /// </summary>
        public string id { get; private set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; private set; }

        /// <summary>
        /// Apellido
        /// </summary>
        public string Apellido { get; private set; }

        /// <summary>
        /// Edad
        /// </summary>
        public int Edad { get; private set; }

        /// <summary>
        /// Correo
        /// </summary>
        public string Correo { get; private set; }

        /// <summary>
        /// Sexo
        /// </summary>
        public string Sexo { get; private set; }

        /// <summary>
        /// Departamento
        /// </summary>
        public Departamento Departamento { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="edad"></param>
        /// <param name="correo"></param>
        /// <param name="sexo"></param>
        /// <param name="departamento"></param>
        public EmpleadoEntity(string nombre, string apellido, int edad, string correo, string sexo, Departamento departamento)
        {
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Correo = correo;
            Sexo = sexo;
            Departamento = departamento;
        }

        /// <summary>
        /// Convertir a entidad de dominio Empleado
        /// </summary>
        /// <returns></returns>
        public Empleado AsEntity()
        {
            return new(id, Nombre, Apellido, Edad, Correo, Sexo, Departamento);
        }
    }
}