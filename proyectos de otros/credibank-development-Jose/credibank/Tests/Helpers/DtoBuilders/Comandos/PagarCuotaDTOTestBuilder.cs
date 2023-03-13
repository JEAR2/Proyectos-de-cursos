using EntryPoints.Commons.DTOs.Comandos;

namespace Helpers.DtoBuilders.Comandos;

public class PagarCuotaDTOTestBuilder
{
    private PagarCuota _pago = new();

    public PagarCuotaDTOTestBuilder ConCuotasACancelar(int cuotasACancelar)
    {
        _pago.CuotasACancelar = cuotasACancelar;
        return this;
    }

    public PagarCuotaDTOTestBuilder ConFechaDeCancelacion(DateTime fechaDeCancelacion)
    {
        _pago.FechaDeCancelacion = fechaDeCancelacion;
        return this;
    }

    public PagarCuota Build() => _pago;
}