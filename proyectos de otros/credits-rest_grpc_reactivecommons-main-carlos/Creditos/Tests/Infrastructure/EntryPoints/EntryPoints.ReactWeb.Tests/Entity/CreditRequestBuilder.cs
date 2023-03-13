using EntryPoints.ReactiveWeb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoints.ReactWeb.Tests.Entity
{
    public class CreditRequestBuilder
    {
        private double _capitalBase = 0;
        private double _effectiveAnnualInterestRate = 0;
        private int _numberOfInstallments = 0;
        private string _userId =string.Empty;

        public CreditRequestBuilder WithCapitalBase(double capitalBase)
        {
            _capitalBase = capitalBase;
            return this;
        }
        public CreditRequestBuilder WithEffectiveAnnualInterestRate(double effectiveAnnualInterestRate)
        {
            _effectiveAnnualInterestRate = effectiveAnnualInterestRate;
            return this;
        }
        public CreditRequestBuilder WithNumberOfInstallments(int numberOfInstallments)
        {
            _numberOfInstallments = numberOfInstallments;
            return this;
        }
        public CreditRequestBuilder WithUserId(string userId)
        {
            _userId = userId;
            return this;
        }
        public CreditRequest Build() =>
            new(_capitalBase, _effectiveAnnualInterestRate, _numberOfInstallments, _userId);
    }
}
