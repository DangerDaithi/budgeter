using System;
using System.Collections.Generic;
using System.Linq;

namespace Budgeter
{
    public class BudgetCalculator
    {
        private Dictionary<ExpenditureCategory, double> _expenditureCategoriesToSubtotals;       

        public BudgetCalculator(double budget)
        {            
            _expenditureCategoriesToSubtotals = new Dictionary<ExpenditureCategory, double>();
            Budget = budget;
        }

        public double Budget { get; }

        public void Add(ExpenditureCategory category, double amount)
        {
            AddCategoryIfNotExists(category);
            _expenditureCategoriesToSubtotals[category] += amount;
        }

        public void Subtract(ExpenditureCategory category, double amount)
        {
            AddCategoryIfNotExists(category);        
            _expenditureCategoriesToSubtotals[category] -= amount;
        }

        public double SumTotal()
        {
            return _expenditureCategoriesToSubtotals.ToList().Sum(c => c.Value);
        }

        public double SumTotal(ExpenditureCategory category)
        {
            if (_expenditureCategoriesToSubtotals.TryGetValue(category, out double value))
            {
                return value;
            }
            // todo log a warning that expenditure category was not added
            return 0;
        }

        public double CalculateBudgetRemainder()
        {
            return Budget - _expenditureCategoriesToSubtotals.ToList().Sum(e => e.Value);
        }

        public IEnumerable<ExpenditureCategory> GetExpenditureCategories()
        {
            return _expenditureCategoriesToSubtotals.Keys;
        }       

        private void AddCategoryIfNotExists(ExpenditureCategory category)
        {
            _expenditureCategoriesToSubtotals.TryAdd(category, 0.00);
        }
    }
}
