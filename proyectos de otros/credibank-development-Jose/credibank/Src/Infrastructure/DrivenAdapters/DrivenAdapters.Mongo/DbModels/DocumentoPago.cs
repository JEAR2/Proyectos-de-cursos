using System;

using DrivenAdapters.Mongo.DbModels.Base;

using MongoDB.Bson.Serialization.Attributes;

namespace DrivenAdapters.Mongo.DbModels;

public class DocumentoPago : DocumentoBase
{
    [BsonElement("monto")]
    public decimal Monto { get; set; }

    [BsonElement("cuotasACancelar")]
    public int CuotasACancelar { get; set; }

    [BsonElement("fechaDeCancelacion")]
    public DateTime FechaDeCancelacion { get; set; }
}