using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// ITestEntityRepository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// FindAll
        /// </summary>
        /// <returns>Entity list</returns>
        Task<ICollection<User>> FindAllAsync();
        /// <summary>
        /// Find a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> FindByIdAsync(string id);
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> CreateUserAsync(User user);

    }
}