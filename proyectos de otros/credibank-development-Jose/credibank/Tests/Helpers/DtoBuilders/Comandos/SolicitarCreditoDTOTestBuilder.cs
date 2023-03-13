using EntryPoints.Commons.DTOs.Comandos;

namespace Helpers.DtoBuilders.Comandos;

public class SolicitarCreditoDTOTestBuilder
{
    private SolicitarCredito _credito = new();

    public SolicitarCreditoDTOTestBuilder ConMonto(decimal monto)
    {
        _credito.Monto = monto;
        return this;
    }

    public SolicitarCreditoDTOTestBuilder ConPlazoEnMeses(int plazoEnMeses)
    {
        _credito.PlazoEnMeses = plazoEnMeses;
        return this;
    }

    public SolicitarCreditoDTOTestBuilder ConFechaDeSolicitud(DateTime fechaDeSolicitud)
    {
        _credito.FechaDeSolicitud = fechaDeSolicitud;
        return this;
    }

    public SolicitarCredito Build() => _credito;
}