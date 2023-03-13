using System;
using System.Collections.Generic;

using Domain.Model.Entities.Base;

namespace Domain.Model.Entities;

public class Credito : EntidadBase
{
    public decimal Monto { get; set; }
    public EstadosDeCredito Estado { get; set; }
    public int PlazoEnMeses { get; set; }
    public decimal MontoTotalDeIntereses { get; set; }
    public decimal MontoPorCuota { get; set; }
    public int CuotasRestantes { get; set; }
    public DateTime FechaDeSolicitud { get; set; }
    public DateTime? FechaDeCancelacion { get; set; }
    public IList<Pago> PagosRealizados { get; set; }

    public decimal CalcularMontoTotalDeIntereses(decimal tasaDeInteresEfectivoAnual)
    {
        // (1 + tasa de interés efectivo anual) ^ (meses a pagar / 12) - 1
        decimal tasaDeInteres = (decimal)Math.Pow(
            decimal.ToDouble(1 + tasaDeInteresEfectivoAnual),
            PlazoEnMeses / 12.0D
        ) - 1.0M;
        return Monto * tasaDeInteres;
    }

    public void PagarCuota(Pago pago)
    {
        CuotasRestantes -= pago.CuotasACancelar;
        //PagosRealizados.Add(pago);

        if (CuotasRestantes == 0) CambiarEstado(EstadosDeCredito.CANCELADO);
    }

    public void CambiarEstado(EstadosDeCredito nuevoEstado)
    {
        Estado = nuevoEstado;
    }
}