using Xunit;
using Domain.Model.Interfaces.Gateways;
using Moq;
using FluentAssertions;
using Domain.Model.Entities;
using Helpers.Domain.Clientes;
using credinet.exception.middleware.models;
using Helpers.Commons.Exceptions;

namespace Domain.UseCases.Clientes.Tests;

public class CrearClienteUseCaseTests
{
    private readonly Mock<IRepositorioDeClientesGateway> _repositorioDeClientesMock;
    private readonly CrearClienteUseCase _crearClienteUseCase;

    public CrearClienteUseCaseTests()
    {
        _repositorioDeClientesMock = new Mock<IRepositorioDeClientesGateway>();
        _crearClienteUseCase = new CrearClienteUseCase(_repositorioDeClientesMock.Object);
    }

    [Fact(DisplayName = "#CrearCliente debería fallar cuando ya existe un cliente con el mismo documento de identidad.")]
    public async Task Crear_DeberiaFallar_CuandoYaExisteOtroClienteConElMismoDocumentoDeIdentidad()
    {
        // Arrange
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConNombre("Juan Martinez")
            .ConCedulaDeCiudadania("123456789")
            .ConNumeroDeCelular("320457812")
            .Build();
        _repositorioDeClientesMock
            .Setup(
            x => x.ClienteConDocumentoDeIdentidadExisteAsync(cliente.DocumentoDeIdentidad)
            )
            .ReturnsAsync(() => true);

        // Act & Assert
        await _crearClienteUseCase
            .Invoking(x => x.Crear(cliente))
            .Should()
            .ThrowAsync<BusinessException>()
            .Where(
            exception => exception.code == Convert.ToInt32(TipoExcepcionNegocio.DocumentoDeIdentidadYaRegistrado)
            );
    }

    [Fact(DisplayName = "#CrearCliente debería guardar la información del cliente cuando esta es válida.")]
    public async Task Crear_DeberiaGuardarLaInformacionDelCliente_CuandoSeaExitoso()
    {
        // Arrange
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConNombre("Juan")
            .ConApellido("Martinez")
            .ConCedulaDeCiudadania("123456789")
            .ConNumeroDeCelular("320457812")
            .ConCorreoElectronico("juan_martinez@gmail.com")
            .Build();
        _repositorioDeClientesMock
           .Setup(
           x => x.ClienteConDocumentoDeIdentidadExisteAsync(cliente.DocumentoDeIdentidad)
           )
           .ReturnsAsync(() => false);
        _repositorioDeClientesMock
            .Setup(x => x.GuardarAsync(cliente))
            .ReturnsAsync((Cliente cliente) => new Cliente()
            {
                Id = Guid.NewGuid().ToString(),
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                DocumentoDeIdentidad = cliente.DocumentoDeIdentidad,
                CorreoElectronico = cliente.CorreoElectronico,
                NumeroDeCelular = cliente.NumeroDeCelular,
                Creditos = cliente.Creditos,
            });

        // Act
        Cliente clienteGuardado = await _crearClienteUseCase.Crear(cliente);

        // Assert
        clienteGuardado.Id.Should().NotBeNullOrEmpty();
        clienteGuardado.Creditos.Count.Should().Be(0);
        clienteGuardado.Creditos.Should().NotBeNull();
        clienteGuardado.DocumentoDeIdentidad.Should().Be(cliente.DocumentoDeIdentidad);
    }
}