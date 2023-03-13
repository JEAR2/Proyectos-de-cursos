using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils.Extensions;
using System;
using System.Threading.Tasks;

namespace Domain.UseCase.Creditos
{
    /// <summary>
    /// Clase CreditoUseCase
    /// </summary>
    public class CreditoUseCase : ICreditoUseCase
    {
        private readonly ICreditoRepository _creditoRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="creditoRepository"></param>
        public CreditoUseCase(ICreditoRepository creditoRepository)
        {
            _creditoRepository = creditoRepository;
        }

        /// <summary>
        /// <see cref="ICreditoRepository.CrearCredito(Credito)"/>
        /// </summary>
        /// <param name="credito"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Credito> CrearCredito(Credito credito)
        {
            if (credito is null)
            {
                throw new BusinessException(TipoExcepcionNegocio.CreditoNoValido.GetDescription(), (int)TipoExcepcionNegocio.CreditoNoValido);
            }

            Credito nuevoCredito = await _creditoRepository.CrearCredito(credito);
            return nuevoCredito;
        }

        /// <summary>
        /// <see cref="ICreditoRepository.ObtenerCreditoPorId(string)"/>
        /// </summary>
        /// <param name="IdCredito"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Credito> ObtenerCreditoPorId(string IdCredito)
        {
            return await _creditoRepository.ObtenerCreditoPorId(IdCredito);
        }

        /// <summary>
        /// <see cref="ICreditoRepository.ActualizarCredito(string, Credito)"/>
        /// </summary>
        /// <param name="IdCredito"></param>
        /// <param name="credito"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Credito> ActualizarCredito(string IdCredito, Credito credito)
        {
            return await _creditoRepository.ActualizarCredito(IdCredito, credito);
        }
    }
}