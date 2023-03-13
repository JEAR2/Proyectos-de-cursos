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
    public class CreditoRepositoryTest
    {
        private readonly Mock<IContext> _mockContext;
        private readonly Mock<IMongoCollection<CreditoEntity>> _mockColeccionCreditos;
        private readonly Mock<IAsyncCursor<CreditoEntity>> _mockCreditoCursor;

        public CreditoRepositoryTest()
        {
            _mockContext = new();
            _mockColeccionCreditos = new();
            _mockCreditoCursor = new();

            _mockColeccionCreditos.Object.InsertMany(ObtenerCreditosTest());
            _mockCreditoCursor.SetupSequence(item => item.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true).Returns(false);

            _mockCreditoCursor.SetupSequence(item => item.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true)).Returns(Task.FromResult(false));
        }

        [Theory]
        [InlineData("john", 10000, 5, 1.5)]
        [InlineData("Edward", 20000, 8, 1.5)]
        public async Task Credito_Repository_Crear_Credito_Retorna_Credito_Creado(string concepto, decimal monto, int coutas, decimal interes)
        {
            _mockColeccionCreditos.Setup(op => op.InsertOneAsync(It.IsAny<CreditoEntity>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()));

            _mockContext.Setup(context => context.Creditos).Returns(_mockColeccionCreditos.Object);

            var creditoRepository = new CreditoRepository(_mockContext.Object);

            Credito result = await creditoRepository.CrearCredito(ObtenerCreditoTest(concepto, monto, coutas, interes));

            Assert.NotNull(result);
            Assert.Equal(concepto, result.Concepto);
            Assert.IsType<Credito>(result);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public async Task Credito_Repository_Obtener_Credito_Por_Id_Retorna_Credito_Encontrado(string idCredito)
        {
            List<CreditoEntity> listaCreditos = new() { ObtenerCreditoEntityTest() };
            _mockCreditoCursor.Setup(item => item.Current).Returns(listaCreditos);

            _mockColeccionCreditos.Setup(op => op.FindAsync(It.IsAny<FilterDefinition<CreditoEntity>>(),
                It.IsAny<FindOptions<CreditoEntity, CreditoEntity>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_mockCreditoCursor.Object);

            _mockContext.Setup(context => context.Creditos).Returns(_mockColeccionCreditos.Object);

            var creditoRepository = new CreditoRepository(_mockContext.Object);

            var result = await creditoRepository.ObtenerCreditoPorId(idCredito);

            Assert.NotNull(result);
            Assert.IsType<Credito>(result);
        }

        [Fact]
        public async Task Credito_Repository_Actualizar_Credito_Retorna_Credito_Actualizado()
        {
            string idCredito = "1";
            _mockColeccionCreditos.Setup(op => op.ReplaceOneAsync(It.IsAny<FilterDefinition<CreditoEntity>>(),
              It.IsAny<CreditoEntity>(), It.IsAny<ReplaceOptions>(), It.IsAny<CancellationToken>()));

            _mockContext.Setup(context => context.Creditos).Returns(_mockColeccionCreditos.Object);

            var creditoRepository = new CreditoRepository(_mockContext.Object);
            var result = await creditoRepository.ActualizarCredito(idCredito, ObtenerCreditoTest("concepto", 5000, 5, (decimal)1.5));

            Assert.NotNull(result);
        }

        private List<CreditoEntity> ObtenerCreditosTest() => new()
        {
            new CreditoEntityBuilderTest()
            .ConId("1")
            .ConConcepto("concepto")
            .ConMonto(25000)
            .ConCuotas(4)
            .ConInteres((decimal)1.5)
            .Build(),

            new CreditoEntityBuilderTest()
            .ConId("1")
            .ConConcepto("concepto")
            .ConMonto(25000)
            .ConCuotas(4)
            .ConInteres((decimal)1.5)
            .Build()
        };

        private Credito ObtenerCreditoTest(string concepto, decimal monto, int cuotas, decimal interes)
        {
            return new CreditoBuilderTest()
                .ConId("1")
                .ConConcepto(concepto)
                .ConMonto(monto)
                .ConCuotas(cuotas)
                .ConInteres(interes)
                .Build();
        }

        private CreditoEntity ObtenerCreditoEntityTest()
        {
            return new CreditoEntityBuilderTest()
                .ConId("1")
                .ConConcepto("concepto")
                .ConMonto(50000)
                .ConCuotas(5)
                .ConInteres((decimal)1.5)
                .Build();
        }
    }
}

/*
        [Theory]
        [InlineData("John", "Acevedo", "john@correo.com", "Co")]
        [InlineData("Edward", "Rojas", "edward@correo.com", "Co")]
        public async Task Credito_Repository_Crear_Credito_Retorna_Credito_Creado(string nombre, string apellido, string correo, string pais)
        {
            _mockColeccionClientes.Setup(op => op.InsertOneAsync(It.IsAny<ClienteEntity>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()));

            _mockContext.Setup(context => context.Creditos).Returns(_mockColeccionClientes.Object);

            var clienteRepository = new ClienteRepository(_mockContext.Object);

            Credito result = await clienteRepository.CrearCliente(ObtenerClienteTest());

            Assert.NotNull(result);
            Assert.Equal(correo, result.Correo);
            Assert.IsType<Cliente>(result);
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

        private List<ClienteEntity> ObtenerClienteEntityTest() => new()
        {
            new ClienteEntityBuilderTest()
            .ConId("1")
            .ConNombre("john")
            .ConCorreo("john@correo.com")
            .ConPais("CO")
            .ConCreditos(new List<Credito>())
            .Build()
        };

        private List<Cliente> ObtenerClienteTest() => new()
        {
            new ClienteBuilderTest()
            .ConId("1")
            .ConNombre("john")
            .ConCorreo("john@correo.com")
            .ConPais("CO")
            .ConCreditos(new List<Credito>())
            .Build()
        };*/