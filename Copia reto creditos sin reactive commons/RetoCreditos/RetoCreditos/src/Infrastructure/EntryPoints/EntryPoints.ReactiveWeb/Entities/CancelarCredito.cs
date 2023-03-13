using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoints.ReactiveWeb.Entities
{
    public class CancelarCredito
    {
        public string IdCliente { get; set; }
        public string IdCredito { get; set; }

        public CancelarCredito(string idCliente, string idCredito)
        {
            IdCliente = idCliente;
            IdCredito = idCredito;
        }
    }
}