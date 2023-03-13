namespace Domain.Model.Entities.Request
{
    /// <summary>
    /// Ubicación Entity
    /// </summary>
    public class UbicacionRequest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="latitud"></param>
        /// <param name="longitud"></param>
        public UbicacionRequest(decimal latitud, decimal longitud)
        {
            Latitud = latitud;
            Longitud = longitud;
        }

        /// <summary>
        /// Latitud
        /// </summary>
        public decimal Latitud { get; set; }

        /// <summary>
        /// Longitud
        /// </summary>
        public decimal Longitud { get; set; }
    }
}