using Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Tests.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class CreditBuilder
    {
        private string _id = null;

        private double _capitalBase = 0;

        private double _capitalDebt = 0;

        private double _effectiveAnnualInterestRate = 0;

        private double _installmentAmount = 0;

        private int _numberOfInstallments = 0;

        private bool _isActive = true;

        private string _userId = string.Empty;

        public CreditBuilder() { }

        public Credit Build() => new(
            _capitalBase,
            _effectiveAnnualInterestRate,
            _numberOfInstallments,
            _userId,
            _id);
        public Credit TotalBuild() => new(
            _id,
            _capitalBase,
            _capitalDebt,
            _effectiveAnnualInterestRate,
            _installmentAmount,
            _numberOfInstallments,
            _isActive,
            _userId);

        public CreditBuilder WithCapitalBase(double capitalBase)
        {
            _capitalBase = capitalBase;
            return this;
        }
        public CreditBuilder WithId(string id)
        {
            _id = id;
            return this;
        }
        public CreditBuilder WithEffectiveAnnualInterestRate(double rate)
        {
            _effectiveAnnualInterestRate = rate;
            return this;
        }
        public CreditBuilder WithNumberOfInstallments(int number)
        {
            _numberOfInstallments = number;
            return this;
        }
        public CreditBuilder WithUserId(string userId)
        {
            _userId = userId;
            return this;
        }
        public CreditBuilder WithCapitalDebt(double capitalDebt)
        {
            _capitalDebt = capitalDebt;
            return this;
        }
        public CreditBuilder WithInstallmentAmount(double installmentAmount)
        {
            _installmentAmount = installmentAmount;
            return this;
        }
        public CreditBuilder WithIsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }
    }

}
