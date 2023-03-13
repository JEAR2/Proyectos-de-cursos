using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoints.ReactiveWeb.Entities
{
    public class CrearCreditoRequest
    {
        public string IdCliente { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; }
        public int Cuotas { get; set; }

        public CrearCreditoRequest(string idCliente, decimal monto, string concepto, int cuotas)
        {
            IdCliente = idCliente;
            Monto = monto;
            Concepto = concepto;
            Cuotas = cuotas;
        }
    }
}