using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoints.ReactiveWeb.Entity
{
    /// <summary>
    /// Credit Request
    /// </summary>
    public class CreditRequest
    {
        /// <summary>
        /// Capital base
        /// </summary>
        public double CapitalBase { get; set; }
        /// <summary>
        /// Interes rate
        /// </summary>
        public double EffectiveAnnualInterestRate { get; set; }
        /// <summary>
        /// Number of installments
        /// </summary>
        public int NumberOfInstallments { get; set; }
        /// <summary>
        /// Id of user to assign the credit
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Convert to entity
        /// </summary>
        /// <returns></returns>
        public Credit AsEntity()
        {

            return new(
                CapitalBase,
                EffectiveAnnualInterestRate,
                NumberOfInstallments,
                UserId);
        }

        public CreditRequest(double capitalBase, double effectiveAnnualInterestRate, int numberOfInstallments, string userId)
        {
            CapitalBase = capitalBase;
            EffectiveAnnualInterestRate = effectiveAnnualInterestRate;
            NumberOfInstallments = numberOfInstallments;
            UserId = userId;
        }
    }
}
