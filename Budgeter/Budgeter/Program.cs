using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budgeter
{
    class Program
    {

        private static string _appVersion = "2.0.0";

        public static void Main(string[] args)
        {
            var expenditureReportStringBuilder = new StringBuilder();

            Console.WriteLine($"*** Budgeting app v{_appVersion} ***");

            Console.WriteLine("Enter budget and hit return: ");    
            var budget = getBudgetFromStandardIn();

            Console.WriteLine("Enter month: ");
            var month = getMonthFromStandardIn();

            Dictionary<ExpenditureCategory, double> expendituresToTotalsMap = new Dictionary<ExpenditureCategory, double>() {
            {ExpenditureCategory.Appoinments, 0.00 },
            {ExpenditureCategory.Bins, 0.00 },
            {ExpenditureCategory.Broadband, 0.00},
            {ExpenditureCategory.Electricity, 0.00 },
            {ExpenditureCategory.Gas, 0.00 },
            {ExpenditureCategory.Netflix, 0.00 },
            {ExpenditureCategory.Spotify, 0.00 },
            {ExpenditureCategory.Petrol, 0.00 },
            {ExpenditureCategory.RestaurantAndTakeout, 0.00 },
            {ExpenditureCategory.SupermarketFood, 0.00 },
            {ExpenditureCategory.Games, 0.00 },
            {ExpenditureCategory.DomesticAndHoushold, 0.00 },
            {ExpenditureCategory.Mobile, 0.00 },                  
            {ExpenditureCategory.Rent, 0.00 },
            {ExpenditureCategory.Social, 0.00 },
            {ExpenditureCategory.Misc, 0.00 }};

            Console.WriteLine("Hit return when finished to proceed to next category.");
            getExpenditireTotalsFromStandardIn(expendituresToTotalsMap);

            var budgetCalculator = new BudgetCalculator(budget, expendituresToTotalsMap);

            Console.WriteLine("REPORT OVERVIEW \n");

            Console.WriteLine("{0,30} : {1,5}", $"Budget for month {month}", budget);
            Console.WriteLine("{0,30} : {1,5}", "Subtotal expenditure for month", budgetCalculator.CalculateExpenditureTotals());
            Console.WriteLine("{0,30} : {1,5}", "Total budget remaining:", budgetCalculator.CalculateRemainingBugdet());  

            expenditureReportStringBuilder.AppendFormat("\n{0,30} : {1,5}", $"Budget for month {month}", budget);
            expenditureReportStringBuilder.AppendFormat("\n{0,30} : {1,5}", "Subtotal expenditure for month", budgetCalculator.CalculateExpenditureTotals());
            expenditureReportStringBuilder.AppendFormat("\n{0,30} : {1,5}", "Total budget remaining:", budgetCalculator.CalculateRemainingBugdet());

            var expenditureSubtotalOverviewMessage = "\n\nSubtotals for each expenditure category: \n";
            Console.WriteLine(expenditureSubtotalOverviewMessage);
            expenditureReportStringBuilder.Append(expenditureSubtotalOverviewMessage);

            expendituresToTotalsMap.ToList().ForEach(e => {
                Console.WriteLine("{0,20} : {1,5}", e.Key, budgetCalculator.GetSubtotalForExpenditureCategory(e.Key));
                expenditureReportStringBuilder.AppendFormat("\n{0,20} : {1,5}", e.Key, budgetCalculator.GetSubtotalForExpenditureCategory(e.Key));
            });

            var expenditureWriter = new ExpenditureReportFileWriter();
            expenditureWriter.Save(expenditureReportStringBuilder.ToString());

            

            Console.WriteLine("\nPress any key to close terminal...");
            Console.ReadKey();
        }

        private static string getMonthFromStandardIn()
        {
            var month = Console.ReadLine();
            while (string.IsNullOrEmpty(month))
            {
                Console.WriteLine("Not a valid month, try again.");
                month = Console.ReadLine();
            }

            return month;
        }

        private static void getExpenditireTotalsFromStandardIn(Dictionary<ExpenditureCategory, double> expendituresToTotalsMap)
        {

            var currentExpenditureCount = 1;
            foreach (var expenditureToTotal in expendituresToTotalsMap.ToList())
            {
                Console.WriteLine($"{currentExpenditureCount}/{expendituresToTotalsMap.Count} - {expenditureToTotal.Key}:");
                var expenditureTotal = 0.00;
                var input = Console.ReadLine();
                while (!string.IsNullOrEmpty(input))
                {
                    while (!double.TryParse(input, out expenditureTotal))
                    {
                        Console.WriteLine("Not a valid number, try again.");
                        input = Console.ReadLine();
                    }
                    if (!string.IsNullOrEmpty(input))
                    {
                        expendituresToTotalsMap[expenditureToTotal.Key] += expenditureTotal;
                    }
                    input = Console.ReadLine();
                }
                currentExpenditureCount++;
            }
        }

        private static double getBudgetFromStandardIn()
        {
            var budget = 0.00;
            var budgetString = Console.ReadLine();
            while (!double.TryParse(budgetString, out budget))
            {
                Console.WriteLine("Not a valid number, try again.");
                budgetString = Console.ReadLine();
            }
            return budget;
        }
    }
}