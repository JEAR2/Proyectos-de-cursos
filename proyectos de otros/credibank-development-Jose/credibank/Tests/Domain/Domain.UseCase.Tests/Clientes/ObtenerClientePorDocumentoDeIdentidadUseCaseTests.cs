using Xunit;
using FluentAssertions;
using Moq;
using Domain.Model.Interfaces.Gateways;
using Domain.Model.Interfaces.UseCases;
using credinet.exception.middleware.models;
using Helpers.Commons.Exceptions;
using Helpers.Domain.Clientes;
using Domain.Model.Entities;

namespace Domain.UseCases.Clientes.Tests;

public class ObtenerClientePorDocumentoDeIdentidadUseCaseTests
{
    private readonly Mock<IRepositorioDeClientesGateway> _repositorioDeClientesMock;
    private readonly IObtenerClientePorDocumentoDeIdentidadUseCase _obtenerClientePorDocumentoDeIdentidadUseCase;

    public ObtenerClientePorDocumentoDeIdentidadUseCaseTests()
    {
        _repositorioDeClientesMock = new Mock<IRepositorioDeClientesGateway>();
        _obtenerClientePorDocumentoDeIdentidadUseCase = new ObtenerClientePorDocumentoDeIdentidadUseCase(_repositorioDeClientesMock.Object);
    }

    [Fact(DisplayName = "#Obtener debería fallar cuando el Documento de Identidad ingresado no pertenece a ningún Cliente.")]
    public async Task Obtener_DeberiaFallar_CuandoElDocumentoDeIdentidadIngresadoNoPerteneceANingunCliente()
    {
        // Arrange
        _repositorioDeClientesMock
            .Setup(x => x.ObtenerPorDocumentoDeIdentidadAsync(It.IsAny<string>()))
            .ReturnsAsync(() => null);

        // Act & Assert
        await _obtenerClientePorDocumentoDeIdentidadUseCase
            .Invoking(x => x.Obtener(It.IsAny<string>()))
            .Should()
            .ThrowAsync<BusinessException>()
            .Where(exception => exception.code == Convert.ToInt32(TipoExcepcionNegocio.ClienteNoExiste));
    }

    [Fact(DisplayName = "#Obtener debería retornar el usuario asociado a un Documento de Identidad ingresado.")]
    public async Task Obtener_DeberiaRetornarElUsuarioAsociadoAUnDocumentoDeIdentidadIngresado_CuandoSeaExitoso()
    {
        // Arrange
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConCedulaDeCiudadania("12398794234")
            .Build();
        _repositorioDeClientesMock
            .Setup(x => x.ObtenerPorDocumentoDeIdentidadAsync(cliente.DocumentoDeIdentidad))
            .ReturnsAsync(() => cliente);

        // Act
        Cliente clienteObtenido = await _obtenerClientePorDocumentoDeIdentidadUseCase.Obtener(cliente.DocumentoDeIdentidad);

        // Assert
        clienteObtenido.Should().NotBeNull();
        clienteObtenido.DocumentoDeIdentidad.Should().Be(cliente.DocumentoDeIdentidad);
    }
}