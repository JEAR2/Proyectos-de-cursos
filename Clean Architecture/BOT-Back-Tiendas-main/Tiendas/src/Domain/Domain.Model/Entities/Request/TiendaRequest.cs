namespace Domain.Model.Entities.Request
{
    /// <summary>
    /// Tienda DTO
    /// </summary>
    public class TiendaRequest
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
        public UbicacionRequest Ubicacion { get; set; }

        /// <summary>
        /// Valor Numero del Celular
        /// </summary>
        public string Celular { get; set; }

        /// <summary>
        /// Valor de el Correo
        /// </summary>
        public string Correo { get; set; }
    }
}