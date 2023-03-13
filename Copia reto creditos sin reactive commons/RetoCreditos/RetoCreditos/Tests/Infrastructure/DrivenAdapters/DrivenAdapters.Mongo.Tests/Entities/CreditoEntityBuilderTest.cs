using DrivenAdapters.Mongo.Entities;

namespace DrivenAdapters.Mongo.Tests.Entities
{
    public class CreditoEntityBuilderTest
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

        public CreditoEntity Build() => new(_id, _concepto, _monto, _montoConInteres, _interes, _cuotas, _valorCuota, _cuotasPagadas, _cuotasPendientes, _saldo, _fechaInicio, _fechaFin, _fechaProximaCuota);

        public CreditoEntityBuilderTest ConId(string id)
        {
            _id = id;
            return this;
        }

        public CreditoEntityBuilderTest ConConcepto(string concepto)
        {
            _concepto = concepto;
            return this;
        }

        public CreditoEntityBuilderTest ConMonto(decimal monto)
        {
            _monto = monto;
            return this;
        }

        public CreditoEntityBuilderTest ConMontoConInteres(decimal montoConInteres)
        {
            _montoConInteres = montoConInteres;
            return this;
        }

        public CreditoEntityBuilderTest ConInteres(decimal interes)
        {
            _interes = interes;
            return this;
        }

        public CreditoEntityBuilderTest ConCuotas(int cuotas)
        {
            _cuotas = cuotas;
            return this;
        }

        public CreditoEntityBuilderTest ConValorCuota(decimal valorCuota)
        {
            _valorCuota = valorCuota;
            return this;
        }

        public CreditoEntityBuilderTest ConCuotasPagadas(int cuotasPagadas)
        {
            _cuotasPagadas = cuotasPagadas;
            return this;
        }

        public CreditoEntityBuilderTest ConCuotasPendientes(int cuotasPendientes)
        {
            _cuotasPendientes = cuotasPendientes;
            return this;
        }

        public CreditoEntityBuilderTest ConSaldo(decimal saldo)
        {
            _saldo = saldo;
            return this;
        }

        public CreditoEntityBuilderTest ConFechaInicio(DateTime fechaInicio)
        {
            _fechaInicio = fechaInicio;
            return this;
        }

        public CreditoEntityBuilderTest ConFechaFin(DateTime fechaFin)
        {
            _fechaFin = fechaFin;
            return this;
        }

        public CreditoEntityBuilderTest ConFechaproximaCuota(DateTime fechaProximaCuota)
        {
            _fechaProximaCuota = fechaProximaCuota;
            return this;
        }
    }
}