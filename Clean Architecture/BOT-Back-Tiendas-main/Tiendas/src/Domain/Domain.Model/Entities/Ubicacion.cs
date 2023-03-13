namespace Domain.Model.Entities
{
    /// <summary>
    /// Ubicación
    /// </summary>
    public class Ubicacion
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="latitud"></param>
        /// <param name="longitud"></param>
        public Ubicacion(decimal latitud, decimal longitud)
        {
            Latitud = latitud;
            Longitud = longitud;
        }

        /// <summary>
        /// Latitud
        /// </summary>
        public decimal Latitud { get; private set; }

        /// <summary>
        /// Longitud
        /// </summary>
        public decimal Longitud { get; private set; }
    }
}