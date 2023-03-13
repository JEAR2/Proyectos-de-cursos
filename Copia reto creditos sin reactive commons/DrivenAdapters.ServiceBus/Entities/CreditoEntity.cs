namespace DrivenAdapters.ServiceBus.Entities
{
    public class CreditoEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Concepto
        /// </summary>
        public string Concepto { get; set; }

        /// <summary>
        /// Monto
        /// </summary>
        public decimal Monto { get; set; }

        /// <summary>
        /// MontoConInteres
        /// </summary>
        public decimal MontoConInteres { get; set; }

        /// <summary>
        /// Interés
        /// </summary>
        public decimal Interes { get; set; }

        /// <summary>
        /// Cuotas
        /// </summary>
        public int Cuotas { get; set; }

        /// <summary>
        /// ValorCuota
        /// </summary>
        public decimal ValorCuota { get; set; }

        /// <summary>
        /// CuotasPagadas
        /// </summary>
        public int CuotasPagadas { get; set; }

        /// <summary>
        /// CoutasPendientes
        /// </summary>
        public int CuotasPendientes { get; set; }

        /// <summary>
        /// Saldo
        /// </summary>
        public decimal Saldo { get; set; }

        /// <summary>
        /// FechaInicio
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// FechaFin
        /// </summary>
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// FechaProximaCuota
        /// </summary>
        public DateTime FechaProximaCuota { get; set; }

        /// <summary>
        /// Constructor con todos los parámetros
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
        public CreditoEntity(string idCredito, string concepto, decimal monto, int cuotas, decimal valorCuota, int cuotasPagadas, int cuotasPendientes, decimal saldo, DateTime fechaInicio, DateTime fechaFin, DateTime fechaProximaCuota)
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
    }
}