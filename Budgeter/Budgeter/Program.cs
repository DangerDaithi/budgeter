using System;
using System.Collections.Generic;
using System.Linq;

/*
 * Presents a list of expenditure categories to user, requires numeric
 * values for each category and calculates a subtotal.
 * User can enter multple numeric values for each budget category 
 * until they hit enter and program moves onto next category.
 * Totals sum of all categories.
 * Program asks for total budget for month.
 * Outputs total of each 
 */
namespace Budgeter
{
    class Program
    {

        private static Dictionary<ExpenditureCategory, int> _expendituresToTotalsMap = new Dictionary<ExpenditureCategory, int>() {
            {ExpenditureCategory.Appoinments, 0 },
            {ExpenditureCategory.Bins, 0 },
            { ExpenditureCategory.Broadband, 0},
            {ExpenditureCategory.Electricity, 0 },
            {ExpenditureCategory.Food, 0 },
            {ExpenditureCategory.Gas, 0 },
            {ExpenditureCategory.Misc, 0 },
            {ExpenditureCategory.Mobile, 0 },
            {ExpenditureCategory.Petrol, 0 },
            {ExpenditureCategory.Rent, 0 },
            {ExpenditureCategory.Social, 0 }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("*** Budgeting app v1.0.0 ***" +
                "\n" +
                "Enter budget and hit return: ");
            var budgetString = Console.ReadLine();
            var budget = 0;
            while (!int.TryParse(budgetString, out budget))
            {
                Console.WriteLine("Not a valid number, try again.");
                budgetString = Console.ReadLine();
            }
            Console.WriteLine("Hot return when finished to proceed to next category.");

            var currentExpenditureCount = 1;
            foreach (var expenditureToTotal in _expendituresToTotalsMap.ToList())
            {
                Console.WriteLine($"{currentExpenditureCount}/{_expendituresToTotalsMap.Count}.{expenditureToTotal.Key}:");
                var expenditureTotal = 0;
                var input = Console.ReadLine();
                while (!string.IsNullOrEmpty(input))
                {                  
                    while (!int.TryParse(input, out expenditureTotal))
                    {
                        Console.WriteLine("Not a valid number, try again.");
                        input = Console.ReadLine();
                    }
                    if (!string.IsNullOrEmpty(input))
                    {
                        _expendituresToTotalsMap[expenditureToTotal.Key] += expenditureTotal;
                    }
                    input = Console.ReadLine();
                }
                currentExpenditureCount++;
            }

            Console.WriteLine("Total for each expenditure category:");
            foreach (var expenditureToTotal in _expendituresToTotalsMap)
            {
                Console.WriteLine($"\t{expenditureToTotal.Key}: {expenditureToTotal.Value}");
            }

            var subTotalExpenditure = _expendituresToTotalsMap.ToList().Sum(c => c.Value);
            Console.WriteLine($"Subtotal expenditure for month: {subTotalExpenditure}" +
                $"\nTotal budget remaining: {budget - subTotalExpenditure}");
        }
    }
}
