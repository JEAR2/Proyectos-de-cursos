using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities
{
    /// <summary>
    /// Clase Cliente
    /// </summary>
    public class Cliente
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; private set; }

        /// <summary>
        /// Apellido
        /// </summary>
        public string Apellido { get; private set; }

        /// <summary>
        /// Correo
        /// </summary>
        public string Correo { get; private set; }

        /// <summary>
        /// País
        /// </summary>
        public string Pais { get; private set; }

        /// <summary>
        /// Créditos
        /// </summary>
        public List<Credito> Creditos { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="correo"></param>
        /// <param name="pais"></param>
        /// <param name="creditos"></param>
        public Cliente(string id, string nombre, string apellido, string correo, string pais, List<Credito> creditos)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Pais = pais;
            Creditos = creditos;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="correo"></param>
        /// <param name="pais"></param>
        /// <param name="creditos"></param>
        public Cliente(string nombre, string apellido, string correo, string pais, List<Credito> creditos)
        {
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Pais = pais;
            Creditos = creditos;
        }

        public void AgregarCredito(Credito credito) => Creditos.Add(credito);
    }
}