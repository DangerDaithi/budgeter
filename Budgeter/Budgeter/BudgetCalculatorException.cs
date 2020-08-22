using System;
using System.Collections.Generic;
using System.Text;

namespace Budgeter
{
    public class BudgetCalculatorException : Exception
    {
        public BudgetCalculatorException(string message) : base(message) { }
    }
}
