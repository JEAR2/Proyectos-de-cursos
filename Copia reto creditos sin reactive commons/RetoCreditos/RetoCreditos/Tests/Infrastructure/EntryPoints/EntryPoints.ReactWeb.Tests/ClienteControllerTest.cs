using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Interfaces;
using Domain.Model.Tests;
using Domain.UseCase.Clientes;
using EntryPoints.ReactiveWeb.Controllers;
using EntryPoints.ReactiveWeb.Entities;
using Helpers.Commons.Exceptions;
using Helpers.ObjectsUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Moq;
using StackExchange.Redis;
using System.Net;
using Xunit;

namespace EntryPoints.ReactWeb.Tests
{
    public class ClienteControllerTest
    {
        private readonly Mock<IClienteUseCase> _mockClienteUseCase;
        private readonly Mock<IManageEventsUseCase> _manageEventsUseCase;
        private readonly Mock<IOptions<ConfiguradorAppSettings>> _appSettings;

        private ClienteController _clienteController { get; set; }

        public ClienteControllerTest()
        {
            _appSettings = new();
            _manageEventsUseCase = new();
            _mockClienteUseCase = new();
            _appSettings.Setup(settings => settings.Value)
               .Returns(new ConfiguradorAppSettings
               {
                   DefaultCountry = "co",
                   DomainName = "RetoCreditos"
               });
            _clienteController = new(_mockClienteUseCase.Object, _manageEventsUseCase.Object, _appSettings.Object);
            _clienteController.ControllerContext.HttpContext = new DefaultHttpContext();
            _clienteController.ControllerContext.HttpContext.Request.Headers["Location"] = "1,1";
            _clienteController.ControllerContext.RouteData = new RouteData();
            _clienteController.ControllerContext.RouteData.Values.Add("controller", "Clientes");
        }

