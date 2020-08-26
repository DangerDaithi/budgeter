using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Budgeter.Test
{
    public class BudgetCalculatorTest
    {
        public class Constructor
        {
            [Fact]
            public void PassValidParams_NothingHappens()
            {
                var budgetCalculator = new BudgetCalculator(100);
                Assert.NotNull(budgetCalculator);
            }        
        }

        public class CalculateRemainingBugdet
        {
            [Fact]
            public void CallingCalculateBudgetRemainderBeforeAddingExpenditureAmounts_BudgetAndRemainderAreEqual()
            {
                var budgetCalculator = new BudgetCalculator(100);
                Assert.Equal(100, budgetCalculator.CalculateBudgetRemainder());
            }

            [Fact]
            public void AddingExpenditureAmounts_ThenCallingCalculateBudgetRemainder_ReturnsValidRemaindingBudgetAmount()
            {
                var budgetCalculator = new BudgetCalculator(250);
                budgetCalculator.Add(ExpenditureCategory.Electricity, 50);
                Assert.Equal(200, budgetCalculator.CalculateBudgetRemainder());
            }

            [Fact]
            public void AddingExpenditureAmountsGreaterThanBudgetAmount_ThenCallingCalculateBudgetRemainder_ReturnsNegativeBudgetAmount()
            {
                var budgetCalculator = new BudgetCalculator(250);
                budgetCalculator.Add(ExpenditureCategory.Electricity, 50);
                budgetCalculator.Add(ExpenditureCategory.Gas, 300);
                Assert.Equal(-100, budgetCalculator.CalculateBudgetRemainder());
            }
        }

        public class SumTotal
        {
            [Fact]
            public void UsingValidBudgetAndExpenditureCategories_CalculatesRemainingBudgetSuccessfully()
            {
                var budgetCalculator = new BudgetCalculator(100);
                budgetCalculator.Add(ExpenditureCategory.Appoinments, 10);
                budgetCalculator.Add(ExpenditureCategory.Appoinments, 10); ;
                budgetCalculator.Add(ExpenditureCategory.Broadband, 10);
                Assert.Equal(30, budgetCalculator.SumTotal());
            }
        }


        public class SumTotalForExpenditureCategory
        {
            [Fact]
            public void CallingSumTotalForCategoryBeforeCallingAdd_ReturnsZero()
            {
                var budgetCalculator = new BudgetCalculator(100);
                Assert.Equal(0, budgetCalculator.SumTotal(ExpenditureCategory.Appoinments));
            }

            [Fact]
            public void AddingCategoryAndValue_ThenCallingSumTotalForCategory_ReturnsCorrectSumTotal()
            {
                var budgetCalculator = new BudgetCalculator(100);
                budgetCalculator.Add(ExpenditureCategory.Appoinments, 10);
                budgetCalculator.Add(ExpenditureCategory.Appoinments, 10); ;
                Assert.Equal(20, budgetCalculator.SumTotal(ExpenditureCategory.Appoinments));
            }

        }

        public class Add
        {
            [Fact]
            public void AddingValidExpendituireAmounts_CalculatesSumTotalOfAllExpenditureCategoriesSuccessfully()
            {
                var budgetCalculator = new BudgetCalculator(100);
                budgetCalculator.Add(ExpenditureCategory.Appoinments, 10);
                budgetCalculator.Add(ExpenditureCategory.Appoinments, 10); ;
                budgetCalculator.Add(ExpenditureCategory.Broadband, 10);
                Assert.Equal(20, budgetCalculator.SumTotal(ExpenditureCategory.Appoinments));
                Assert.Equal(10, budgetCalculator.SumTotal(ExpenditureCategory.Broadband));
                Assert.Equal(70, budgetCalculator.CalculateBudgetRemainder());
            }
        }

        public class Subtract
        {
            [Fact]
            public void AddingThenSubtractingValidExpenditureAmounts_CalculatesSumTotalOfAllExpenditureCategoriesSuccessfully()
            {
                var budgetCalculator = new BudgetCalculator(100);
                budgetCalculator.Add(ExpenditureCategory.Appoinments, 10);
                budgetCalculator.Add(ExpenditureCategory.Appoinments, 10); ;
                budgetCalculator.Add(ExpenditureCategory.Broadband, 10);
                budgetCalculator.Subtract(ExpenditureCategory.Appoinments, 10);
                Assert.Equal(10, budgetCalculator.SumTotal(ExpenditureCategory.Appoinments));
                Assert.Equal(10, budgetCalculator.SumTotal(ExpenditureCategory.Broadband));
                Assert.Equal(80, budgetCalculator.CalculateBudgetRemainder());
            }

            [Fact]
            public void SubtractingAmountGreaterThanTotal_ReturnsNegativeValue()
            {
                var budgetCalculator = new BudgetCalculator(100);
                budgetCalculator.Add(ExpenditureCategory.Appoinments, 10);
                budgetCalculator.Add(ExpenditureCategory.Appoinments, 10); ;
                budgetCalculator.Add(ExpenditureCategory.Broadband, 30);
                budgetCalculator.Subtract(ExpenditureCategory.Appoinments, 30);
                Assert.Equal(-10, budgetCalculator.SumTotal(ExpenditureCategory.Appoinments));
                Assert.Equal(80, budgetCalculator.CalculateBudgetRemainder());
            }
        }

        public class GetExpenditureCategories
        {
            [Fact]
            public void AddingFiveExpendituireAmounts_ReturnsCorrectNumberOfExpenditureCategoryKeys()
            {
                var budgetCalculator = new BudgetCalculator(100);
                budgetCalculator.Add(ExpenditureCategory.Appoinments, 10);
                budgetCalculator.Add(ExpenditureCategory.Appoinments, 10); ;
                budgetCalculator.Add(ExpenditureCategory.Broadband, 10);
                budgetCalculator.Add(ExpenditureCategory.DomesticAndHoushold, 10);
                budgetCalculator.Add(ExpenditureCategory.Broadband, 10);
                budgetCalculator.Add(ExpenditureCategory.Games, 10);
                budgetCalculator.Add(ExpenditureCategory.Games, 10);
                budgetCalculator.Add(ExpenditureCategory.Games, 10);
                budgetCalculator.Add(ExpenditureCategory.Mobile, 10);
                Assert.Equal(5, budgetCalculator.GetExpenditureCategories().Count());
            }
        }
    }
}
