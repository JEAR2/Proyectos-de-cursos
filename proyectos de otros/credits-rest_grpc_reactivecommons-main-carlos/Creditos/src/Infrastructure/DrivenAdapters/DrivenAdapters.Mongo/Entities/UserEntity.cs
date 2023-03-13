using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo.Entities
{
    /// <summary>
    /// User mongo entity
    /// </summary>
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// User name
        /// </summary>
        [BsonElement(elementName: "nombre")]
        public string Name { get; set; }
        /// <summary>
        /// User email
        /// </summary>
        [BsonElement(elementName: "email")]
        public string Email { get; set; }
        /// <summary>
        /// Constructor without entityId
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        public UserEntity(string name, string email)
        {
            Name = name;
            Email = email;
        }
        /// <summary>
        /// Constructor with entityId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        public UserEntity(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
        /// <summary>
        /// Convert to entity
        /// </summary>
        /// <returns></returns>
        public User AsEntity()
        {
            return new(Id, Name, Email);
        }

    }
}
