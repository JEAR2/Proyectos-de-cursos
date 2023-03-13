using Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.UseCase.Clientes
{
    /// <summary>
    /// IClienteUseCase interface
    /// </summary>
    public interface IClienteUseCase
    {
        /// <summary>
        /// Registrar un nuevo Cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        Task<Cliente> CrearCliente(Cliente cliente);

        /// <summary>
        /// Obtener todos los clientes registrados
        /// </summary>
        /// <returns></returns>
        Task<List<Cliente>> ObtenerClientes();

        /// <summary>
        /// Obtener un cliente en especifico
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <returns></returns>
        Task<Cliente> ObtenerClientePorId(string IdCliente);

        /// <summary>
        /// Actualizar un Cliente respecto del Id
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <param name="cliente"></param>
        /// <returns></returns>
        Task<Cliente> ActualizarCliente(string IdCliente, Cliente cliente);

        /// <summary>
        /// Asignar crédito a un cliente
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <param name="monto"></param>
        /// <param name="concepto"></param>
        /// <param name="cuotas"></param>
        /// <returns></returns>
        Task<Cliente> AsignarCredito(string IdCliente, decimal monto, string concepto, int cuotas);

        /// <summary>
        /// Cancelar cuota crédito de un cliente
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <param name="IdCredito"></param>
        /// <returns></returns>
        Task<Cliente> CancelarCuota(string IdCliente, string IdCredito);

        /// <summary>
        /// Obtener los créditos que pertenecen a un cliente en específico
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <returns></returns>
        Task<List<Credito>> ObtenerCreditosCliente(string IdCliente);

        /// <summary>
        /// Obtener los créditos que pertenecen a un cliente en específico
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <returns></returns>
        Task<List<Credito>> ObtenerCreditosPendientesCliente(string IdCliente);
    }
}