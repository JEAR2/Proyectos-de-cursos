using DrivenAdapters.Mongo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivenAdapters.Mongo.Tests.Entity
{
    /// <summary>
    /// Builder
    /// </summary>
    public class CreditEntityBuilder
    {

        private string _id = string.Empty;

        private double _capitalBase = 0;

        private double _capitalDebt = 0;

        private double _effectiveAnnualInterestRate = 0;

        private double _installmentAmount = 0;

        private int _numberOfInstallments = 0;

        private bool _isActive = true;

        private string _userId = string.Empty;

        public CreditEntityBuilder() { }

        public CreditEntity Build() => new(
            _id,
            _capitalBase,
            _capitalDebt,
            _effectiveAnnualInterestRate,
            _installmentAmount,
            _numberOfInstallments,
            _isActive,
            _userId);

        public CreditEntityBuilder WithId(string id)
        {
            _id = id;
            return this;
        }
        public CreditEntityBuilder WithCapitalBase(double capitalBase)
        {
            _capitalBase = capitalBase;
            return this;
        }
        public CreditEntityBuilder WithCapitalDebt(double capitalDebt)
        {
            _capitalDebt = capitalDebt;
            return this;
        }
        public CreditEntityBuilder WithEffectiveAnnualInterestRate(double rate)
        {
            _effectiveAnnualInterestRate = rate;
            return this;
        }
        public CreditEntityBuilder WithInstallmentAmount(double installmentAmount)
        {
            _installmentAmount = installmentAmount;
            return this;
        }
        public CreditEntityBuilder WithNumberOfInstallments(int number)
        {
            _numberOfInstallments = number;
            return this;
        }
        public CreditEntityBuilder WithIsActive(bool isActive)
        {
            _isActive = isActive;
            return this;
        }
        public CreditEntityBuilder WithUserId(string userId)
        {
            _userId = userId;
            return this;
        }
    }
}
