using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budgeter
{
    class Program
    {

        private static readonly string _appVersion = "2.1.2";

        // read these from file in future app version
        private static readonly IEnumerable<ExpenditureCategory> _expenditureCategoriesToCalculate = new List<ExpenditureCategory>() {
            {ExpenditureCategory.Appoinments },
            {ExpenditureCategory.Bins },
            {ExpenditureCategory.Broadband},
            {ExpenditureCategory.Electricity },
            {ExpenditureCategory.Gas },
            {ExpenditureCategory.Netflix },
            {ExpenditureCategory.Spotify },
            {ExpenditureCategory.Petrol },
            {ExpenditureCategory.RestaurantAndTakeout },
            {ExpenditureCategory.SupermarketFood },
            {ExpenditureCategory.Games },
            {ExpenditureCategory.DomesticAndHoushold },
            {ExpenditureCategory.Mobile },
            {ExpenditureCategory.Rent },
            {ExpenditureCategory.Social },
            {ExpenditureCategory.Misc }};

        public static void Main(string[] args)
        {
            Console.WriteLine($"*** Budgeting app v{_appVersion} ***");

            Console.WriteLine("Enter budget and hit return: ");    
            var budget = GetBudgetFromStandardIn();

            Console.WriteLine("Enter time period: ");
            var timePeriodString = GetTimePeriodStringFromStandardIn();         

            var budgetCalculator = new BudgetCalculator(budget);
         
            GetExpenditureTotalsFromStandardIn(budgetCalculator);
            
            WriteReportToFile(GetExpenditureReportStringAndPrintToStandardOut(budgetCalculator, timePeriodString));
            Console.WriteLine();
            Console.WriteLine("\nPress any key to close terminal...");
            Console.ReadKey();
        }

        private static string GetTimePeriodStringFromStandardIn()
        {
            var timePeriod = Console.ReadLine();
            while (string.IsNullOrEmpty(timePeriod))
            {
                Console.WriteLine("Not a valid entry, try again.");
                timePeriod = Console.ReadLine();
            }

            return timePeriod;
        }

        private static double GetBudgetFromStandardIn()
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

        private static void GetExpenditureTotalsFromStandardIn(BudgetCalculator budgetCalculator) {

            Console.WriteLine("Hit return when finished to proceed to next category.");
            var currentExpenditureCount = 1;

            foreach (var expenditureCategory in _expenditureCategoriesToCalculate.ToList())
            {
                Console.WriteLine($"{currentExpenditureCount}/{ _expenditureCategoriesToCalculate.ToList().Count()} - {expenditureCategory}:");
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
                        budgetCalculator.Add(expenditureCategory, expenditureTotal);
                    }
                    input = Console.ReadLine();
                }
                currentExpenditureCount++;
            }
        }

        private static string GetExpenditureReportStringAndPrintToStandardOut(BudgetCalculator budgetCalculator, string timePeriodString)
        {
            var expenditureReportStringBuilder = new StringBuilder();

            Console.WriteLine($"REPORT OVERVIEW - {timePeriodString} \n");
            expenditureReportStringBuilder.AppendFormat($"REPORT OVERVIEW - {timePeriodString} \n");

            Console.WriteLine("{0,30} : {1,5}", $"Budget for time period {timePeriodString}", budgetCalculator.Budget);
            Console.WriteLine("{0,30} : {1,5}", "Subtotal expenditure for time period", budgetCalculator.SumTotal());
            Console.WriteLine("{0,30} : {1,5}", "Total budget remaining:", budgetCalculator.CalculateBudgetRemainder());

            expenditureReportStringBuilder.AppendFormat("\n{0,30} : {1,5}", $"Budget for time period {timePeriodString}", budgetCalculator.Budget);
            expenditureReportStringBuilder.AppendFormat("\n{0,30} : {1,5}", "Subtotal expenditure for time period", budgetCalculator.SumTotal());
            expenditureReportStringBuilder.AppendFormat("\n{0,30} : {1,5}", "Total budget remaining:", budgetCalculator.CalculateBudgetRemainder());

            var expenditureSubtotalOverviewMessage = "\n\nSubtotals for each expenditure category: \n";
            Console.WriteLine(expenditureSubtotalOverviewMessage);
            expenditureReportStringBuilder.Append(expenditureSubtotalOverviewMessage);

            _expenditureCategoriesToCalculate.ToList().ForEach(e => {
                Console.WriteLine("{0,20} : {1,5}", e, budgetCalculator.SumTotal(e));
                expenditureReportStringBuilder.AppendFormat("\n{0,20} : {1,5}", e, budgetCalculator.SumTotal(e));
            });
            return expenditureReportStringBuilder.ToString();         
        }

        private static void WriteReportToFile(string report)
        {
            var expenditureWriter = new ExpenditureReportFileWriter();
            expenditureWriter.Save(report);
            Console.WriteLine($"Expenditure report saved to directory {expenditureWriter.BasePath}");
        }
    }
}