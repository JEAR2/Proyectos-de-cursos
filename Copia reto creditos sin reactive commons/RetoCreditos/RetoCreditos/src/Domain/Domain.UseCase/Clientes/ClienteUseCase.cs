using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils;
using Helpers.ObjectsUtils.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.UseCase.Clientes
{
    /// <summary>
    /// Clase ClienteUseCase
    /// </summary>
    public class ClienteUseCase : IClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ICreditoRepository _creditoRepository;
        private readonly IOptions<ConfiguradorAppSettings> _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="clienteRepository"></param>
        /// <param name="creditoRepository"></param>
        public ClienteUseCase(IClienteRepository clienteRepository, ICreditoRepository creditoRepository, IOptions<ConfiguradorAppSettings> configuration)
        {
            _clienteRepository = clienteRepository;
            _creditoRepository = creditoRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Método para crear un cliente
        /// <see cref="IClienteRepository.CrearCliente(Cliente)"/>
        /// <param name="cliente"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Cliente> CrearCliente(Cliente cliente)
        {
            if (cliente is null)
            {
                throw new BusinessException(TipoExcepcionNegocio.ClienteNoValido.GetDescription(), (int)TipoExcepcionNegocio.ClienteNoValido);
            }
            return _clienteRepository.CrearCliente(cliente);
        }

        /// <summary>
        /// <see cref="IClienteRepository.ObtenerClientePorId(string)"/>
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Cliente> ObtenerClientePorId(string IdCliente)
        {
            return _clienteRepository.ObtenerClientePorId(IdCliente);
        }

        /// <summary>
        /// <see cref="IClienteRepository.ObtenerClientes()"/>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<List<Cliente>> ObtenerClientes()
        {
            return _clienteRepository.ObtenerClientes();
        }

        /// <summary>
        /// <see cref="IClienteRepository.ActualizarCliente(string, Cliente)"/>
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <param name="cliente"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Cliente> ActualizarCliente(string IdCliente, Cliente cliente)
        {
            return await _clienteRepository.ActualizarCliente(IdCliente, cliente);
        }

        /// <summary>
        /// <see cref="IClienteUseCase.AsignarCredito(string, decimal, string, int)"/>
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <param name="monto"></param>
        /// <param name="concepto"></param>
        /// <param name="cuotas"></param>
        /// <returns></returns>
        public async Task<Cliente> AsignarCredito(string IdCliente, decimal monto, string concepto, int cuotas)
        {
            decimal interes = _configuration.Value.Interes;
            // decimal interes = (decimal)1.5;
            Cliente cliente = await _clienteRepository.ObtenerClientePorId(IdCliente);
            Credito credito = await _creditoRepository.CrearCredito(new Credito(monto, concepto, cuotas, interes));
            cliente.AgregarCredito(credito);
            await _clienteRepository.ActualizarCliente(IdCliente, cliente);
            return cliente;
        }

        /// <summary>
        /// <see cref="IClienteUseCase.CancelarCuota(string, string)"/>
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <param name="IdCredito"></param>
        /// <returns></returns>
        public async Task<Cliente> CancelarCuota(string IdCliente, string IdCredito)
        {
            Cliente cliente = await _clienteRepository.ObtenerClientePorId(IdCliente);
            cliente.Creditos.Find(credito => credito.Id.Equals(IdCredito)).CancelarCuota();
            Credito credito = cliente.Creditos.Find(credito => credito.Id == IdCredito);
            Cliente clienteActualizar = new Cliente(IdCliente, cliente.Nombre, cliente.Apellido, cliente.Correo, cliente.Pais, cliente.Creditos);

            Credito creditoActualizar = new Credito(IdCredito, credito.Concepto, credito.Monto, credito.Cuotas, credito.ValorCuota, credito.CuotasPagadas, credito.CuotasPendientes, credito.Saldo, credito.FechaInicio, credito.FechaFin, credito.FechaProximaCuota);

            await _creditoRepository.ActualizarCredito(IdCredito, creditoActualizar);
            await _clienteRepository.ActualizarCliente(IdCliente, clienteActualizar);
            return cliente;
        }

        /// <summary>
        /// <see cref="IClienteUseCase.ObtenerCreditosCliente(string)"/>
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <returns></returns>
        public async Task<List<Credito>> ObtenerCreditosCliente(string IdCliente)
        {
            Cliente cliente = await _clienteRepository.ObtenerClientePorId(IdCliente);
            return cliente.Creditos.ToList();
        }

        /// <summary>
        /// <see cref="IClienteUseCase.ObtenerCreditosPendientesCliente(string)"/>
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <returns></returns>
        public async Task<List<Credito>> ObtenerCreditosPendientesCliente(string IdCliente)
        {
            Cliente cliente = await _clienteRepository.ObtenerClientePorId(IdCliente);
            List<Credito> creditos = cliente.Creditos.FindAll(credito => credito.CuotasPendientes != 0);
            return creditos;
        }
    }
}