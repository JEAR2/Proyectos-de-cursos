using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo.Repositories
{
    /// <summary>
    /// Clase ClienteRepository
    /// </summary>
    public class ClienteRepository : IClienteRepository
    {
        private readonly IMongoCollection<ClienteEntity> _coleccionCliente;
        private readonly IMongoCollection<CreditoEntity> _coleccionCredito;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public ClienteRepository(IContext context)
        {
            _coleccionCliente = context.Clientes;
            _coleccionCredito = context.Creditos;
        }

        public async Task<Cliente> CrearCliente(Cliente cliente)
        {
            ClienteEntity clienteEntity = new ClienteEntity(cliente.Nombre, cliente.Apellido, cliente.Correo, cliente.Pais, cliente.Creditos);
            await _coleccionCliente.InsertOneAsync(clienteEntity);
            return clienteEntity.AsEntity();
        }

        public async Task<Cliente> ObtenerClientePorId(string IdCliente)
        {
            FilterDefinition<ClienteEntity> filterCliente =
               Builders<ClienteEntity>.Filter.Eq(cliente => cliente.Id, IdCliente);

            ClienteEntity clienteEntity =
                await _coleccionCliente.Find(filterCliente).FirstOrDefaultAsync();

            return clienteEntity?.AsEntity();
        }

        public async Task<List<Cliente>> ObtenerClientes()
        {
            IAsyncCursor<ClienteEntity> clienteEntity =
                await _coleccionCliente.FindAsync(Builders<ClienteEntity>.Filter.Empty);

            List<Cliente> clientes = clienteEntity.ToEnumerable()
                .Select(clienteEntity => clienteEntity.AsEntity()).ToList();
            return clientes;
        }

        public async Task<Cliente> ActualizarCliente(string IdCliente, Cliente cliente)
        {
            ClienteEntity clienteEntity =
                new(IdCliente, cliente.Nombre, cliente.Apellido, cliente.Correo, cliente.Pais, cliente.Creditos);

            var filter = Builders<ClienteEntity>.Filter.Eq(adapter => adapter.Id, IdCliente);
            var result = await _coleccionCliente.ReplaceOneAsync(filter, clienteEntity);
            /* if (result.ModifiedCount == 0)
             {
                 throw new BusinessException("Error al actualizar el empleado", 500);
             }*/

            return clienteEntity.AsEntity();
        }

        /*
        public async Task<Cliente> AsignarCredito(string IdCliente, decimal monto, string concepto, int cuotas, decimal interes)
        {
            FilterDefinition<ClienteEntity> filterCliente =
               Builders<ClienteEntity>.Filter.Eq(cliente => cliente.Id, IdCliente);

            ClienteEntity clienteEntity =
                await _coleccionCliente.Find(filterCliente).FirstOrDefaultAsync();
            CreditoEntity creditoEntity = new(monto, concepto, cuotas, interes);
            clienteEntity.Creditos.Add(creditoEntity.AsEntity());
            await _coleccionCliente.ReplaceOneAsync(IdCliente, clienteEntity);
            return clienteEntity.AsEntity();
        }
        */
    }
}