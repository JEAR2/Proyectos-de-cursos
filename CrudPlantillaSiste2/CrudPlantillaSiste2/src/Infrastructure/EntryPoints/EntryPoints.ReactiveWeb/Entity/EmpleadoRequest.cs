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
        public string Nombre { get; set; }

        /// <summary>
        /// Apellido
        /// </summary>
        public string Apellido { get; set; }

        /// <summary>
        /// Edad
        /// </summary>
        public int Edad { get; set; }

        /// <summary>
        /// Correo
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Sexo
        /// </summary>
        public string Sexo { get; set; }

        /// <summary>
        /// Departamento
        /// </summary>
        public int DepartamentoId { get; set; }

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