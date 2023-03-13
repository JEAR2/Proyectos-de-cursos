using credinet.exception.middleware.models;
using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.Users
{
    /// <summary>
    /// Implementation User use case
    /// </summary>
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICreditRepository  _creditRepository;
        private readonly IUserEventRepository _userEventRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="creditRepository"></param>
        public UserUseCase(IUserRepository userRepository, ICreditRepository creditRepository, IUserEventRepository userEventRepository)
        {
            _userRepository = userRepository;
            _creditRepository = creditRepository;
            _userEventRepository = userEventRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> CreateUser(User user)
        {
            return await _userRepository.CreateUserAsync(user);
        }

        /// <summary>
        /// Assign a new credit to user
        /// </summary>
        /// <param name="credit"></param>
        /// <returns></returns>
        public async Task<User> AssignCredit(Credit credit)
        {
            if(credit.UserId == "")
            {
                throw new BusinessException("El id del usuario no puede estar vacio", 566);
            }
            var userModel = await _userRepository.FindByIdAsync(credit.UserId);
            if(userModel == null)
            {
                throw new BusinessException($"El usuario con id {credit.UserId} no se encuentra registrado", 566);
            }
            var creditModel = await _creditRepository.CreateCreditAsync(credit);
            
            userModel.AddCredit(creditModel);

            await _userEventRepository.AssignedCreditEmailNotification(userModel);

            await _userEventRepository.NotifyUserCreated(userModel);

            return userModel;

        }

        /// <summary>
        /// Get a user with one of credits state
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User> GetUserCredits(string userId)
        {
            if (userId == "")
            {
                throw new BusinessException("El Id ingresado no puede estar vacio", 566);
            }
            var userModel = await _userRepository.FindByIdAsync(userId);
            var creditsModel = await _creditRepository.GetAllCreditsWithUserId(userId);

            foreach(var credit in creditsModel)
            {
                userModel.AddCredit(credit);
            }
            return userModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<User> GetCreditState(string userId, string creditId)
        {
            if (userId == "")
            {
                throw new BusinessException("El Id ingresado no puede estar vacio", 566);
            }
            var userModel = await _userRepository.FindByIdAsync(userId);
            var creditModel = await _creditRepository.GetCreditById(creditId);

        userModel.AddCredit(creditModel);

            return userModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<User>> GetUsers()
        {

            var users = await _userRepository.FindAllAsync();

            foreach(var user in users)
            {
                var creditsOfUser = await _creditRepository.GetAllCreditsWithUserId(user.Id);
                user.AddCredits(creditsOfUser.Where(c => c.IsActive==true).ToList());
            }

            return users;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<User> PayInstallment(string creditId)
        {
            var creditModel = await _creditRepository.GetCreditById(creditId);
            if(creditModel == null)
            {
                throw new BusinessException("No existe crédito con el id ingresado", 566);
            }
            creditModel.PayInstallment();
            var creditUpdated = await _creditRepository.UpdateCreditAsync(creditModel);
            var userModel = await _userRepository.FindByIdAsync(creditModel.UserId);

            userModel.AddCredit(creditUpdated);

            return userModel;
        }
    }
}
