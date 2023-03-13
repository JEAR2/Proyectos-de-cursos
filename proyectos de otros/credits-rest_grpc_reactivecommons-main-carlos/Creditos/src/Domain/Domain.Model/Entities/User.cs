using System.Collections.Generic;

namespace Domain.Model.Entities
{   /// <summary>
    /// User class
    /// </summary>
    public class User
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
        public ICollection<Credit> Credits { get; set; }
        /// <summary>
        /// Constructor without id
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="credits"></param>
        public User(string name, string email, ICollection<Credit> credits)
        {
            Name = name;
            Email = email;
            Credits = credits;
        }
        /// <summary>
        /// Constructor with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="credits"></param>
        public User(string id, string name, string email, ICollection<Credit> credits)
        {
            Id = id;
            Name = name;
            Email = email;
            Credits = credits;
        }
        /// <summary>
        /// ctor without coll and id
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        public User(string name, string email)
        {
            Name = name;
            Email = email;
            Credits = new List<Credit>();  
        }

        /// <summary>
        /// ctor without coll and id
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="id"></param>
        public User(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            Credits = new List<Credit>();
        }

        /// <summary>
        /// Add a item to list
        /// </summary>
        /// <param name="credit"></param>
        public void AddCredit(Credit credit)
        {
            Credits.Add(credit);
        }
        /// <summary>
        /// Delete credit from user
        /// </summary>
        public void DeleteCredit(Credit credit) 
        {
            Credits.Remove(credit);
        }
        /// <summary>
        /// Update Email
        /// </summary>
        /// <param name="newEmail"></param>
        public void UpdateEmail(string newEmail) 
        {
            Email= newEmail;
        }
        /// <summary>
        /// Add a colletion of credits
        /// </summary>
        /// <param name="credits"></param>
        public void AddCredits(ICollection<Credit> credits)
        {
            Credits = credits;
        }
    }
}
