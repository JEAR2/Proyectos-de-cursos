using Domain.Model.Entities;
using DrivenAdapters.Mongo.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo.Entities
{
    /// <summary>
    /// Credit mongo entity
    /// </summary>
    public class CreditEntity : BaseEntity
    {
        /// <summary>
        /// Const capital base (Only to know how much was lent)
        /// </summary>
        [BsonElement(elementName: "capital_base")]
        public double CapitalBase { get; set; }
        /// <summary>
        /// Money amount (base)
        /// </summary>
        [BsonElement(elementName: "capital_deuda")]
        public double CapitalDebt { get; set; }
        /// <summary>
        /// Interest rate
        /// </summary>
        [BsonElement(elementName: "interes_efectivo_anual")]
        public double EffectiveAnnualInterestRate { get; set; }
        /// <summary>
        /// Installment Amount
        /// </summary>
        [BsonElement(elementName: "valor_cuota")]
        public double InstallmentAmount { get; set; }
        /// <summary>
        /// Number of Installments 
        /// </summary>
        [BsonElement(elementName: "numero_cuotas")]
        public int NumberOfInstallments { get; set; }
        /// <summary>
        /// Is the credit active
        /// </summary>
        [BsonElement(elementName: "activo")]
        public bool IsActive { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        [BsonElement(elementName: "id_usuario")]
        public string UserId { get; set; }

        /// <summary>
        /// Constructor without entityId
        /// </summary>
        /// <param name="capitalBase"></param>
        /// <param name="capitalDebt"></param>
        /// <param name="effectiveAnnualInterestRate"></param>
        /// <param name="installmentAmount"></param>
        /// <param name="numberOfInstallments"></param>
        /// <param name="isActive"></param>
        /// <param name="userId"></param>
        public CreditEntity(double capitalBase, double capitalDebt, double effectiveAnnualInterestRate, double installmentAmount, int numberOfInstallments, bool isActive, string userId)
        {
            CapitalBase = capitalBase;
            CapitalDebt = capitalDebt;
            EffectiveAnnualInterestRate = effectiveAnnualInterestRate;
            InstallmentAmount = installmentAmount;
            NumberOfInstallments = numberOfInstallments;
            IsActive = isActive;
            UserId = userId;
        }

        /// <summary>
        /// Constructor without entityId
        /// </summary>
        /// <param name="capitalBase"></param>
        /// <param name="id"></param>
        /// <param name="capitalDebt"></param>
        /// <param name="effectiveAnnualInterestRate"></param>
        /// <param name="installmentAmount"></param>
        /// <param name="numberOfInstallments"></param>
        /// <param name="isActive"></param>
        /// <param name="userId"></param>
        public CreditEntity(string id, double capitalBase, double capitalDebt, double effectiveAnnualInterestRate, double installmentAmount, int numberOfInstallments, bool isActive, string userId)
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
        /// Convert to model
        /// </summary>
        /// <returns></returns>
        public Credit AsEntity()
        {
            return new(
                Id,
                CapitalBase,
                CapitalDebt,
                EffectiveAnnualInterestRate,
                InstallmentAmount,
                NumberOfInstallments,
                IsActive,
                UserId);
        } 
    }
}
