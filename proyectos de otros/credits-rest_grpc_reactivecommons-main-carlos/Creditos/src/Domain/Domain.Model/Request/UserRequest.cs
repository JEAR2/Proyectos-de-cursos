using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request
{
    /// <summary>
    /// User request for commands and events
    /// </summary>
    public class UserRequest
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
        public ICollection<CreditRequest> Credits { get; set; }
    }
}
