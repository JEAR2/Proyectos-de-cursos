using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// Interface of credits repo
    /// </summary>
    public interface ICreditRepository
    {
        /// <summary>
        /// Create a new credit
        /// </summary>
        /// <param name="credit"></param>
        /// <returns></returns>
        Task<Credit> CreateCreditAsync(Credit credit);
        /// <summary>
        /// Get all credits with user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Credit>> GetAllCreditsWithUserId(string userId);
        /// <summary>
        /// Get a credit by id
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        Task<Credit> GetCreditById(string creditId);
        /// <summary>
        /// Update a credit
        /// </summary>
        /// <param name="credit"></param>
        /// <returns></returns>
        Task<Credit> UpdateCreditAsync(Credit credit);
    }
}