        [Theory]
        [InlineData("John", "Acevedo", "John@correo.com", "CO")]
        public async Task Cliente_Controller_Crear_Clientes_Return_Status200(string nombre, string apellido, string correo, string pais)
        {
            ClienteRequest clienteRequest = ObtenerClienteResquestTest(nombre, apellido, correo, pais);

            _mockClienteUseCase.Setup(useCase => useCase.CrearCliente(It.IsAny<Cliente>()))
                .ReturnsAsync(ObtenerClienteTest(nombre, apellido, correo, pais));
            _clienteController.ControllerContext.RouteData.Values.Add("action", "CrearClientes");
            var result = await _clienteController.CrearClientes(clienteRequest);
            var okObjectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, okObjectResult?.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Cliente_Controller_Crear_Clientes_Return_Excepcion_No_Controlada()
        {
            ClienteRequest clienteRequest = new();

            _mockClienteUseCase
                .Setup(useCase => useCase.CrearCliente(It.IsAny<Cliente>()))
                .ReturnsAsync(ObtenerClienteTest(string.Empty, string.Empty, string.Empty, string.Empty));

            _clienteController.ControllerContext.RouteData.Values.Add("action", "CrearClientes");

            BusinessException exception =
                await Assert.ThrowsAsync<BusinessException>(async () => await _clienteController.CrearClientes(clienteRequest));

            Assert.Equal((int)TipoExcepcionNegocio.ExceptionNoControlada, exception.code);
        }

        [Fact]
        public async Task Cliente_Controller_Crear_Clientes_Return_Excepcion_De_Negocio()
        {
            ClienteRequest clienteRequest = ObtenerClienteResquestTest(string.Empty, string.Empty, string.Empty, string.Empty);

            _mockClienteUseCase
                .Setup(useCase => useCase.CrearCliente(It.IsAny<Cliente>()))
                .Throws(new BusinessException(nameof(TipoExcepcionNegocio.ClienteNoValido), (int)TipoExcepcionNegocio.ClienteNoValido));

            _clienteController.ControllerContext.RouteData.Values.Add("action", "CrearClientes");

            BusinessException exception =
                await Assert.ThrowsAsync<BusinessException>(async () => await _clienteController.CrearClientes(clienteRequest));

            Assert.Equal((int)TipoExcepcionNegocio.ClienteNoValido, exception.code);
        }

        [Theory]
        [InlineData("1", 12345, "concepto", 5)]
        public async Task Cliente_Controller_Asignar_Credito_Return_Status200(string idCliente, decimal monto, string concepto, int cuotas)
        {
            string nombre = "John";
            string apellido = "Acevedo";
            string correo = "John@correo.com";
            string pais = "CO";
            CrearCreditoRequest crearCreditoRequest = new CrearCreditoRequest(idCliente, monto, concepto, cuotas);
            _mockClienteUseCase.Setup(useCase => useCase.AsignarCredito(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(ObtenerClienteTest(nombre, apellido, correo, pais));
            _clienteController.ControllerContext.RouteData.Values.Add("action", "AsignarCredito");
            var result = await _clienteController.AsignarCredito(crearCreditoRequest);
            var okObjectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, okObjectResult?.StatusCode);
            Assert.NotNull(result);
        }

        /*
        [Fact]
        public async Task Cliente_Controller_Asignar_Credito_Return_Excepcion_No_Controlada()
        {
            CrearCreditoRequest crearCreditoRequest = new CrearCreditoRequest(string.Empty, decimal.Zero, string.Empty, 0);
            _mockClienteUseCase
                .Setup(useCase => useCase.AsignarCredito(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(ObtenerClienteTest(string.Empty, string.Empty, string.Empty, string.Empty));

            _clienteController.ControllerContext.RouteData.Values.Add("action", "AsignarCredito");

            BusinessException exception =
                await Assert.ThrowsAsync<BusinessException>(async () => await _clienteController.AsignarCredito(crearCreditoRequest));

            Assert.Equal((int)TipoExcepcionNegocio.ExceptionNoControlada, exception.code);
        }

        [Fact]
        public async Task Cliente_Controller_Asignar_Credito_Return_Excepcion_De_Negocio()
        {
            CrearCreditoRequest crearCreditoRequest = new CrearCreditoRequest(string.Empty, decimal.Zero, string.Empty, 0);

            _mockClienteUseCase
                .Setup(useCase => useCase.AsignarCredito(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>()))
                .Throws(new BusinessException(nameof(TipoExcepcionNegocio.ClienteNoValido), (int)TipoExcepcionNegocio.ClienteNoValido));

            _clienteController.ControllerContext.RouteData.Values.Add("action", "AsignarCredito");

            BusinessException exception =
                await Assert.ThrowsAsync<BusinessException>(async () => await _clienteController.AsignarCredito(crearCreditoRequest));

            Assert.Equal((int)TipoExcepcionNegocio.ClienteNoValido, exception.code);
        }
        */

        [Theory]
        [InlineData("1", "1")]
        public async Task Cliente_Controller_Cancelar_Cuota_Credito_Return_Status200(string idCliente, string idCredito)
        {
            string nombre = "John";
            string apellido = "Acevedo";
            string correo = "John@correo.com";
            string pais = "CO";
            CancelarCredito cancelarCredito = new(idCliente, idCredito);
            _mockClienteUseCase.Setup(useCase => useCase.CancelarCuota(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(ObtenerClienteTest(nombre, apellido, correo, pais));
            _clienteController.ControllerContext.RouteData.Values.Add("action", "CancelarCuota");
            var result = await _clienteController.CancelarCuota(cancelarCredito);
            var okObjectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, okObjectResult?.StatusCode);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("1")]
        public async Task Cliente_Controller_Ver_Creditos_Cliente_Return_Status200(string idCliente)
        {
            _mockClienteUseCase.Setup(useCase => useCase.ObtenerCreditosCliente(It.IsAny<string>()))
                .ReturnsAsync(ObtenerCreditosTest());
            _clienteController.ControllerContext.RouteData.Values.Add("action", "VerCreditosCLiente");
            var result = await _clienteController.VerCreditosCLiente(idCliente);
            var okObjectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, okObjectResult?.StatusCode);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("1")]
        public async Task Cliente_Controller_Ver_Creditos_Pendientes_Cliente_Return_Status200(string idCliente)
        {
            _mockClienteUseCase.Setup(useCase => useCase.ObtenerCreditosPendientesCliente(It.IsAny<string>()))
                .ReturnsAsync(ObtenerCreditosTest());
            _clienteController.ControllerContext.RouteData.Values.Add("action", "VerCreditosCLiente");
            var result = await _clienteController.VerCreditosPendientesCLiente(idCliente);
            var okObjectResult = result as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, okObjectResult?.StatusCode);
            Assert.NotNull(result);
        }

        private ClienteRequest ObtenerClienteResquestTest(string nombre, string apellido, string correo, string pais) => new()
        {
            Nombre = nombre,
            Apellido = apellido,
            Correo = correo,
            Pais = pais,
            Creditos = new List<Credito>()
        };

        private Cliente ObtenerClienteTest(string nombre, string apellido, string correo, string pais) => new ClienteBuilderTest()
           .ConId("1")
           .ConNombre(nombre)
            .ConApellido(apellido)
            .ConCorreo(correo)
            .ConPais(pais)
            .ConCreditos(new List<Credito>())
            .Build();

        private List<Credito> ObtenerCreditosTest() => new()
        {
            new CreditoBuilderTest()
            .ConId("1")
            .ConConcepto("concepto 1")
            .ConCuotas(3)
            .ConMonto(23456)
            .Build(),
            new CreditoBuilderTest()
            .ConId("2")
            .ConConcepto("concepto 2")
            .ConCuotas(13)
            .ConMonto(223456)
            .Build()
        };
    }
}