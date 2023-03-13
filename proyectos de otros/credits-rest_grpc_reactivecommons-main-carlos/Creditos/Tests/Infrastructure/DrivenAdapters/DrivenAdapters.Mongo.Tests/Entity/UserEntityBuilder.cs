using DrivenAdapters.Mongo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo.Tests.Entity
{
    public class UserEntityBuilder
    {
        private string _id = string.Empty;

        private string _name = string.Empty;

        private string _email = string.Empty;

        public UserEntityBuilder() { }

        public UserEntity Build() => new(
            _id, _name, _email);

        public UserEntityBuilder WithId(string id)
        {
            _id = id;
            return this;
        }
        public UserEntityBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }
        public UserEntityBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

    }
}
