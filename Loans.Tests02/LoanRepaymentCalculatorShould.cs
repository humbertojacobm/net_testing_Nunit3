using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests02
{
    public class LoanRepaymentCalculatorShould
    {
        [Test]
        [TestCase(200_000, 6.5, 30, 1264.14)]
        [TestCase(200_000, 10, 30, 1755.14)]
        public void CalculateCorrectMonthlyRepayment(decimal principal, decimal interestRate, int termInYears, decimal expectedMontlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var montlyPayment = sut.CalculateMonthlyRepayment(
                    new LoanAmount("USD", principal),
                    interestRate,
                    new LoanTerm(termInYears)
                );
            Assert.That(montlyPayment, Is.EqualTo(expectedMontlyPayment));
        }

        [Test]
        [TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
        [TestCase(200_000, 10, 30, ExpectedResult = 1755.14)]
        public decimal CalculateCorrectMonthlyRepayment_SimplifiedTestCase(decimal principal, 
            decimal interestRate, 
            int termInYears            )
        {
            var sut = new LoanRepaymentCalculator();

            return sut.CalculateMonthlyRepayment(
                    new LoanAmount("USD", principal),
                    interestRate,
                    new LoanTerm(termInYears)
                );            
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestData), "TestCases")]
        public void CalculateCorrectMonthlyRepayment_Centralized(decimal principal, decimal interestRate, int termInYears, decimal expectedMontlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var montlyPayment = sut.CalculateMonthlyRepayment(
                    new LoanAmount("USD", principal),
                    interestRate,
                    new LoanTerm(termInYears)
                );
            Assert.That(montlyPayment, Is.EqualTo(expectedMontlyPayment));
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentTestDataWithReturn), "TestCases")]
        public decimal CalculateCorrectMonthlyRepayment_CentralizedWithReturn(decimal principal,
            decimal interestRate,
            int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            return sut.CalculateMonthlyRepayment(
                    new LoanAmount("USD", principal),
                    interestRate,
                    new LoanTerm(termInYears)
                );
        }

        [Test]
        [TestCaseSource(typeof(MonthlyRepaymentCsvData), "GetTestCases", new object[] { "Data.csv" })]
        public void CalculateCorrectMonthlyRepayment_Csv(decimal principal, decimal interestRate, int termInYears, decimal expectedMontlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var montlyPayment = sut.CalculateMonthlyRepayment(
                    new LoanAmount("USD", principal),
                    interestRate,
                    new LoanTerm(termInYears)
                );
            Assert.That(montlyPayment, Is.EqualTo(expectedMontlyPayment));
        }

        [Test]        
        public void CalculateCorrectMonthlyRepayment_Combinatorial(
            [Values(100_000, 200_000,500_000)]decimal principal,
            [Values(6.5, 10,20)]decimal interestRate,
            [Values(10,20,30)]int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            var x = sut.CalculateMonthlyRepayment(
                    new LoanAmount("USD", principal),
                    interestRate,
                    new LoanTerm(termInYears)
                );
        }

        [Test]
        [Sequential]
        public void CalculateCorrectMonthlyRepayment_Sequential(
            [Values(200_000, 200_000, 500_000)] decimal principal,
            [Values(6.5, 10, 10)] decimal interestRate,
            [Values(30, 30, 30)] int termInYears, 
            [Values(1264.14,1755.14,4387.86)]decimal expectedMontlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var montlyPayment = sut.CalculateMonthlyRepayment(
                    new LoanAmount("USD", principal),
                    interestRate,
                    new LoanTerm(termInYears)
                );
            Assert.That(montlyPayment, Is.EqualTo(expectedMontlyPayment));
        }

        [Test]
        public void CalculateCorrectMonthlyRepayment_Range(
            [Range(50_000, 1_000_000, 50_000)] decimal principal,
            [Range(0.5, 20.00, 0.5)] decimal interestRate,
            [Values(10, 20, 30)] int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            var x = sut.CalculateMonthlyRepayment(
                    new LoanAmount("USD", principal),
                    interestRate,
                    new LoanTerm(termInYears)
                );
        }
    }
}
