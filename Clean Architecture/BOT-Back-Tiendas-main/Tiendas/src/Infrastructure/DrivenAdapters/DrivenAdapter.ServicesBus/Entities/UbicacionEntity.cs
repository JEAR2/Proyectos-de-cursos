using Domain.Model.Entities;
using Helpers.ObjectsUtils;

namespace DrivenAdapter.ServicesBus.Entities
{
    /// <summary>
    /// Ubicación Entity
    /// </summary>
    public class UbicacionEntity : IDomainEntity<Ubicacion>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="latitud"></param>
        /// <param name="longitud"></param>
        public UbicacionEntity(decimal latitud, decimal longitud)
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

        /// <summary>
        /// Convertir a entidad de dominio
        /// </summary>
        /// <returns></returns>
        public Ubicacion AsEntity() =>
            new(Latitud, Longitud);
    }
}