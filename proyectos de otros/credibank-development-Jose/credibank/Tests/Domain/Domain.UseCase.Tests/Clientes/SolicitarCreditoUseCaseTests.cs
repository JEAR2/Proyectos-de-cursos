using Xunit;
using FluentAssertions;
using Moq;
using Domain.Model.Interfaces.Gateways;
using Domain.Model.Interfaces.UseCases;
using Domain.Model.Entities;
using Helpers.Domain.Creditos;
using credinet.exception.middleware.models;
using Helpers.Commons.Exceptions;
using Helpers.Domain.Clientes;

namespace Domain.UseCases.Clientes.Tests;

public class SolicitarCreditoUseCaseTests
{
    private readonly Mock<IRepositorioDeClientesGateway> _repositorioDeClientesMock;
    private readonly ISolicitarCreditoUseCase _solicitarCreditoUseCase;

    public SolicitarCreditoUseCaseTests()
    {
        _repositorioDeClientesMock = new Mock<IRepositorioDeClientesGateway>();
        _solicitarCreditoUseCase = new SolicitarCreditoUseCase(_repositorioDeClientesMock.Object);
    }

    [Fact(DisplayName = "#Solicitar debería fallar cuando el cliente ingresado no existe.")]
    public void SolicitarCredito_DeberiaFallar_CuandoElClienteNoExiste()
    {
        // Arrange
        Credito credito = CreditoTestBuilder.Builder()
            .Build();
        string documentoDeIdentidad = Guid.NewGuid().ToString();
        _repositorioDeClientesMock
            .Setup(x => x.ObtenerPorDocumentoDeIdentidadAsync(documentoDeIdentidad))
            .ReturnsAsync(() => null);

        // Act & Assert
        _solicitarCreditoUseCase
            .Invoking(x => x.Solicitar(documentoDeIdentidad, credito, 0.46M))
            .Should()
            .ThrowAsync<BusinessException>()
            .Where(exception => exception.code == Convert.ToInt32(TipoExcepcionNegocio.ClienteNoExiste));
    }

    [Fact(DisplayName = "#Solicitar debería fallar cuando el monto ingresado no es valido.")]
    public void SolicitarCredito_DeberiaFallar_CuandoElMontoIngresadoNoEsValido()
    {
        // Arrange
        Credito credito = CreditoTestBuilder.Builder()
            .ConMonto(50000M)
            .Build();
        Cliente cliente = ClienteTestBuilder.Builder().Build();
        string documentoDeIdentidad = Guid.NewGuid().ToString();
        _repositorioDeClientesMock
            .Setup(x => x.ObtenerPorDocumentoDeIdentidadAsync(documentoDeIdentidad))
            .ReturnsAsync(() => cliente);

        // Act & Assert
        _solicitarCreditoUseCase
            .Invoking(x => x.Solicitar(documentoDeIdentidad, credito, 0.46M))
            .Should()
            .ThrowAsync<BusinessException>()
            .Where(exception => exception.code == Convert.ToInt32(TipoExcepcionNegocio.MontoDeCreditoNoValido));
    }

    [Fact(DisplayName = "#Solicitar debería fallar cuando el plazo ingresado para cancelar es invalido.")]
    public void SolicitarCredito_DeberiaFallar_CuandoElPlazoIngresadoEsInvalido()
    {
        // Arrange
        Credito credito = CreditoTestBuilder.Builder()
            .ConMonto(101000M)
            .ConPlazoEnMeses(0)
            .Build();
        Cliente cliente = ClienteTestBuilder.Builder().Build();
        string documentoDeIdentidad = Guid.NewGuid().ToString();
        _repositorioDeClientesMock
            .Setup(x => x.ObtenerPorDocumentoDeIdentidadAsync(documentoDeIdentidad))
            .ReturnsAsync(() => cliente);

        // Act & Assert
        _solicitarCreditoUseCase
            .Invoking(x => x.Solicitar(documentoDeIdentidad, credito, 0.46M))
            .Should()
            .ThrowAsync<BusinessException>()
            .Where(exception => exception.code == Convert.ToInt32(TipoExcepcionNegocio.PlazoDeCancelacionNoValido));
    }

    [Fact(DisplayName = "#Solicitar debería solicitar un nuevo crédito de forma satisfactoria.")]
    public async Task SolicitarCredito_DeberiaSolicitarUnNuevoCredito()
    {
        // Arrange
        Credito credito = CreditoTestBuilder.Builder()
            .ConMonto(101000M)
            .ConPlazoEnMeses(2)
            .Build();
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConCreditos(new List<Credito>())
            .Build();
        string documentoDeIdentidad = Guid.NewGuid().ToString();
        _repositorioDeClientesMock
            .Setup(x => x.ObtenerPorDocumentoDeIdentidadAsync(documentoDeIdentidad))
            .ReturnsAsync(() => cliente);
        _repositorioDeClientesMock
            .Setup(x => x.AnexarCreditoAClienteAsync(cliente, credito))
            .ReturnsAsync((Cliente cliente, Credito credito) =>
            {
                credito.Id = Guid.NewGuid().ToString();
                return credito;
            });

        // Act
        Credito creditoSolicitado = await _solicitarCreditoUseCase
            .Solicitar(documentoDeIdentidad, credito, 0.46M);

        // Assert
        creditoSolicitado.MontoTotalDeIntereses.Should().NotBe(null);
        creditoSolicitado.FechaDeSolicitud.Should().BeBefore(DateTime.Now);
        creditoSolicitado.FechaDeCancelacion.Should().Be(null);
        creditoSolicitado.CuotasRestantes.Should().Be(credito.PlazoEnMeses);
        creditoSolicitado.PagosRealizados.Count.Should().Be(0);
        creditoSolicitado.MontoPorCuota.Should().Be((creditoSolicitado.Monto + creditoSolicitado.MontoTotalDeIntereses) / creditoSolicitado.PlazoEnMeses);
        creditoSolicitado.Estado.Should().Be(EstadosDeCredito.EN_VIGENCIA);
    }
}