using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request
{
    /// <summary>
    /// Credit request for command and events
    /// </summary>
    public class CreditRequest
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
    }
}
