using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities.Gateway
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserEventRepository
    {
        /// <summary>
        /// Command to send an email notification
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AssignedCreditEmailNotification(User user);
        /// <summary>
        /// Notify user created
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task NotifyUserCreated(User user);
    
    }
}
