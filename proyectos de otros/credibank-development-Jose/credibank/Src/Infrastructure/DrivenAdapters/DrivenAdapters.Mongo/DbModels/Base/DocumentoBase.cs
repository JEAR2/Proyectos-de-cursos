using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DrivenAdapters.Mongo.DbModels.Base;

public abstract class DocumentoBase
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}