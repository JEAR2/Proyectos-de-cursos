using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace DrivenAdapters.Mongo.Entities
{
    /// <summary>
    /// Empleado DTO
    /// </summary>
    public class EmpleadoEntity : EntityBase, IDomainEntity<Empleado>
    {
        /// <summary>
        /// Nombre
        /// </summary>
        ///
        [BsonElement("nombre")]
        public string Nombre { get; set; }

        /// <summary>
        /// Apellido
        /// </summary>
        ///
        [BsonElement("apellido")]
        public string Apellido { get; set; }

        /// <summary>
        /// Edad
        /// </summary>
        ///
        [BsonElement("edad")]
        public int Edad { get; set; }

        /// <summary>
        /// Correo
        /// </summary>
        ///
        [BsonElement("correo")]
        public string Correo { get; set; }

        /// <summary>
        /// Sexo
        /// </summary>
        ///
        [BsonElement("sexo")]
        public string Sexo { get; set; }

        /// <summary>
        /// Departamento
        /// </summary>
        ///
        [BsonElement("departamento")]
        public DepartamentoEntity Departamento { get; set; }

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
        public EmpleadoEntity(string id, string nombre, string apellido, int edad, string correo, string sexo, DepartamentoEntity departamento)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Correo = correo;
            Sexo = sexo;
            Departamento = departamento;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="edad"></param>
        /// <param name="correo"></param>
        /// <param name="sexo"></param>
        /// <param name="departamento"></param>
        public EmpleadoEntity(string nombre, string apellido, int edad, string correo, string sexo, DepartamentoEntity departamento)
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
        public Empleado AsEntity() => new(Id, Nombre, Apellido, Edad, Correo, Sexo, Departamento.AsEntity());
    }
}