using Loans.Domain.Applications;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests02
{
    class MonthlyRepaymentGreaterThanZeroConstraint: Constraint
    {
        public string ExpectedProductName { get; }
        public decimal ExpectedInterestedRate { get; }

        public MonthlyRepaymentGreaterThanZeroConstraint(string expectedProductName, decimal expectedInterestedRate)
        {
            ExpectedProductName = expectedProductName;
            ExpectedInterestedRate = expectedInterestedRate;
        }

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            MonthlyRepaymentComparison comparison = actual as MonthlyRepaymentComparison;

            if(comparison is null)
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Error);
            }

            if(comparison.InterestRate == ExpectedInterestedRate && 
                comparison.ProductName == ExpectedProductName &&
                comparison.MonthlyRepayment > 0)
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Success);
            }

            return new ConstraintResult(this, actual, ConstraintStatus.Failure);

        }
    }
}
