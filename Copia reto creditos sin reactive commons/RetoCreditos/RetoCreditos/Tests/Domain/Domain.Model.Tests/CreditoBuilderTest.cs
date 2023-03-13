using Domain.Model.Entities;

namespace Domain.Model.Tests
{
    public class CreditoBuilderTest
    {
        private string _id = string.Empty;
        private string _concepto = string.Empty;
        private decimal _monto = 0;
        private decimal _montoConInteres = 0;
        private decimal _interes = 0;
        private int _cuotas = 0;
        private decimal _valorCuota = 0;
        private int _cuotasPagadas = 0;
        private int _cuotasPendientes = 0;
        private decimal _saldo = 0;
        private DateTime _fechaInicio = new DateTime();
        private DateTime _fechaFin = new DateTime();
        private DateTime _fechaProximaCuota = new DateTime();

        public Credito Build() => new(_id, _concepto, _monto, _montoConInteres, _interes, _cuotas, _valorCuota, _cuotasPagadas, _cuotasPendientes, _saldo, _fechaInicio, _fechaFin, _fechaProximaCuota);

        public CreditoBuilderTest ConId(string id)
        {
            _id = id;
            return this;
        }

        public CreditoBuilderTest ConConcepto(string concepto)
        {
            _concepto = concepto;
            return this;
        }

        public CreditoBuilderTest ConMonto(decimal monto)
        {
            _monto = monto;
            return this;
        }

        public CreditoBuilderTest ConMontoConInteres(decimal montoConInteres)
        {
            _montoConInteres = montoConInteres;
            return this;
        }

        public CreditoBuilderTest ConInteres(decimal interes)
        {
            _interes = interes;
            return this;
        }

        public CreditoBuilderTest ConCuotas(int cuotas)
        {
            _cuotas = cuotas;
            return this;
        }

        public CreditoBuilderTest ConValorCuota(decimal valorCuota)
        {
            _valorCuota = valorCuota;
            return this;
        }

        public CreditoBuilderTest ConCuotasPagadas(int cuotasPagadas)
        {
            _cuotasPagadas = cuotasPagadas;
            return this;
        }

        public CreditoBuilderTest ConCuotasPendientes(int cuotasPendientes)
        {
            _cuotasPendientes = cuotasPendientes;
            return this;
        }

        public CreditoBuilderTest ConSaldo(decimal saldo)
        {
            _saldo = saldo;
            return this;
        }

        public CreditoBuilderTest ConFechaInicio(DateTime fechaInicio)
        {
            _fechaInicio = fechaInicio;
            return this;
        }

        public CreditoBuilderTest ConFechaFin(DateTime fechaFin)
        {
            _fechaFin = fechaFin;
            return this;
        }

        public CreditoBuilderTest ConFechaproximaCuota(DateTime fechaProximaCuota)
        {
            _fechaProximaCuota = fechaProximaCuota;
            return this;
        }
    }
}