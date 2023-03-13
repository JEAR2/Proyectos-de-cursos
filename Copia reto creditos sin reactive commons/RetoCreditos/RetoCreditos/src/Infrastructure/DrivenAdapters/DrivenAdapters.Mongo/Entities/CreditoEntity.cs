using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DrivenAdapters.Mongo.Entities
{
    /// <summary>
    /// Clase CreditoEntity
    /// </summary>
    public class CreditoEntity : EntityBase, IDomainEntity<Credito>
    {
        /// <summary>
        /// Concepto
        /// </summary>
        [BsonElement("concepto")]
        public string Concepto { get; set; }

        /// <summary>
        /// Monto
        /// </summary>
        [BsonElement("monto")]
        public decimal Monto { get; set; }

        /// <summary>
        /// MontoConInteres
        /// </summary>
        [BsonElement("montoConInteres")]
        public decimal MontoConInteres { get; set; }

        /// <summary>
        /// Interés
        /// </summary>
        [BsonElement("interes")]
        public decimal Interes { get; set; }

        /// <summary>
        /// Cuotas
        /// </summary>
        [BsonElement("cuotas")]
        public int Cuotas { get; set; }

        /// <summary>
        /// ValorCuota
        /// </summary>
        [BsonElement("valorCuota")]
        public decimal ValorCuota { get; set; }

        /// <summary>
        /// CuotasPagadas
        /// </summary>
        [BsonElement("cuotasPagadas")]
        public int CuotasPagadas { get; set; }

        /// <summary>
        /// CoutasPendientes
        /// </summary>
        [BsonElement("cuotasPendientes")]
        public int CuotasPendientes { get; set; }

        /// <summary>
        /// Saldo
        /// </summary>
        [BsonElement("saldo")]
        public decimal Saldo { get; set; }

        /// <summary>
        /// FechaInicio
        /// </summary>
        [BsonElement("fechaInicio")]
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// FechaFin
        /// </summary>
        [BsonElement("fechaFin")]
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// FechaProximaCuota
        /// </summary>
        [BsonElement("fechaProximaCuota")]
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="concepto"></param>
        /// <param name="cuotas"></param>
        /// <param name="monto"></param>
        public CreditoEntity(decimal monto, string concepto, int cuotas, decimal interes)
        {
            Concepto = concepto;
            Monto = monto;
            MontoConInteres = Monto * interes;
            Interes = interes;
            Saldo = MontoConInteres;
            Cuotas = cuotas;
            CuotasPendientes = cuotas;
            ValorCuota = Monto / cuotas;
            FechaInicio = DateTime.Now;
            FechaProximaCuota = FechaInicio.AddMonths(1);
        }

        /// <summary>
        /// Constructor par         /// </summary>
        /// <param name="idCredito"></param>
        /// <param name="concepto"></param>
        /// <param name="monto"></param>
        /// <param name="cuotasPagadas"></param>
        /// <param name="cuotasPendientes"></param>
        /// <param name="saldo"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="fechaProximaCuota"></param>
        public CreditoEntity(string idCredito, string concepto, decimal monto, int cuotasPagadas, int cuotasPendientes, decimal saldo, DateTime fechaInicio, DateTime fechaFin, DateTime fechaProximaCuota)
        {
            Id = idCredito;
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
        /// Constructor
        /// </summary>
        /// <param name="concepto"></param>
        /// <param name="monto"></param>
        /// <param name="montoConInteres"></param>
        /// <param name="interes"></param>
        /// <param name="cuotasPagadas"></param>
        /// <param name="cuotasPendientes"></param>
        /// <param name="saldo"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="fechaProximaCuota"></param>
        public CreditoEntity(string concepto, decimal monto, decimal montoConInteres, decimal interes, int cuotas, int cuotasPagadas, int cuotasPendientes, decimal saldo, DateTime fechaInicio, DateTime fechaFin, DateTime fechaProximaCuota)
        {
            Concepto = concepto;
            Monto = monto;
            MontoConInteres = montoConInteres;
            Interes = interes;
            Cuotas = cuotas;
            CuotasPagadas = cuotasPagadas;
            CuotasPendientes = cuotasPendientes;
            Saldo = saldo;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            FechaProximaCuota = fechaProximaCuota;
        }

        public CreditoEntity(string id, string concepto, decimal monto, decimal montoConInteres, decimal interes, int cuotas, decimal valorCuota, int cuotasPagadas, int cuotasPendientes, decimal saldo, DateTime fechaInicio, DateTime fechaFin, DateTime fechaProximaCuota)
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

        public Credito AsEntity()
        {
            return new Credito(Id, Monto, Concepto, Cuotas, Interes);
        }
    }
}