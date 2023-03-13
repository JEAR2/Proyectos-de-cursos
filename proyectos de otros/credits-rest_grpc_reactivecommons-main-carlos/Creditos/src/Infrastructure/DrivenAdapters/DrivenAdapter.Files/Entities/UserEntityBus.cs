using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapter.ServiceBus.Entities
{
    public class UserEntityBus
    {
        /// <summary>
        /// User id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Credits in user
        /// </summary>
        public ICollection<CreditEntityBus> Credits { get; set; }

        public UserEntityBus(string id, string name, string email, ICollection<CreditEntityBus> credits)
        {
            Id = id;
            Name = name;
            Email = email;
            Credits = credits;
        }
    }
}
