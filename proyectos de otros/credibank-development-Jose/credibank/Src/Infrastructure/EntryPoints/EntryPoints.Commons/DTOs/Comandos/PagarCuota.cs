using System;

namespace EntryPoints.Commons.DTOs.Comandos;

public class PagarCuota
{
    public int CuotasACancelar { get; set; }
    public DateTime FechaDeCancelacion { get; set; }
}