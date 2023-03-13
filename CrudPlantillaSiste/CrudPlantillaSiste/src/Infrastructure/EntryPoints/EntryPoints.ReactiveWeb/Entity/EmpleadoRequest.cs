using Domain.Model.Entities;

namespace EntryPoints.ReactiveWeb.Entity
{
    /// <summary>
    /// Clase EmpleadoRequest
    /// </summary>
    public class EmpleadoRequest
    {
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
        public int DepartamentoId { get; private set; }

        /// <summary>
        /// Convertir a entidad de dominio Empleado
        /// </summary>
        /// <returns></returns>
        public Empleado AsEntity()
        {
            return new(Nombre, Apellido, Edad, Correo, Sexo, DepartamentoId);
        }
    }
}