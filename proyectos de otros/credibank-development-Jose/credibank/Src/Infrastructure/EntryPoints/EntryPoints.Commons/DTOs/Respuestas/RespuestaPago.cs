using System;

namespace EntryPoints.Commons.DTOs.Respuestas;

public class RespuestaPago
{
    public string Id { get; set; }
    public decimal Monto { get; set; }
    public int CuotasACancelar { get; set; }
    public DateTime FechaDeCancelacion { get; set; }
}