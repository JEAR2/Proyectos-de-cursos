using Domain.Model.Entities;

namespace EntryPoints.ReactiveWeb.Entity
{
    /// <summary>
    /// Almacén request DTO
    /// </summary>
    public class TiendaRequest
    {
        /// <summary>
        /// valor Nombre
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// valor TipoId
        /// </summary>
        public int TipoId { get; set; }

        /// <summary>
        /// valor AliadoId
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// valor Celular
        /// </summary>
        public string Celular { get; set; }

        /// <summary>
        /// valor Correo
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// valor Ubicación
        /// </summary>
        public UbicacionRequest Ubicacion { get; set; }

        /// <summary>
        /// Convertir DTO a Entidad del dominio
        /// </summary>
        /// <returns></returns>
        public Tienda AsEntity() =>
            new(Nombre, TipoId, Direccion, Ubicacion.AsEntity(), Celular, Correo);
    }
}