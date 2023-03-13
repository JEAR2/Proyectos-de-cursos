using credinet.comun.models.Credits;
using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using Domain.Model.Tests;
using Domain.UseCase.Clientes;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Threading;
using Xunit;

namespace Domain.UseCase.Tests
{
    public class ClienteUseCaseTest
    {
        private readonly Mock<IClienteRepository> _mockClienteRepository;
        private readonly Mock<ICreditoRepository> _mockCreditoRepository;
        private readonly Mock<IOptions<ConfiguradorAppSettings>> _mockConfiguration;
        private readonly ClienteUseCase _clienteUseCase;

        public ClienteUseCaseTest()
        {
            _mockClienteRepository = new();
            _mockCreditoRepository = new();
            _mockConfiguration = new();
            _clienteUseCase = new(_mockClienteRepository.Object, _mockCreditoRepository.Object, _mockConfiguration.Object);
        }

        [Theory]
        [InlineData("John", "Correo", null)]
        [InlineData("Edward", "Correo2", null)]
        public async Task Cliente_Use_Case_Crear_Cliente_Retorna_Cliente_Creado(string nombre, string correo, List<Credito> creditos)
        {
            Cliente cliente = new ClienteBuilderTest()
                .ConNombre(nombre)
                .ConCorreo(correo)
                .ConCreditos(creditos)
                .Build();

            _mockClienteRepository.Setup(repository => repository.CrearCliente(It.IsAny<Cliente>()))
                .ReturnsAsync(ObtenerClienteTest(nombre, correo, creditos));

            Cliente clienteCreado = await _clienteUseCase.CrearCliente(cliente);

            _mockClienteRepository.Verify(repository => repository.CrearCliente(It.IsAny<Cliente>()), Times.Once());
            Assert.NotNull(clienteCreado);
            Assert.NotNull(clienteCreado.Id);
            Assert.Equal(nombre, clienteCreado.Nombre);
        }

        [Fact]
        public async Task Cliente_Use_Case_Crear_Cliente_Retorna_Excepcion()
        {
            BusinessException excepcion = await Assert.ThrowsAsync<BusinessException>(async () => await _clienteUseCase.CrearCliente(null));

            _mockClienteRepository.Verify(repository => repository.CrearCliente(It.IsAny<Cliente>()), Times.Never());

            Assert.Equal((int)TipoExcepcionNegocio.ClienteNoValido, excepcion.code);
        }

        [Fact]
        public async Task Cliente_Use_Case_Obtener_Cliente_Por_Id_Retorna_Cliente_Encontrado()
        {
            string nombre = "compra celular";
            string correo = "correo";
            List<Credito> creditos = new List<Credito>();

            _mockClienteRepository.Setup(repository => repository.ObtenerClientePorId(It.IsAny<string>()))
                .ReturnsAsync(ObtenerClienteTest(nombre, correo, creditos));

            Cliente clienteEncontrado = await _clienteUseCase.ObtenerClientePorId(It.IsAny<string>());

            _mockClienteRepository.Verify(repository => repository.ObtenerClientePorId(It.IsAny<string>()), Times.Once);

            Assert.NotNull(clienteEncontrado);
            Assert.Equal(correo, clienteEncontrado.Correo);
        }

        [Fact]
        public async Task Cliente_Use_Case_Obtener_Clientes_Retorna_lista_De_Clientes()
        {
            _mockClienteRepository.Setup(repository => repository.ObtenerClientes())
                .ReturnsAsync(ObtenerClientesTest);

            List<Cliente> clientes = await _clienteUseCase.ObtenerClientes();

            _mockClienteRepository.Verify(repository => repository.ObtenerClientes(), Times.Once);

            Assert.NotNull(clientes);
        }

