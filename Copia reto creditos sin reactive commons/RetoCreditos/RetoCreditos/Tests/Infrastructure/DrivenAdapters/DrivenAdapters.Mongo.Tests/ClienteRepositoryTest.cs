using Domain.Model.Entities;
using Domain.Model.Tests;
using DrivenAdapters.Mongo.Entities;
using DrivenAdapters.Mongo.Repositories;
using DrivenAdapters.Mongo.Tests.Entities;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace DrivenAdapters.Mongo.Tests
{
    public class ClienteRepositoryTest
    {
        private readonly Mock<IContext> _mockContext;
        private readonly Mock<IMongoCollection<ClienteEntity>> _mockColeccionClientes;
        private readonly Mock<IAsyncCursor<ClienteEntity>> _mockClienteCursor;

        public ClienteRepositoryTest()
        {
            _mockContext = new();
            _mockColeccionClientes = new();
            _mockClienteCursor = new();
            _mockColeccionClientes.Object.InsertMany(ObtenerClientesTest());
            _mockClienteCursor.SetupSequence(item => item.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true).Returns(false);
            _mockClienteCursor.SetupSequence(item => item.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true)).Returns(Task.FromResult(false));
        }

        //[Theory]
        //[InlineData("John", "Acevedo", "john@correo.com", "Co")]
        //[InlineData("Edward", "Rojas", "edward@correo.com", "Co")]
        [Fact]
        public async Task Cliente_Repository_Crear_Cliente_Retorna_Cliente_Creado()
        {
            _mockColeccionClientes.Setup(op => op.InsertOneAsync(It.IsAny<ClienteEntity>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()));

            _mockContext.Setup(context => context.Clientes).Returns(_mockColeccionClientes.Object);

            var clienteRepository = new ClienteRepository(_mockContext.Object);

            var result = await clienteRepository.CrearCliente(ObtenerClienteTest());

            Assert.NotNull(result);
            Assert.Equal("john@correo.com", result.Correo);
            Assert.IsType<Cliente>(result);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public async Task Cliente_Repository_Obtener_Cliente_Por_Id_Retorna_Cliente_Encontrado(string idCliente)
        {
            List<ClienteEntity> listaClientes = new() { ObtenerClienteEntityTest() };
            _mockClienteCursor.Setup(item => item.Current).Returns(listaClientes);

            _mockColeccionClientes.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<ClienteEntity>>(),
                It.IsAny<FindOptions<ClienteEntity, ClienteEntity>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_mockClienteCursor.Object);

            _mockContext.Setup(context => context.Clientes).Returns(_mockColeccionClientes.Object);

            var clienteRepository = new ClienteRepository(_mockContext.Object);

            var result = await clienteRepository.ObtenerClientePorId(idCliente);

            Assert.NotNull(result);
            Assert.IsType<Cliente>(result);
        }

        [Fact]
        public async Task Cliente_Repository_Obtener_Clientes_Retorna_Lista_De_Clientes()
        {
            List<ClienteEntity> listaClientes = new() { ObtenerClienteEntityTest() };
            _mockClienteCursor.Setup(item => item.Current).Returns(listaClientes);

            _mockColeccionClientes.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<ClienteEntity>>(),
                It.IsAny<FindOptions<ClienteEntity, ClienteEntity>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_mockClienteCursor.Object);

            _mockContext.Setup(context => context.Clientes).Returns(_mockColeccionClientes.Object);

            var clienteRepository = new ClienteRepository(_mockContext.Object);

            var result = await clienteRepository.ObtenerClientes();

            Assert.NotNull(result);
            Assert.IsType<List<Cliente>>(result);
        }

        [Fact]
        public async Task Cliente_Repository_Obtener_Clientes_Retorna_Lista_Vacia()
        {
            _mockColeccionClientes.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<ClienteEntity>>(),
                It.IsAny<FindOptions<ClienteEntity, ClienteEntity>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_mockClienteCursor.Object);

            _mockContext.Setup(context => context.Clientes).Returns(_mockColeccionClientes.Object);

            var clienteRepository = new ClienteRepository(_mockContext.Object);

            var result = await clienteRepository.ObtenerClientes();

            Assert.Empty(result);
            Assert.IsType<List<Cliente>>(result);
        }

        [Fact]
        public async Task Cliente_Repository_Actualizar_Cliente_Retorna_Cliente_Actualizado()
        {
            string idCliente = "1";
            _mockColeccionClientes.Setup(op => op.ReplaceOneAsync(It.IsAny<FilterDefinition<ClienteEntity>>(),
              It.IsAny<ClienteEntity>(), It.IsAny<ReplaceOptions>(), It.IsAny<CancellationToken>()));

            _mockContext.Setup(context => context.Clientes).Returns(_mockColeccionClientes.Object);

            var clienteRepository = new ClienteRepository(_mockContext.Object);
            var result = await clienteRepository.ActualizarCliente(idCliente, ObtenerClienteTest());

            Assert.NotNull(result);
        }

        private List<ClienteEntity> ObtenerClientesTest() => new()
        {
            new ClienteEntityBuilderTest()
            .ConId("1")
            .ConNombre("john")
            .ConCorreo("john@correo.com")
            .ConPais("CO")
            .ConCreditos(new List<Credito>())
            .Build(),
            new ClienteEntityBuilderTest()
            .ConId("2")
            .ConNombre("edward")
            .ConCorreo("edward@correo.com")
            .ConPais("CO")
            .ConCreditos(new List<Credito>())
            .Build(),
        };

        private Cliente ObtenerClienteTest()
        {
            return new ClienteBuilderTest()
             .ConId("1")
             .ConNombre("john")
             .ConCorreo("john@correo.com")
             .ConPais("CO")
             .ConCreditos(new List<Credito>())
             .Build();
        }

        private ClienteEntity ObtenerClienteEntityTest()
        {
            return new ClienteEntityBuilderTest()
             .ConId("1")
             .ConNombre("john")
             .ConCorreo("john@correo.com")
             .ConPais("CO")
             .ConCreditos(new List<Credito>())
             .Build();
        }
    }
}