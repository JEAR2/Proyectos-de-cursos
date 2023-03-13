using Xunit;
using FluentAssertions;
using Helpers.Domain.Creditos;
using Helpers.Domain.Clientes;
using Helpers.Domain.Pagos;

namespace Domain.Model.Entities.Tests;

public class ClienteTests
{
    [Fact(DisplayName = "#SolicitarCredito debería retornar un crédito valido y en proceso.")]
    public void SolicitarCredito_DeberiaRetornarUnCreditoValidoYAprovado()
    {
        // Arrange
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConNombre("Pepito")
            .ConApellido("Suarez Gonzales")
            .ConCreditos(new List<Credito>())
            .Build();
        Credito creditoASolicitar = CreditoTestBuilder
            .Builder()
            .ConMonto(1000000)
            .ConEstado(EstadosDeCredito.EN_PROCESAMIENTO)
            .ConFechaDeSolicitud(DateTime.Now)
            .ConPlazoEnMeses(6)
            .ConCuotasRestantes(6)
            .Build();

        // Act
        cliente.SolicitarCredito(creditoASolicitar, 0.46M);

        // Assert
        cliente.Creditos.Count.Should().Be(1);
        cliente.Creditos.FirstOrDefault().Should().NotBeNull();
        cliente.Creditos.FirstOrDefault()!.Monto.Should().Be(creditoASolicitar.Monto);
        cliente.Creditos.FirstOrDefault()!.Estado.Should().Be(EstadosDeCredito.EN_PROCESAMIENTO);
        cliente.Creditos.FirstOrDefault()!.PlazoEnMeses.Should().Be(creditoASolicitar.PlazoEnMeses);
        cliente.Creditos.FirstOrDefault()!.CuotasRestantes.Should().Be(creditoASolicitar.CuotasRestantes);
    }

    [Fact(DisplayName = "#PagarCuotaDeCredito debería añadir un pago dentro del crédito.")]
    public void PagarCuotaDeCreditoTest()
    {
        // Arrange
        string idDelCredito = Guid.NewGuid().ToString();
        Cliente cliente = ClienteTestBuilder.Builder()
            .ConNombre("Pepito")
            .ConApellido("Suarez Gonzales")
            .ConCreditos(new List<Credito>())
            .Build();
        Credito creditoASolicitar = CreditoTestBuilder
            .Builder()
            .ConId(idDelCredito)
            .ConMonto(1000000)
            .ConEstado(EstadosDeCredito.EN_PROCESAMIENTO)
            .ConFechaDeSolicitud(DateTime.Now)
            .ConPlazoEnMeses(6)
            .ConCuotasRestantes(6)
            .ConPagosRealizados(new List<Pago>())
            .Build();
        Pago pago = PagoTestBuilder.Builder()
            .ConCuotasACancelar(1)
            .ConMonto(23423)
            .Build();
        cliente.SolicitarCredito(creditoASolicitar, 0.46M);

        // Act
        cliente.PagarCuotaDeCredito(idDelCredito, pago);

        // Assert
        cliente.Creditos.FirstOrDefault().Should().NotBeNull();
        cliente.Creditos.FirstOrDefault()!.PagosRealizados.Count.Should().Be(1);
    }
}