        [Fact]
        public async Task Cliente_Use_Case_Actualizar_Cliente_Retorna_Cliente_Actualizado()
        {
            string idCliente = "1";
            Cliente cliente = new ClienteBuilderTest()
                .ConId(idCliente)
                .ConNombre("JOhn")
                .ConCorreo("John@correo.com")
                .ConCreditos(new List<Credito>())
                .Build();
            _mockClienteRepository.Setup(repository => repository.ActualizarCliente(It.IsAny<string>(), It.IsAny<Cliente>()))
                .ReturnsAsync(cliente);

            Cliente clienteActualizado = await _clienteUseCase.ActualizarCliente(idCliente, cliente);

            _mockClienteRepository.Verify(repository => repository.ActualizarCliente(It.IsAny<string>(), It.IsAny<Cliente>()), Times.Once);
            Assert.NotNull(clienteActualizado);
            Assert.Equal(idCliente, clienteActualizado.Id);
        }

        [Theory]
        [InlineData("10", "concepto 1", 40000, 5)]
        public async Task Cliente_Use_Case_Asignar_Credito_Retorna_Cliente_Con_Credito_Asignado(string idCliente, string concepto, decimal monto, int cuotas)
        {
            //decimal interes = _mockConfiguration.Object.Value.Interes;
            decimal interes = (decimal)1.5;
            Cliente cliente = new ClienteBuilderTest()
                .ConId(idCliente)
                .ConNombre("JOhn")
                .ConCorreo("John@correo.com")
                .ConCreditos(new List<Credito>())
                .Build();
            Credito credito = new CreditoBuilderTest()
                .ConId("1")
                .ConConcepto(concepto)
                .ConMonto(monto)
                .ConCuotas(cuotas)
                .ConValorCuota(3510)
                .ConInteres(interes)
                .Build();
            _mockClienteRepository.Setup(repository => repository.ObtenerClientePorId(It.IsAny<string>()))
                .ReturnsAsync(ObtenerClienteTest("John", "John@correo.com", new List<Credito>()));
            _mockConfiguration.Setup(config => config.Value.Interes).Returns(interes);
            //.ReturnsAsync(ObtenerClienteTest("John", "John@correo.com", new List<Credito>()));

            _mockCreditoRepository.Setup(repository => repository.CrearCredito(It.IsAny<Credito>()))
                .ReturnsAsync(credito);

            cliente.AgregarCredito(credito);

            _mockClienteRepository.Setup(repository => repository.ActualizarCliente(It.IsAny<string>(), It.IsAny<Cliente>()))
                .ReturnsAsync(cliente);

            Cliente clienteConCredito = await _clienteUseCase.AsignarCredito(idCliente, monto, concepto, cuotas);

            _mockClienteRepository.Verify(repository => repository.ActualizarCliente(It.IsAny<string>(), It.IsAny<Cliente>()), Times.Once);
            _mockClienteRepository.Verify(repository => repository.ObtenerClientePorId(It.IsAny<string>()), Times.Once);
            _mockCreditoRepository.Verify(repository => repository.CrearCredito(It.IsAny<Credito>()), Times.Once);

            Assert.Equal(clienteConCredito.Creditos.Count, cliente.Creditos.Count);
        }

