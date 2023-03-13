namespace DrivenAdapter.ServicesBus.Entities
{
    /// <summary>
    /// Tienda DTO
    /// </summary>
    public class TiendaEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Valor Nombre
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Tipo
        /// </summary>
        public int IdTipo { get; set; }

        /// <summary>
        /// Dirección
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Ubicación
        /// </summary>
        public UbicacionEntity Ubicacion { get; set; }

        /// <summary>
        /// Valor Numero del Celular
        /// </summary>
        public string Celular { get; set; }

        /// <summary>
        /// Valor de el Correo
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="idTipo"></param>
        /// <param name="direccion"></param>
        /// <param name="ubicacion"></param>
        /// <param name="celular"></param>
        /// <param name="correo"></param>
        /// <param name="fechaCreacion"></param>
        public TiendaEntity(string id, string nombre, int idTipo, string direccion, UbicacionEntity ubicacion, string celular,
            string correo)
        {
            Id = id;
            Nombre = nombre;
            IdTipo = idTipo;
            Direccion = direccion;
            Ubicacion = ubicacion;
            Celular = celular;
            Correo = correo;
        }
    }
}