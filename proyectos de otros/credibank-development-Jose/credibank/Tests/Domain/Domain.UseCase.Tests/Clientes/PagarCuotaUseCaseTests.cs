using credinet.exception.middleware.models;

using Domain.Model.Entities;
using Domain.Model.Interfaces.Gateways;
using Domain.Model.Interfaces.UseCases;

using FluentAssertions;

using Helpers.Commons.Exceptions;
using Helpers.Domain.Clientes;
using Helpers.Domain.Creditos;
using Helpers.Domain.Pagos;

using Moq;

using Xunit;

namespace Domain.UseCases.Clientes.Tests;

public class PagarCuotaUseCaseTests
{
    private readonly Mock<IRepositorioDeClientesGateway> _repositorioDeClientesMock;

    public PagarCuotaUseCaseTests()
    {
        _repositorioDeClientesMock = new Mock<IRepositorioDeClientesGateway>();
        _pagarCuotaUseCase = new PagarCuotaUseCase(_repositorioDeClientesMock.Object);
    }

    private readonly IPagarCuotasUseCase _pagarCuotaUseCase;

    [Fact(DisplayName = "#Pagar debería fallar cuando el crédito ingresado no existe dentro del Cliente.")]
    public void PagarCuota_DeberiaFallar_CuandoElCreditoIngresadoNoExiste()
    {
        // Arrange
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConCedulaDeCiudadania(Guid.NewGuid().ToString())
            .ConCreditos(new List<Credito>())
            .Build();
        Pago pago = PagoTestBuilder.Builder()
            .Build();

        _repositorioDeClientesMock
            .Setup(x => x.ObtenerPorDocumentoDeIdentidadAsync(cliente.DocumentoDeIdentidad))
            .ReturnsAsync(() => cliente);

        // Act & Assert
        _pagarCuotaUseCase
            .Invoking(x => x.Pagar(cliente.DocumentoDeIdentidad, Guid.NewGuid().ToString(), pago))
            .Should()
            .ThrowAsync<BusinessException>()
            .Where(exception => exception.code == Convert.ToInt32(TipoExcepcionNegocio.CreditoNoExiste));
    }

    [Fact(DisplayName = "#Pagar debería fallar cuando el cliente intenta pagar más cuotas de las que debe.")]
    public void PagarCuota_DeberiaFallar_CuandoSeIntentaPagarMasCuotasDeLasQueSeDebe()
    {
        // Arrange
        Credito credito = CreditoTestBuilder.Builder()
            .ConCuotasRestantes(2)
            .Build();
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConCedulaDeCiudadania(Guid.NewGuid().ToString())
            .ConCreditos(new List<Credito>() { credito })
            .Build();
        Pago pago = PagoTestBuilder.Builder()
            .ConCuotasACancelar(3)
            .Build();

        _repositorioDeClientesMock
            .Setup(x => x.ObtenerPorDocumentoDeIdentidadAsync(cliente.DocumentoDeIdentidad))
            .ReturnsAsync(() => cliente);

        // Act & Assert
        _pagarCuotaUseCase
            .Invoking(x => x.Pagar(cliente.DocumentoDeIdentidad, Guid.NewGuid().ToString(), pago))
            .Should()
            .ThrowAsync<BusinessException>()
            .Where(exception => exception.code == Convert.ToInt32(TipoExcepcionNegocio.SeIntentoPagarMasCuotasDeLasDebidas));
    }

    [Fact(DisplayName = "#Pagar debería añadir un pago al crédito y reducir las cuotas restantes de forma satisfactoria.")]
    public async Task PagarCuota_DeberiaAnexarUnPagoAlCreditoYReducirLasCuotasRestantes()
    {
        // Arrange
        Credito credito = CreditoTestBuilder.Builder()
            .ConId(Guid.NewGuid().ToString())
            .ConCuotasRestantes(4)
            .ConPagosRealizados(new List<Pago>())
            .ConEstado(EstadosDeCredito.EN_VIGENCIA)
            .Build();
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConCedulaDeCiudadania(Guid.NewGuid().ToString())
            .ConCreditos(new List<Credito>() { credito })
            .Build();
        Pago pago = PagoTestBuilder.Builder()
            .ConMonto(350000)
            .ConCuotasACancelar(3)
            .Build();

        _repositorioDeClientesMock
            .Setup(x => x.ObtenerPorDocumentoDeIdentidadAsync(cliente.DocumentoDeIdentidad))
            .ReturnsAsync(() => cliente);
        _repositorioDeClientesMock
           .Setup(x => x.AnexarCuotaACreditoAsync(cliente, credito, pago))
           .ReturnsAsync((Cliente cliente, Credito credito, Pago pago) =>
           {
               pago.Id = Guid.NewGuid().ToString();
               return credito;
           });

        // Act
        Credito creditoModificado = await _pagarCuotaUseCase.Pagar(cliente.DocumentoDeIdentidad, credito.Id, pago);

        // Assert
        creditoModificado.PagosRealizados.Count.Should().Be(1);
        creditoModificado.CuotasRestantes.Should().Be(1);
        creditoModificado.Estado.Should().Be(EstadosDeCredito.EN_VIGENCIA);
    }

    [Fact(DisplayName = "#Pagar debería cambiar el estado del crédito a Cancelado cuando se paguen todas las cuotas.")]
    public async Task PagarCuota_DeberiaCambiarElEstadoDelCreditoACancelado_CuandoSePaguenTodasLasCuotas()
    {
        // Arrange
        Credito credito = CreditoTestBuilder.Builder()
            .ConCuotasRestantes(3)
            .ConPagosRealizados(new List<Pago>())
            .ConEstado(EstadosDeCredito.EN_VIGENCIA)
            .Build();
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConCedulaDeCiudadania(Guid.NewGuid().ToString())
            .ConCreditos(new List<Credito>() { credito })
            .Build();
        Pago pago = PagoTestBuilder.Builder()
            .ConMonto(350000)
            .ConCuotasACancelar(3)
            .Build();

        _repositorioDeClientesMock
            .Setup(x => x.ObtenerPorDocumentoDeIdentidadAsync(cliente.DocumentoDeIdentidad))
            .ReturnsAsync(() => cliente);
        _repositorioDeClientesMock
           .Setup(x => x.AnexarCuotaACreditoAsync(cliente, credito, pago))
           .ReturnsAsync((Cliente cliente, Credito credito, Pago pago) =>
           {
               pago.Id = Guid.NewGuid().ToString();
               return credito;
           });

        // Act
        Credito creditoModificado = await _pagarCuotaUseCase.Pagar(cliente.DocumentoDeIdentidad, credito.Id, pago);

        // Assert
        creditoModificado.Estado.Should().Be(EstadosDeCredito.CANCELADO);
    }
}