using System;
using System.Collections.Generic;
using System.Linq;

using Domain.Model.Entities.Base;

namespace Domain.Model.Entities;

public class Cliente : EntidadBase
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string CorreoElectronico { get; set; }
    public string DocumentoDeIdentidad { get; set; }
    public string NumeroDeCelular { get; set; }
    public IList<Credito> Creditos { get; set; }

    public Credito SolicitarCredito(Credito creditoASolicitar, decimal tasaDeInteresEfectivoAnual)
    {
        creditoASolicitar.CambiarEstado(EstadosDeCredito.EN_PROCESAMIENTO);
        creditoASolicitar.FechaDeSolicitud = DateTime.Now;
        creditoASolicitar.FechaDeCancelacion = null;
        creditoASolicitar.CuotasRestantes = creditoASolicitar.PlazoEnMeses;
        creditoASolicitar.PagosRealizados = new List<Pago>();
        creditoASolicitar.MontoTotalDeIntereses = creditoASolicitar.CalcularMontoTotalDeIntereses(tasaDeInteresEfectivoAnual);
        creditoASolicitar.MontoPorCuota =
            (creditoASolicitar.Monto + creditoASolicitar.MontoTotalDeIntereses) / creditoASolicitar.PlazoEnMeses;

        Creditos.Add(creditoASolicitar);

        return creditoASolicitar;
    }

    public Credito PagarCuotaDeCredito(string idDelCredito, Pago pago)
    {
        Credito credito = Creditos.First(c => c.Id == idDelCredito);
        credito.PagarCuota(pago);
        return credito;
    }
}