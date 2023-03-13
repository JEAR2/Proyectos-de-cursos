using Domain.Model.Entities;

namespace EntryPoints.ReactiveWeb.Entity
{
    /// <summary>
    /// Ubicación
    /// </summary>
    public class UbicacionRequest
    {
        /// <summary>
        /// Latitud
        /// </summary>
        public decimal Latitud { get; set; }

        /// <summary>
        /// Longitud
        /// </summary>
        public decimal Longitud { get; set; }

        /// <summary>
        /// Convertir DTO a Entidad del dominio
        /// </summary>
        /// <returns></returns>
        public Ubicacion AsEntity() =>
            new(Latitud, Longitud);
    }
}