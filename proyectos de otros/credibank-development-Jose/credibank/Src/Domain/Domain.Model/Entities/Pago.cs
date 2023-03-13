using System;

using Domain.Model.Entities.Base;

namespace Domain.Model.Entities;

public class Pago : EntidadBase
{
    public decimal Monto { get; set; }
    public int CuotasACancelar { get; set; }
    public DateTime FechaDeCancelacion { get; set; }
}