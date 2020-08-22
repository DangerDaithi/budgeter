using System;
using System.Collections.Generic;
using System.Linq;

namespace Budgeter
{
    public class BudgetCalculator
    {
        private Dictionary<ExpenditureCategory, double> _expenditureCategoriesToSubtotals;
        private readonly double _budget;

        public BudgetCalculator(double budget, Dictionary<ExpenditureCategory, double> expenditureCategoriesToSubtotals)
        {
            _expenditureCategoriesToSubtotals = expenditureCategoriesToSubtotals;
            _budget = budget;
        }

        public double CalculateExpenditureTotals()
        {
            return _expenditureCategoriesToSubtotals.ToList().Sum(c => c.Value);
        }

        public double GetSubtotalForExpenditureCategory(ExpenditureCategory category)
        {
            if (_expenditureCategoriesToSubtotals.TryGetValue(category, out double value))
            {
                return value;
            }
            // todo add logging
            Console.WriteLine($"Budget Calculator failed to find the expenditure category {category}. Make sure the Budget Calculator is inititliased with this category.");
            return 0;
        }

        public double CalculateRemainingBugdet()
        {
            return _budget - _expenditureCategoriesToSubtotals.ToList().Sum(e => e.Value);
        }
    }
}
