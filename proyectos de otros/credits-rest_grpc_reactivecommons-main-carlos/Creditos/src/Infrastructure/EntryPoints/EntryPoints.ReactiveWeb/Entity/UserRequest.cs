using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoints.ReactiveWeb.Entity
{
    /// <summary>
    /// userRequest
    /// </summary>
    public class UserRequest
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public User AsEntity()
        {
            return new(Name, Email);
        }

        public UserRequest(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}
