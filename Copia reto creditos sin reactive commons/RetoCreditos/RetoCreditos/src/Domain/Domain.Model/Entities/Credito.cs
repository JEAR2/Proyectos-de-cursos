using System;

namespace Domain.Model.Entities
{
    /// <summary>
    /// Clase Crédito
    /// </summary>
    public class Credito
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Concepto
        /// </summary>
        public string Concepto { get; private set; }

        /// <summary>
        /// Monto
        /// </summary>
        public decimal Monto { get; private set; }

        /// <summary>
        /// MontoConInteres
        /// </summary>
        public decimal MontoConInteres { get; private set; }

        /// <summary>
        /// Interés
        /// </summary>
        public decimal Interes { get; private set; }

        /// <summary>
        /// Cuotas
        /// </summary>
        public int Cuotas { get; private set; }

        /// <summary>
        /// ValorCuota
        /// </summary>
        public decimal ValorCuota { get; private set; }

        /// <summary>
        /// CuotasPagadas
        /// </summary>
        public int CuotasPagadas { get; private set; }

        /// <summary>
        /// CoutasPendientes
        /// </summary>
        public int CuotasPendientes { get; private set; }

        /// <summary>
        /// Saldo
        /// </summary>
        public decimal Saldo { get; private set; }

        /// <summary>
        /// FechaInicio
        /// </summary>
        public DateTime FechaInicio { get; private set; }

        /// <summary>
        /// FechaFin
        /// </summary>
        public DateTime FechaFin { get; private set; }

        /// <summary>
        /// FechaProximaCuota
        /// </summary>
        public DateTime FechaProximaCuota { get; private set; }

