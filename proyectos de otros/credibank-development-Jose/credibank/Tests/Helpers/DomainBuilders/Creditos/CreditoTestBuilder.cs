using Domain.Model.Entities;

namespace Helpers.Domain.Creditos;

public class CreditoTestBuilder
{
    private string Id { get; set; }
    private decimal Monto { get; set; }
    private EstadosDeCredito Estado { get; set; }
    private int PlazoEnMeses { get; set; }
    private decimal MontoTotalDeIntereses { get; set; }
    private decimal MontoPorCuota { get; set; }
    private int CuotasRestantes { get; set; }
    private DateTime FechaDeSolicitud { get; set; }
    private DateTime FechaDeCancelacion { get; set; }
    private IList<Pago> PagosRealizados { get; set; }

    public static CreditoTestBuilder Builder()
    {
        return new();
    }

    public CreditoTestBuilder ConId(string id)
    {
        Id = id;
        return this;
    }

    public CreditoTestBuilder ConMonto(decimal monto)
    {
        Monto = monto;
        return this;
    }

    public CreditoTestBuilder ConEstado(EstadosDeCredito estado)
    {
        Estado = estado;
        return this;
    }

    public CreditoTestBuilder ConPlazoEnMeses(int plazoEnMeses)
    {
        PlazoEnMeses = plazoEnMeses;
        return this;
    }

    public CreditoTestBuilder ConMontoTotalDeIntereses(decimal montoTotalDeIntereses)
    {
        MontoTotalDeIntereses = montoTotalDeIntereses;
        return this;
    }

    public CreditoTestBuilder ConMontoPorCuota(decimal montoPorCuota)
    {
        MontoPorCuota = montoPorCuota;
        return this;
    }

    public CreditoTestBuilder ConCuotasRestantes(int cuotasRestantes)
    {
        CuotasRestantes = cuotasRestantes;
        return this;
    }

    public CreditoTestBuilder ConFechaDeSolicitud(DateTime fechaDeSolicitud)
    {
        FechaDeSolicitud = fechaDeSolicitud;
        return this;
    }

    public CreditoTestBuilder ConFechaDeCancelacion(DateTime fechaDeCancelacion)
    {
        FechaDeCancelacion = fechaDeCancelacion;
        return this;
    }

    public CreditoTestBuilder ConPagosRealizados(IList<Pago> pagos)
    {
        PagosRealizados = pagos;
        return this;
    }

    public Credito Build() => new()
    {
        Id = Id,
        Monto = Monto,
        Estado = Estado,
        PlazoEnMeses = PlazoEnMeses,
        MontoTotalDeIntereses = MontoTotalDeIntereses,
        MontoPorCuota = MontoPorCuota,
        CuotasRestantes = CuotasRestantes,
        FechaDeSolicitud = FechaDeSolicitud,
        FechaDeCancelacion = FechaDeCancelacion,
        PagosRealizados = PagosRealizados,
    };
}