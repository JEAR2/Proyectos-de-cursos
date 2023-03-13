using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Users
{
    /// <summary>
    /// Interface User use case
    /// </summary>
    public interface IUserUseCase
    {
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> CreateUser(User user);
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        Task<ICollection<User>> GetUsers();
        /// <summary>
        /// Get an user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetUserCredits(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        Task<User> PayInstallment(string creditId);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="creditId"></param>
        /// <returns></returns>
        Task<User> GetCreditState(string userId, string creditId);
        /// <summary>
        /// Assign a credit
        /// </summary>
        /// <param name="credit"></param>
        /// <returns></returns>
        Task<User> AssignCredit(Credit credit);

    }
}
