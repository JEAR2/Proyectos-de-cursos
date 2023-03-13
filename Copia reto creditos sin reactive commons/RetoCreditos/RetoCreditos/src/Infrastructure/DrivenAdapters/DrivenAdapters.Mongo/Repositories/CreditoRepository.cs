using credinet.comun.models.Credits;
using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo.Repositories
{
    /// <summary>
    /// Clase CreditoRepository
    /// </summary>
    public class CreditoRepository : ICreditoRepository
    {
        private readonly IMongoCollection<CreditoEntity> _coleccionCreditos;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public CreditoRepository(IContext context)
        {
            _coleccionCreditos = context.Creditos;
        }

        /// <summary>
        /// <see cref="ICreditoRepository.CrearCredito(Credito)"/>
        /// </summary>
        /// <param name="credito"></param>
        /// <returns></returns>
        public async Task<Credito> CrearCredito(Credito credito)
        {
            CreditoEntity creditoEntity = new CreditoEntity(credito.Monto, credito.Concepto, credito.Cuotas, credito.Interes);
            await _coleccionCreditos.InsertOneAsync(creditoEntity);
            return creditoEntity.AsEntity();
        }

        /// <summary>
        /// <see cref="ICreditoRepository.ObtenerCreditoPorId(string)"/>
        /// </summary>
        /// <param name="IdCredito"></param>
        /// <returns></returns>
        public async Task<Credito> ObtenerCreditoPorId(string IdCredito)
        {
            FilterDefinition<CreditoEntity> filterCredito =
                Builders<CreditoEntity>.Filter.Eq(credito => credito.Id, IdCredito);
            CreditoEntity creditoEntity = await _coleccionCreditos.Find(filterCredito).FirstOrDefaultAsync();

            return creditoEntity.AsEntity();
        }

        /// <summary>
        /// <see cref="ICreditoRepository.ActualizarCredito(string, Credito)"/>
        /// </summary>
        /// <param name="IdCredito"></param>
        /// <param name="credito"></param>
        /// <returns></returns>
        public async Task<Credito> ActualizarCredito(string IdCredito, Credito credito)
        {
            /*UpdateDefinition<CreditoEntity> creditoEntity = Builders<CreditoEntity>.Update
                .Set(dato => dato.Concepto, credito.Concepto)
                .Set(dato => dato.Monto, credito.Monto)
                .Set(dato => dato.Cuotas, credito.Cuotas)
                .Set(dato => dato.ValorCuota, credito.ValorCuota)
                .Set(dato => dato.CuotasPagadas, credito.CuotasPagadas)
                .Set(dato => dato.CuotasPendientes, credito.CuotasPendientes)
                .Set(dato => dato.Saldo, credito.Saldo)
                .Set(dato => dato.FechaInicio, credito.FechaInicio)
                .Set(dato => dato.FechaFin, credito.FechaFin)
                .Set(dato => dato.FechaProximaCuota, credito.FechaProximaCuota);*/
            CreditoEntity creditoEntity = new CreditoEntity(IdCredito, credito.Concepto, credito.Monto, credito.Cuotas, credito.ValorCuota,
            credito.CuotasPagadas, credito.CuotasPendientes, credito.Saldo, credito.FechaInicio, credito.FechaFin, credito.FechaProximaCuota);
            FilterDefinition<CreditoEntity> filter = Builders<CreditoEntity>.Filter.Eq(adapter => adapter.Id, IdCredito);
            var result = await _coleccionCreditos.ReplaceOneAsync(filter, creditoEntity);
            /* if (result.ModifiedCount == 0)
             {
                 throw new BusinessException("no se pudo actualizar el crédito", 500);
             }*/
            return creditoEntity.AsEntity();
        }
    }
}