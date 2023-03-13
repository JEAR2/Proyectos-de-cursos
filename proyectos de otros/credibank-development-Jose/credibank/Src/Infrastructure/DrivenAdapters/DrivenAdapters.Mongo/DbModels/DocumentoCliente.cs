using System.Collections.Generic;

using DrivenAdapters.Mongo.DbModels.Base;

using MongoDB.Bson.Serialization.Attributes;

namespace DrivenAdapters.Mongo.DbModels;

public class DocumentoCliente : DocumentoBase
{
    [BsonElement("nombre")]
    public string Nombre { get; set; }

    [BsonElement("apellido")]
    public string Apellido { get; set; }

    [BsonElement("correoElectronico")]
    public string? CorreoElectronico { get; set; }

    [BsonElement("cedulaDeCiudadania")]
    public string DocumentoDeIdentidad { get; set; }

    [BsonElement("numeroDeCelular")]
    public string NumeroDeCelular { get; set; }

    [BsonElement("creditos")]
    public IList<DocumentoCredito> Creditos { get; set; }
}