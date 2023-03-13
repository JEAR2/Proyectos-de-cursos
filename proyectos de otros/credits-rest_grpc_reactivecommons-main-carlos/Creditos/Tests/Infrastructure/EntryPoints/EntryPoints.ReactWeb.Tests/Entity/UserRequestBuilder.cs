using EntryPoints.ReactiveWeb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoints.ReactWeb.Tests.Entity
{
    public class UserRequestBuilder
    {
        private string _email = string.Empty;
        private string _name = string.Empty;

        public UserRequestBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }
        public UserRequestBuilder WithName(string name)
        {
            name = name;
            return this;
        }
        public UserRequest Build() => 
            new (_email, _name);
        
    }
}
