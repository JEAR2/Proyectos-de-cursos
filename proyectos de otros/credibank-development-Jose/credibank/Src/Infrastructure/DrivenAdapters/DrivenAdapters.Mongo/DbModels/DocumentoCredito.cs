using System;
using System.Collections.Generic;

using Domain.Model.Entities;

using DrivenAdapters.Mongo.DbModels.Base;

using MongoDB.Bson.Serialization.Attributes;

namespace DrivenAdapters.Mongo.DbModels;

public class DocumentoCredito : DocumentoBase
{
    [BsonElement("monto")]
    public decimal Monto { get; set; }

    [BsonElement("estado")]
    public EstadosDeCredito Estado { get; set; }

    [BsonElement("plazoEnMeses")]
    public int PlazoEnMeses { get; set; }

    [BsonElement("montoTotalDeIntereses")]
    public decimal MontoTotalDeIntereses { get; set; }

    [BsonElement("montoPorCuota")]
    public decimal MontoPorCuota { get; set; }

    [BsonElement("cuotasRestantes")]
    public int CuotasRestantes { get; set; }

    [BsonElement("fechaDeSolicitud")]
    public DateTime FechaDeSolicitud { get; set; }

    [BsonElement("fechaDeCancelacion")]
    public DateTime? FechaDeCancelacion { get; set; }

    [BsonElement("pagosRealizados")]
    public IList<DocumentoPago> PagosRealizados { get; set; }
}