        /// <summary>
        /// Constructor con todos los parámetros
        /// </summary>
        /// <param name="id"></param>
        /// <param name="concepto"></param>
        /// <param name="monto"></param>
        /// <param name="montoConInteres"></param>
        /// <param name="interes"></param>
        /// <param name="cuotas"></param>
        /// <param name="valorCuota"></param>
        /// <param name="cuotasPagadas"></param>
        /// <param name="cuotasPendientes"></param>
        /// <param name="saldo"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="fechaProximaCuota"></param>
        public Credito(string id, string concepto, decimal monto, decimal montoConInteres, decimal interes, int cuotas, decimal valorCuota, int cuotasPagadas, int cuotasPendientes, decimal saldo, DateTime fechaInicio, DateTime fechaFin, DateTime fechaProximaCuota)
        {
            Id = id;
            Concepto = concepto;
            Monto = monto;
            MontoConInteres = montoConInteres;
            Interes = interes;
            Cuotas = cuotas;
            ValorCuota = valorCuota;
            CuotasPagadas = cuotasPagadas;
            CuotasPendientes = cuotasPendientes;
            Saldo = saldo;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            FechaProximaCuota = fechaProximaCuota;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="concepto"></param>
        /// <param name="monto"></param>
        /// <param name="cuotasPagadas"></param>
        /// <param name="cuotasPendientes"></param>
        /// <param name="saldo"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="fechaProximaCuota"></param>
        public Credito(string id, string concepto, decimal monto, int cuotasPagadas, int cuotasPendientes, decimal saldo, DateTime fechaInicio, DateTime fechaFin, DateTime fechaProximaCuota)
        {
            Id = id;
            Concepto = concepto;
            Monto = monto;
            CuotasPagadas = cuotasPagadas;
            CuotasPendientes = cuotasPendientes;
            Saldo = saldo;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            FechaProximaCuota = fechaProximaCuota;
        }

        /// <summary>
        /// Constructor sin id
        /// </summary>
        /// <param name="concepto"></param>
        /// <param name="monto"></param>
        /// <param name="cuotas"></param>
        /// <param name="cuotasPagadas"></param>
        /// <param name="cuotasPendientes"></param>
        /// <param name="saldo"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="fechaProximaCuota"></param>
        public Credito(string concepto, decimal monto, int cuotas, int cuotasPagadas, int cuotasPendientes, decimal saldo, DateTime fechaInicio, DateTime fechaFin, DateTime fechaProximaCuota)
        {
            Concepto = concepto;
            Monto = monto;
            Cuotas = cuotas;
            CuotasPagadas = cuotasPagadas;
            CuotasPendientes = cuotasPendientes;
            Saldo = saldo;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            FechaProximaCuota = fechaProximaCuota;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="concepto"></param>
        /// <param name="cuotas"></param>
        /// <param name="monto"></param>
        public Credito(string id, decimal monto, string concepto, int cuotas, decimal interes)
        {
            Id = id;
            Concepto = concepto;
            Monto = monto;
            Interes = interes;
            MontoConInteres = AplicarInteres(interes);
            Saldo = MontoConInteres;
            Cuotas = cuotas;
            CuotasPendientes = cuotas;
            ValorCuota = MontoConInteres / cuotas;
            FechaInicio = DateTime.Now;
            FechaProximaCuota = FechaInicio.AddMonths(1);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="concepto"></param>
        /// <param name="cuotas"></param>
        /// <param name="monto"></param>
        /// <param name="interes"></param>
        public Credito(decimal monto, string concepto, int cuotas, decimal interes)
        {
            Concepto = concepto;
            Monto = monto;
            Interes = interes;
            MontoConInteres = AplicarInteres(interes);
            Cuotas = cuotas;
            CuotasPendientes = cuotas;
            Saldo = MontoConInteres;
            ValorCuota = MontoConInteres / cuotas;
            FechaInicio = DateTime.Now;
            FechaProximaCuota = FechaInicio.AddMonths(1);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idCredito"></param>
        /// <param name="concepto"></param>
        /// <param name="monto"></param>
        /// <param name="cuotas"></param>
        /// <param name="cuotasPagadas"></param>
        /// <param name="cuotasPendientes"></param>
        /// <param name="saldo"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="fechaProximaCuota"></param>
        public Credito(string idCredito, string concepto, decimal monto, int cuotas, int cuotasPagadas, int cuotasPendientes, decimal saldo, DateTime fechaInicio, DateTime fechaFin, DateTime fechaProximaCuota)
        {
            Id = idCredito;
            Concepto = concepto;
            Monto = monto;
            Cuotas = cuotas;
            CuotasPagadas = cuotasPagadas;
            CuotasPendientes = cuotasPendientes;
            Saldo = saldo;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            FechaProximaCuota = fechaProximaCuota;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idCredito"></param>
        /// <param name="concepto"></param>
        /// <param name="monto"></param>
        /// <param name="cuotas"></param>
        /// <param name="valorCuota"></param>
        /// <param name="cuotasPagadas"></param>
        /// <param name="cuotasPendientes"></param>
        /// <param name="saldo"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="fechaProximaCuota"></param>
        public Credito(string idCredito, string concepto, decimal monto, int cuotas, decimal valorCuota, int cuotasPagadas, int cuotasPendientes, decimal saldo, DateTime fechaInicio, DateTime fechaFin, DateTime fechaProximaCuota)
        {
            Id = idCredito;
            Concepto = concepto;
            Monto = monto;
            Cuotas = cuotas;
            ValorCuota = valorCuota;
            CuotasPagadas = cuotasPagadas;
            CuotasPendientes = cuotasPendientes;
            Saldo = saldo;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            FechaProximaCuota = fechaProximaCuota;
        }

        /// <summary>
        /// Método para cancelar la cuota del crédito
        /// </summary>
        public void CancelarCuota()
        {
            if (CuotasPendientes > 0)
            {
                if (CuotasPendientes == 1)
                {
                    CuotasPagadas = Cuotas;
                    CuotasPendientes = 0;
                    Saldo = 0;
                    FechaProximaCuota = DateTime.Now;
                    FechaFin = DateTime.Now;
                }
                else
                {
                    CuotasPagadas += 1;
                    CuotasPendientes -= 1;
                    Saldo -= ValorCuota;
                    FechaProximaCuota = FechaProximaCuota.AddMonths(1);
                }
            }
        }

        /// <summary>
        /// Método para retornar el valor de la cuota según la cantidad de cuotas
        /// </summary>
        /// <param name="coutas"></param>
        /// <returns></returns>
        public decimal CalcularValorCuota(int coutas)
        {
            decimal ret = MontoConInteres / coutas;
            return ret;
        }

        /// <summary>
        /// Método para aplicar intereses a un crédito
        /// </summary>
        /// <param name="interes"></param>
        private decimal AplicarInteres(decimal interes)
        {
            return MontoConInteres = Monto * interes;
        }
    }
}