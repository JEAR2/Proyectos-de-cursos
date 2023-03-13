using Domain.Model.Entities;
using Domain.Model.Entities.Gateway;
using DrivenAdapters.Mongo.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo
{
    /// <summary>
    /// 
    /// </summary>
    public class CreditRepository : ICreditRepository
    {
        private readonly IMongoCollection<CreditEntity> _collectionCredits;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mongodb"></param>
        public CreditRepository(IContext mongodb)
        {
            _collectionCredits = mongodb.Credits;
        }   
        /// <summary>
        /// Create a credit
        /// </summary>
        /// <param name="credit"></param>
        /// <returns></returns>
        public async Task<Credit> CreateCreditAsync(Credit credit)
        {
            CreditEntity creditEntity = new(
                credit.CapitalBase,
                credit.CapitalBase,
                credit.EffectiveAnnualInterestRate,
                credit.InstallmentAmount,
                credit.NumberOfInstallments,
                credit.IsActive,
                credit.UserId);

            await _collectionCredits.InsertOneAsync(creditEntity);

            return creditEntity.AsEntity();

        }
        /// <summary>
        /// Get all credits
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Credit>> GetAllCreditsWithUserId(string userId)
        {
            IAsyncCursor<CreditEntity> userCredits = 
                await _collectionCredits.FindAsync(Builders<CreditEntity>.Filter.Eq(credit => credit.UserId, userId));

            List<Credit> credits = userCredits.ToEnumerable()
                .Select(credit => credit.AsEntity()).ToList();

            return credits;
        }
        /// <summary>
        /// Get a credit by id
        /// </summary>
        /// <param name="creditId"></param>
        /// <returns></returns>
        public async Task<Credit> GetCreditById(string creditId)
        {
            IAsyncCursor<CreditEntity> creditsEntity =
                await _collectionCredits.FindAsync(Builders<CreditEntity>.Filter.Eq(credit => credit.Id, creditId));

            Credit credit = creditsEntity.FirstOrDefault().AsEntity();

            return credit;

        }
        /// <summary>
        /// update a credit
        /// </summary>
        /// <param name="credit"></param>
        /// <returns></returns>
        public async Task<Credit> UpdateCreditAsync(Credit credit)
        {
            CreditEntity updatedCredit = new(
                credit.CapitalBase,
                credit.CapitalDebt,
                credit.EffectiveAnnualInterestRate,
                credit.InstallmentAmount,
                credit.NumberOfInstallments,
                credit.IsActive,
                credit.UserId)
            {
                Id = credit.Id
            };

            var options = new FindOneAndReplaceOptions<CreditEntity, CreditEntity>();
            options.ReturnDocument = ReturnDocument.After;
            CreditEntity creditModel = await _collectionCredits.
                FindOneAndReplaceAsync<CreditEntity>(
                Builders<CreditEntity>.Filter.Eq(c => c.Id, credit.Id),
                updatedCredit,
                options
                );



            return creditModel.AsEntity();
        }
    }
}
