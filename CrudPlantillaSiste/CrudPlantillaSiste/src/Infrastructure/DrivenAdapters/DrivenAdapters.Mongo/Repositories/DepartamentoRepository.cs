using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo.Repositories
{
    /// <summary>
    ///  <see cref="IDepartamentoRepository"/> .
    /// </summary>
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly IMongoCollection<DepartamentoEntity> _coleccionDepartamentos;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartamentoRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public DepartamentoRepository(IContext context)
        {
            _coleccionDepartamentos = context.Departamentos;
        }

        /// <summary>
        /// <see cref="IDepartamentoRepository.ObtenerDepartamentoPorIdAsync(int)"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Departamento> ObtenerDepartamentoPorIdAsync(int id)
        {
            FilterDefinition<DepartamentoEntity> filterDepartamento =
                Builders<DepartamentoEntity>.Filter.Eq(departamento => departamento.Id, id);

            DepartamentoEntity departamentoEntity =
                await _coleccionDepartamentos.Find(filterDepartamento).FirstOrDefaultAsync();

            return departamentoEntity?.AsEntity();
        }
    }
}