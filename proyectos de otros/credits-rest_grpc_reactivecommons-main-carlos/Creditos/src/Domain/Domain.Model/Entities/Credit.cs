using credinet.exception.middleware.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities
{
    /// <summary>
    /// Credit class
    /// </summary>
    public class Credit
    {

        /// <summary>
        /// Credit id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Const capital base (Only to know how much was lent)
        /// </summary>
        public double CapitalBase { get; set; }
        /// <summary>
        /// Money amount (base)
        /// </summary>
        public double CapitalDebt { get; set; }
        /// <summary>
        /// Interest rate
        /// </summary>
        public double EffectiveAnnualInterestRate { get; set; }
        /// <summary>
        /// Installment Amount
        /// </summary>
        public double InstallmentAmount { get; set; }
        /// <summary>
        /// Number of Installments 
        /// </summary>
        public int NumberOfInstallments { get; set; }
        /// <summary>
        /// Is the credit active
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="capitalBase"></param>
        /// <param name="effectiveAnnualInterestRate"></param>
        /// <param name="numberOfInstallments"></param>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        public Credit(
            double capitalBase,
            double effectiveAnnualInterestRate,
            int numberOfInstallments,
            string userId,
            string id = null)
        {
            Id = id;
            CapitalBase = capitalBase;
            EffectiveAnnualInterestRate = effectiveAnnualInterestRate;
            NumberOfInstallments = numberOfInstallments;
            IsActive = true;
            CapitalDebt = capitalBase;
            double firstStep = (double)1 +  (double)EffectiveAnnualInterestRate/100;
            double secondStep = (double)1 / (double)12;
            double monthlyInterestRateStep = Math.Pow(firstStep, secondStep);
            var monthlyInterestRate = Math.Round(monthlyInterestRateStep - 1,4);
            InstallmentAmount = Math.Round((CapitalBase * (monthlyInterestRate * Math.Pow((1 + monthlyInterestRate), NumberOfInstallments))) / ((Math.Pow((1 + monthlyInterestRate), NumberOfInstallments) - 1)),2);
            UserId = userId;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="capitalBase"></param>
        /// <param name="capitalDebt"></param>
        /// <param name="effectiveAnnualInterestRate"></param>
        /// <param name="installmentAmount"></param>
        /// <param name="numberOfInstallments"></param>
        /// <param name="isActive"></param>
        /// <param name="userId"></param>
        public Credit(string id, double capitalBase, double capitalDebt, double effectiveAnnualInterestRate, double installmentAmount, int numberOfInstallments, bool isActive, string userId)
        {
            Id = id;
            CapitalBase = capitalBase;
            CapitalDebt = capitalDebt;
            EffectiveAnnualInterestRate = effectiveAnnualInterestRate;
            InstallmentAmount = installmentAmount;
            NumberOfInstallments = numberOfInstallments;
            IsActive = isActive;
            UserId = userId;
        }
    

        /// <summary>
        /// 
        /// </summary>
        public void PayInstallment()
        {
            double firstStep = (double)1 + (double)EffectiveAnnualInterestRate /100;
            double secondStep = (double)1 / (double)12;
            double monthlyInterestRateStep = Math.Pow(firstStep, secondStep);
            double monthlyInterestRate = Math.Round(monthlyInterestRateStep - 1, 4);
            double capital =Math.Round(Math.Round(CapitalDebt, 2) - (InstallmentAmount - (Math.Round(CapitalDebt, 2) * monthlyInterestRate)),2);

            if (IsActive && NumberOfInstallments == 1)
            {
                NumberOfInstallments -= 1;
                IsActive= false;
                CapitalDebt = capital;
            }
            else if (IsActive)
            {
                NumberOfInstallments -= 1;
                CapitalDebt = capital;
            }
            else
            {
                throw new BusinessException("El Crédito ya ha sido saldado", 566);
            }
        }
    }

}
