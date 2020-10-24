using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests02
{
    [ProductComparison]
    public class ProductComparerShould
    {
        private List<LoanProduct> products;

        private ProductComparer sut;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            products = new List<LoanProduct>{
                new LoanProduct(1,"a",1),
                new LoanProduct(2,"b",2),
                new LoanProduct(3,"c",3),
            };
        }

        [SetUp]
        public void Setup()
        {


            sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        [Category("category A")]
        public void ReturnCorrectNumberOfComparisons()
        {            
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(3).Items);

        }

        [Test]
        [Category("category A")]
        [Category("category b")]
        public void NotReturnDuplicatedComparison()
        {            

            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Is.Unique);

        }

        [Test]
        [Category("category b")]
        public void ReturnComparisonForFistProduct()
        {            

            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(comparisons, Does.Contain(expectedProduct));

        }

        [Test]
        public void ReturnComparisonForFistProduct_withPartialKnowExpectedValues()
        {            

            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            //Assert.That(comparisons, Has.Exactly(1)
            //                                .Property("ProductName").EqualTo("a")
            //                                .And
            //                                .Property("InterestRate").EqualTo(1)
            //                                .And
            //                                .Property("MonthlyRepayment").GreaterThan(0));

            //Assert.That(comparisons, Has.Exactly(1)
            //                            .Matches<MonthlyRepaymentComparison>(
            //                                    item => item.ProductName == "a" &&
            //                                            item.InterestRate == 1 &&
            //                                            item.MonthlyRepayment > 0));         

            Assert.That(comparisons,
                Has.Exactly(1)
                .Matches(new MonthlyRepaymentGreaterThanZeroConstraint("a", 1)));
        }

    }
}
