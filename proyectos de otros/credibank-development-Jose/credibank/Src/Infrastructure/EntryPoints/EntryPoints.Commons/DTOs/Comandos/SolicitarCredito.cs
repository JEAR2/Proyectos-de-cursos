using System;

namespace EntryPoints.Commons.DTOs.Comandos;

public class SolicitarCredito
{
    public decimal Monto { get; set; }
    public int PlazoEnMeses { get; set; }
    public DateTime FechaDeSolicitud { get; set; }
}