using System;
using System.Collections.Generic;
using System.Linq;

namespace Budgeter
{
    public class BudgetCalculator
    {
        private Dictionary<ExpenditureCategory, double> _expenditureCategoriesToSubtotals;
        private readonly double _budget;

        public BudgetCalculator(double budget)
        {            
            _expenditureCategoriesToSubtotals = new Dictionary<ExpenditureCategory, double>();
            _budget = budget;
        }

        public void Add(ExpenditureCategory category, double amount)
        {
            addCategoryIfNotExists(category);
            _expenditureCategoriesToSubtotals[category] += amount;
        }

        public void Subtract(ExpenditureCategory category, double amount)
        {
            addCategoryIfNotExists(category);        
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
            return _budget - _expenditureCategoriesToSubtotals.ToList().Sum(e => e.Value);
        }

        public IEnumerable<ExpenditureCategory> GetExpenditureCategories()
        {
            return _expenditureCategoriesToSubtotals.Keys;
        }

        private void addCategoryIfNotExists(ExpenditureCategory category)
        {
            _expenditureCategoriesToSubtotals.TryAdd(category, 0.00);
        }
    }
}
