using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo
{
    /// <summary>
    /// EntityAdapter
    /// </summary>
    public class TiendaRepository : ITiendaRepository
    {
        private readonly IMongoCollection<TiendaEntity> _coleccionTiendas;

        /// <summary>
        /// Initializes a new instance of the <see cref="TiendaRepository"/> class.
        /// </summary>
        /// <param name="mongodb">The mapper.</param>
        public TiendaRepository(IContext mongodb)
        {
            _coleccionTiendas = mongodb.Tiendas;
        }

        /// <summary>
        /// <see cref="ITiendaRepository.InsertarTiendaAsync(Tienda)"/>
        /// </summary>
        /// <param name="crearAlmacen"></param>
        /// <returns></returns>
        public async Task<Tienda> InsertarTiendaAsync(Tienda crearAlmacen)
        {
            TiendaEntity nuevaTienda =
                new(crearAlmacen.Nombre, tipo: new TipoEntity(crearAlmacen.Tipo?.Id ?? 0, crearAlmacen.Tipo?.Nombre),
                crearAlmacen.Direccion, ubicacion: new UbicacionEntity(crearAlmacen.Ubicacion.Latitud, crearAlmacen.Ubicacion.Longitud),
                crearAlmacen.Celular, crearAlmacen.Correo, crearAlmacen.FechaCreacion);

            await _coleccionTiendas.InsertOneAsync(nuevaTienda);

            return nuevaTienda.AsEntity();
        }

        /// <summary>
        /// Obtener todas las tiendas
        /// </summary>
        /// <returns></returns>
        public async Task<List<Tienda>> ObtenerTiendasAsync()
        {
            IAsyncCursor<TiendaEntity> tiendasEntity =
                await _coleccionTiendas.FindAsync(Builders<TiendaEntity>.Filter.Empty);

            List<Tienda> tiendas = tiendasEntity.ToEnumerable()
                .Select(tiendaEntity => tiendaEntity.AsEntity()).ToList();

            return tiendas;
        }
    }
}