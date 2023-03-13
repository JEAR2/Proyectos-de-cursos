using Xunit;
using FluentAssertions;

using Helpers.Domain.Creditos;
using System.Threading;
using System.Runtime.CompilerServices;
using Helpers.Domain.Pagos;

namespace Domain.Model.Entities.Tests;

public class CreditoTests
{
    [Theory(DisplayName = "#CalcularMontoTotalDeIntereses debería retornar el monto de intereses correcto.")]
    [InlineData(1000000, 6, 208304.59735)]
    [InlineData(3500000, 8, 1004396.52621)]
    [InlineData(50000000, 48, 177185927.99999)]
    public void CalcularMontoTotalDeIntereses_DeberiaRetornarElMontoDeInteresCorrecto(
        decimal monto,
        int plazoEnMeses,
        decimal interesAproximado
    )
    {
        // Arrange
        decimal interesEfectivoAnual = 0.46M;
        Credito credito = CreditoTestBuilder.Builder()
            .ConMonto(monto)
            .ConPlazoEnMeses(plazoEnMeses)
            .Build();

        // Act
        decimal montoTotalDeInteres = credito.CalcularMontoTotalDeIntereses(interesEfectivoAnual);

        // Assert
        montoTotalDeInteres.Should().BeApproximately(interesAproximado, 0.5M);
    }

    [Theory(DisplayName = "#PagarCuota debería disminuir la cantidad de cuotas restantes y añadir el pago a la lista de pagos realizados.")]
    [InlineData(1000000, 5, 6, 2)]
    [InlineData(3500000, 3, 8, 1)]
    public void PagarCuota_DeberiaDisminuirLaCantidadDeCuotasRestantesYAñadirElPagoALaLista(
        decimal montoDelCredito,
        int cuotasRestantesDelCredito,
        int plazoEnMesesDelCredito,
        int cuotasAPagar
    )
    {
        // Arrange
        Credito credito = CreditoTestBuilder.Builder()
            .ConMonto(montoDelCredito)
            .ConPlazoEnMeses(plazoEnMesesDelCredito)
            .ConPagosRealizados(new List<Pago>())
            .ConCuotasRestantes(cuotasRestantesDelCredito)
            .Build();
        Pago pago = PagoTestBuilder.Builder()
            .ConCuotasACancelar(cuotasAPagar)
            .ConFechaDeCancelacion(DateTime.Now)
            .Build();

        // Act
        credito.PagarCuota(pago);

        // Assert
        credito.CuotasRestantes.Should().Be(cuotasRestantesDelCredito - pago.CuotasACancelar);
        credito.PagosRealizados.Count.Should().Be(1);
    }

    [Fact()]
    public void CambiarEstadoTest()
    {
        // Arrange
        Credito credito = CreditoTestBuilder.Builder()
            .ConEstado(EstadosDeCredito.EN_PROCESAMIENTO)
            .Build();

        // Act
        credito.CambiarEstado(EstadosDeCredito.APROBADO);

        // Assert
        credito.Estado.Should().NotBe(EstadosDeCredito.EN_PROCESAMIENTO);
        credito.Estado.Should().Be(EstadosDeCredito.APROBADO);
    }
}