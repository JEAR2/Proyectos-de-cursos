using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DrivenAdapters.Mongo.Entities.Base
{
    /// <summary>
    /// Entity base
    /// </summary>
    public class EntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}