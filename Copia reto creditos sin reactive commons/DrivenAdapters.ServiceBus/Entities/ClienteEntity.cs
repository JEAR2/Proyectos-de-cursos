using Domain.Model.Entities;

namespace DrivenAdapters.ServiceBus.Entities
{
    public class ClienteEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

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
        /// Créditos
        /// </summary>
        public List<Credito> Creditos { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="correo"></param>
        /// <param name="pais"></param>
        /// <param name="creditos"></param>
        public ClienteEntity(string id, string nombre, string apellido, string correo, string pais, List<Credito> creditos)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Pais = pais;
            Creditos = creditos;
        }
    }
}