        [Theory]
        [InlineData("1", "1")]
        public async Task Cliente_Use_Case_Cancelar_Couta_Retorna_Cliente(string idCliente, string idCredito)
        {
            decimal interes = (decimal)1.5;
            Credito credito = new CreditoBuilderTest()
               .ConId("1")
               .ConConcepto("concepto")
               .ConMonto(240)
               .ConCuotas(5)
               .ConCuotasPagadas(10)
               .ConInteres(interes)
               .Build();
            Credito credito2 = new CreditoBuilderTest()
               .ConId("2")
               .ConConcepto("concepto")
               .ConMonto(240)
               .ConCuotas(5)
               .ConInteres(interes)
               .Build();
            Cliente cliente = new ClienteBuilderTest()
                .ConId(idCliente)
                .ConNombre("JOhn")
                .ConCorreo("John@correo.com")
                .ConCreditos(new List<Credito>
                {
                    credito,
                    credito2
                })
                .Build();
            cliente.Creditos.Find(credito => credito.Id.Equals(idCredito)).CancelarCuota();
            _mockClienteRepository.Setup(repository => repository.ObtenerClientePorId(It.IsAny<string>()))
                .ReturnsAsync(cliente);
            _mockClienteRepository.Setup(repository => repository.ActualizarCliente(It.IsAny<string>(), It.IsAny<Cliente>()))
                .ReturnsAsync(cliente);
            _mockCreditoRepository.Setup(repository => repository.ActualizarCredito(It.IsAny<string>(), It.IsAny<Credito>()))
                .ReturnsAsync(credito);

            Credito creditoaCancelarCuota = cliente.Creditos.Find(credito => credito.Id.Equals(idCredito));

            var clienteConCuotaPagada = await _clienteUseCase.CancelarCuota(idCliente, idCredito);

            _mockClienteRepository.Verify(repository => repository.ActualizarCliente(It.IsAny<string>(), It.IsAny<Cliente>()), Times.Once);
            _mockClienteRepository.Verify(repository => repository.ObtenerClientePorId(It.IsAny<string>()), Times.Once);
            _mockCreditoRepository.Verify(repository => repository.ActualizarCredito(It.IsAny<string>(), It.IsAny<Credito>()), Times.Once);

            Assert.Equal(clienteConCuotaPagada, cliente);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public async Task Cliente_Use_Case_Obtener_Creditos_Cliente_Retorna_List_De_Creditos(string idCliente)
        {
            Cliente cliente = new ClienteBuilderTest()
                .ConId(idCliente)
                .ConNombre("JOhn")
                .ConCorreo("John@correo.com")
                .ConCreditos(ObtenerCreditosTest())
                .Build();

            _mockClienteRepository.Setup(repository => repository.ObtenerClientePorId(It.IsAny<string>()))
                .ReturnsAsync(cliente);

            List<Credito> creditos = await _clienteUseCase.ObtenerCreditosCliente(idCliente);

            Assert.NotNull(creditos);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public async Task Cliente_Use_Case_Obtener_Creditos_Pendientes_Cliente_Retorna_List_De_Creditos(string idCliente)
        {
            Cliente cliente = new ClienteBuilderTest()
                .ConId(idCliente)
                .ConNombre("JOhn")
                .ConCorreo("John@correo.com")
                .ConCreditos(ObtenerCreditosTest())
                .Build();

            _mockClienteRepository.Setup(repository => repository.ObtenerClientePorId(It.IsAny<string>()))
                .ReturnsAsync(cliente);

            List<Credito> creditos = await _clienteUseCase.ObtenerCreditosCliente(idCliente);

            Assert.NotNull(creditos);
        }

        private List<Credito> ObtenerCreditosTest() => new()
        {
            new CreditoBuilderTest()
                .ConId("1")
                .ConConcepto("Concepto")
                .ConMonto(50000)
                .ConCuotas(5)
                .ConInteres((decimal)1.5)
                .Build(),
            new CreditoBuilderTest()
                .ConId("2")
                .ConConcepto("Concepto 2")
                .ConMonto(50000)
                .ConCuotas(5)
                .ConInteres((decimal)1.5)
                .Build()
        };

        private List<Cliente> ObtenerClientesTest() => new()
        {
            new ClienteBuilderTest()
            .ConId("1")
            .ConNombre("John")
            .ConCorreo("John@correo.com")
            .ConCreditos(new List<Credito>())
            .Build(),
            new ClienteBuilderTest()
            .ConId("2")
            .ConNombre("Edward")
            .ConCorreo("Edward@correo.com")
            .ConCreditos(new List<Credito>())
            .Build()
        };

        private Cliente ObtenerClienteTest(string nombre, string correo, List<Credito> creditos)
        {
            return new ClienteBuilderTest()
                .ConId("1")
                .ConNombre(nombre)
                .ConCorreo(correo)
                .ConCreditos(creditos)
                .Build();
        }

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
    }
}