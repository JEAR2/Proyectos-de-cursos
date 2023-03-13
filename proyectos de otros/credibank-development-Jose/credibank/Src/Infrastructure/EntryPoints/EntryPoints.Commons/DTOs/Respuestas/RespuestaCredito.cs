using Domain.Model.Entities;

namespace EntryPoints.Commons.DTOs.Respuestas;

public class RespuestaCredito
{
    public string Id { get; set; }
    public decimal Monto { get; set; }
    public EstadosDeCredito Estado { get; set; }
    public int PlazoEnMeses { get; set; }
    public decimal MontoTotalDeIntereses { get; set; }
    public decimal MontoPorCuota { get; set; }
    public int CuotasRestantes { get; set; }
    public DateTime FechaDeSolicitud { get; set; }
    public DateTime? FechaDeCancelacion { get; set; }
    public IList<RespuestaPago> PagosRealizados { get; set; }
}