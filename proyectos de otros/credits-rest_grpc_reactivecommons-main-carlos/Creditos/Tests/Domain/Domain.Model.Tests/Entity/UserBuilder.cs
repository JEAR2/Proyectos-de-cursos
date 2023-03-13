using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Tests.Entity
{
    /// <summary>
    /// Builder
    /// </summary>
    public class UserBuilder
    {
        private string _id = string.Empty;

        private string _name = string.Empty;

        private string _email = string.Empty;

        private ICollection<Credit> _credits = new List<Credit>();
        

        public UserBuilder() { }

        public User Build() => new(
            _id, _name, _email, _credits);

        public UserBuilder WithId(string id)
        {
            _id = id;
            return this;
        }
        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }
        public UserBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public UserBuilder WithCredits(ICollection<Credit> credits)
        {
            _credits = credits;
            return this;    
        }

    }
}
