using Domain.Model.Entities;
using System.Collections.Generic;

namespace EntryPoints.ReactiveWeb.Entities
{
    /// <summary>
    /// Clase ClienteRequest
    /// </summary>
    public class ClienteRequest
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
        /// Correo
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// País
        /// </summary>
        public string Pais { get; set; }

        /// <summary>
        /// créditos
        /// </summary>
        public List<Credito> Creditos { get; set; }

        /// <summary>
        /// Convertir a entidad de dominio Cliente
        /// </summary>
        /// <returns></returns>
        public Cliente AsEntity()
        {
            return new(Nombre, Apellido, Correo, Pais, Creditos);
        }
    }
}