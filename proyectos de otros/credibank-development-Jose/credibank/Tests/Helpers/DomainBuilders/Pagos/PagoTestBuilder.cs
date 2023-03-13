using Domain.Model.Entities;

namespace Helpers.Domain.Pagos;

public class PagoTestBuilder
{
    private string Id { get; set; }
    private decimal Monto { get; set; }
    private int CuotasACancelar { get; set; }
    private DateTime FechaDeCancelacion { get; set; }

    public static PagoTestBuilder Builder() => new();

    public PagoTestBuilder ConId(string id)
    {
        Id = id;
        return this;
    }

    public PagoTestBuilder ConMonto(decimal monto)
    {
        Monto = monto;
        return this;
    }

    public PagoTestBuilder ConCuotasACancelar(int cuotasACancelar)
    {
        CuotasACancelar = cuotasACancelar;
        return this;
    }

    public PagoTestBuilder ConFechaDeCancelacion(DateTime fechaDeCancelacion)
    {
        FechaDeCancelacion = fechaDeCancelacion;
        return this;
    }

    public Pago Build() => new()
    {
        Id = Id,
        Monto = Monto,
        CuotasACancelar = CuotasACancelar,
        FechaDeCancelacion = FechaDeCancelacion
    };
}