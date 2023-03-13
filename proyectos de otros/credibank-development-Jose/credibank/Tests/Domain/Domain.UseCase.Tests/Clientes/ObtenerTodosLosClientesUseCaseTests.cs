using Xunit;
using FluentAssertions;
using Moq;
using Domain.Model.Interfaces.Gateways;
using Domain.Model.Interfaces.UseCases;
using Domain.Model.Entities;
using Helpers.Domain.Clientes;

namespace Domain.UseCases.Clientes.Tests;

public class ObtenerTodosLosClientesUseCaseTests
{
    private readonly Mock<IRepositorioDeClientesGateway> _repositorioDeClientesMock;
    private readonly IObtenerTodosLosClientesUseCase _obtenerTodosLosClientesUseCase;

    public ObtenerTodosLosClientesUseCaseTests()
    {
        _repositorioDeClientesMock = new Mock<IRepositorioDeClientesGateway>();
        _obtenerTodosLosClientesUseCase = new ObtenerTodosLosClientesUseCase(_repositorioDeClientesMock.Object);
    }

    [Fact(DisplayName = "#Obtener debería retornar una lista de clientes devueltos desde el repositorio de clientes.")]
    public async Task Obtener_DeberiaRetornarUnaListaConLosClientesDevueltosPorElRepositorio()
    {
        // Arrange
        IList<Cliente> clientes = new List<Cliente>() {
            ClienteTestBuilder.Builder()
                .Build()
        };
        _repositorioDeClientesMock
            .Setup(x => x.ObtenerTodosAsync())
            .ReturnsAsync(clientes);

        // Act
        IEnumerable<Cliente> clientesObtenidos = await _obtenerTodosLosClientesUseCase.ObtenerTodos();

        // Assert
        clientesObtenidos.As<ICollection<Cliente>>().Count.Should().Be(clientes.Count);
    }
}