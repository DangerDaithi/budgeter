using System;
using System.Collections.Generic;
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
                var budgetCalculator = new BudgetCalculator(100, new Dictionary<ExpenditureCategory, double>());
                Assert.NotNull(budgetCalculator);
            }

            [Fact]
            public void ProvidingNullDictionary_ThenCallingCalculateExpenditureTotals_ThrowsArgumentNullException()
            {
                var budgetCalculator = new BudgetCalculator(100, null);
                Assert.Throws<ArgumentNullException>(() => budgetCalculator.CalculateExpenditureTotals());
            }

            [Fact]
            public void ProvidingNullDictionary_ThenCallingCalculateRemainingBugdet_ThrowsArgumentNullException()
            {
                var budgetCalculator = new BudgetCalculator(100, null);
                Assert.Throws<ArgumentNullException>(() => budgetCalculator.CalculateRemainingBugdet());
            }

            [Fact]
            public void ProvidingNullDictionary_ThenCallingGetSubtotalForExpenditureCateogory_ThrowsNullReferenceException()
            {
                var budgetCalculator = new BudgetCalculator(100, null);
                Assert.Throws<NullReferenceException>(() => budgetCalculator.GetSubtotalForExpenditureCategory(ExpenditureCategory.Misc));
            }
        }

        public class GetSubtotalForExpenditureCategory
        {
            [Fact]
            public void ExpenditureCategoryExists_CalculatesExpenditureForCategorySuccessfully()
            {
                var budgetToExpenditureMap = getBudgetToExpenditureMap();
                var budgetCalculator = new BudgetCalculator(budgetToExpenditureMap.Key, budgetToExpenditureMap.Value);
                Assert.Equal(10, budgetCalculator.GetSubtotalForExpenditureCategory(ExpenditureCategory.Appoinments));
            }

            [Fact]
            public void ExpenditureCategoryDoesNotExists_CalculatesExpenditureForCategoryReturnsZero()
            {
                var budgetToExpenditureMap = getBudgetToExpenditureMap();
                var budgetCalculator = new BudgetCalculator(budgetToExpenditureMap.Key, budgetToExpenditureMap.Value);
                Assert.Equal(0, budgetCalculator.GetSubtotalForExpenditureCategory(ExpenditureCategory.Social));
            }
        }

        public class CalculateRemainingBugdet
        {
            [Fact]
            public void UsingValidBudgetAndExpenditureCategories_CalculatesRemainingBudgetSuccessfully()
            {
                var budgetToExpenditureMap = getBudgetToExpenditureMap();
                var budgetCalculator = new BudgetCalculator(budgetToExpenditureMap.Key, budgetToExpenditureMap.Value);
                Assert.Equal(0, budgetCalculator.CalculateRemainingBugdet());
            }

            [Fact]
            public void UsingValidBudgetAndZeroExpenditureCategories_BudgetRemainsUnchganged()
            {
                var budgetCalculator = new BudgetCalculator(250, new Dictionary<ExpenditureCategory, double>());
                Assert.Equal(250, budgetCalculator.CalculateRemainingBugdet());
            }
        }

        public class CalculateExpenditureTotals
        {
            [Fact]
            public void UsingValidBudgetAndExpenditureCategories_CalculatesRemainingBudgetSuccessfully()
            {
                var budgetToExpenditureMap = getBudgetToExpenditureMap();
                var budgetCalculator = new BudgetCalculator(budgetToExpenditureMap.Key, budgetToExpenditureMap.Value);
                Assert.Equal(100.00, budgetCalculator.CalculateExpenditureTotals());
            }
        }

        private static KeyValuePair<double, Dictionary<ExpenditureCategory, double>> getBudgetToExpenditureMap()
        {
            return new KeyValuePair<double, Dictionary<ExpenditureCategory, double>>(100.00, new Dictionary<ExpenditureCategory, double>()
                    {
                        { ExpenditureCategory.Appoinments, 10 },
                        { ExpenditureCategory.Bins, 10 },
                        { ExpenditureCategory.Broadband, 10 },
                        { ExpenditureCategory.Electricity, 10 },
                        { ExpenditureCategory.SupermarketFood, 10 },
                        { ExpenditureCategory.Gas, 10 },
                        { ExpenditureCategory.Misc, 10 },
                        { ExpenditureCategory.Mobile, 10 },
                        { ExpenditureCategory.Petrol, 10 },
                        { ExpenditureCategory.Rent, 10 }
                    });
        }
    }
}
