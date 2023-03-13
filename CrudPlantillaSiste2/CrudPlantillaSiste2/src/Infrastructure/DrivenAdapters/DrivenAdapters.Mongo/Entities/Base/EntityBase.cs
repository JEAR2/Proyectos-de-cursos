using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DrivenAdapters.Mongo.Entities
{
    /// <summary>
    /// Entity
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// Id
        /// </summary>
        ///
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}