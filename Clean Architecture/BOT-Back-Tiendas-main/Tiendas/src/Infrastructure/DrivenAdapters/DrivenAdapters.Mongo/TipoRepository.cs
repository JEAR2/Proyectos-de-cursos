using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo
{
    /// <summary>
    /// <see cref="ITipoRepository"/>
    /// </summary>
    public class TipoRepository : ITipoRepository
    {
        private readonly IMongoCollection<TipoEntity> _coleccionTipos;

        /// <summary>
        /// Initializes a new instance of the <see cref="TiendaRepository"/> class.
        /// </summary>
        /// <param name="mongodb">The mapper.</param>
        public TipoRepository(IContext mongodb)
        {
            _coleccionTipos = mongodb.Tipos;
        }

        /// <summary>
        /// <see cref="ITipoRepository.ObtenerTipoPorIdAsync(int)"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tipo> ObtenerTipoPorIdAsync(int id)
        {
            FilterDefinition<TipoEntity> filterTipo =
                Builders<TipoEntity>.Filter.Eq(tienda => tienda.Id, id);

            TipoEntity tipoEntity =
                await _coleccionTipos.Find(filterTipo).FirstOrDefaultAsync();

            return tipoEntity?.AsEntity();
        }
    }
}