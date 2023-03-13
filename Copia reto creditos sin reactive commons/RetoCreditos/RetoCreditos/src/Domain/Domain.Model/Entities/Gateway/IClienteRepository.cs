using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// IClienteRepository interface
    /// </summary>
    public interface IClienteRepository
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
        //Task<Cliente> AsignarCredito(string IdCliente, decimal monto, string concepto, int cuotas, decimal interes);
    }
}