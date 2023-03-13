using Domain.Model.Entities;
using System.Threading.Tasks;

namespace Domain.UseCase.Creditos
{
    /// <summary>
    /// ICreditoUseCase interface
    /// </summary>
    public interface ICreditoUseCase
    {
        /// <summary>
        /// Registrar un nuevo crédito
        /// </summary>
        /// <param name="credito"></param>
        /// <returns></returns>
        Task<Credito> CrearCredito(Credito credito);

        /// <summary>
        /// Obtener un crédito en especifico
        /// </summary>
        /// <param name="IdCredito"></param>
        /// <returns></returns>
        Task<Credito> ObtenerCreditoPorId(string IdCredito);

        /// <summary>
        /// Actualizar crédito respecto al Id
        /// </summary>
        /// <param name="IdCredito"></param>
        /// <param name="credito"></param>
        /// <returns></returns>
        Task<Credito> ActualizarCredito(string IdCredito, Credito credito);
    }
}