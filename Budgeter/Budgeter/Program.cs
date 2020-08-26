﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Budgeter
{
    class Program
    {

        private static string _appVersion = "2.1.0";

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
            var expenditureReportStringBuilder = new StringBuilder();

            Console.WriteLine($"*** Budgeting app v{_appVersion} ***");

            Console.WriteLine("Enter budget and hit return: ");    
            var budget = getBudgetFromStandardIn();

            Console.WriteLine("Enter month: ");
            var month = getMonthFromStandardIn();         

            var budgetCalculator = new BudgetCalculator(budget);

            Console.WriteLine("Hit return when finished to proceed to next category.");
            getExpenditureTotalsFromStandardIn(budgetCalculator);

            Console.WriteLine($"REPORT OVERVIEW - {month} \n");

            Console.WriteLine("{0,30} : {1,5}", $"Budget for month {month}", budget);
            Console.WriteLine("{0,30} : {1,5}", "Subtotal expenditure for month", budgetCalculator.SumTotal());
            Console.WriteLine("{0,30} : {1,5}", "Total budget remaining:", budgetCalculator.CalculateBudgetRemainder());  

            expenditureReportStringBuilder.AppendFormat("\n{0,30} : {1,5}", $"Budget for month {month}", budget);
            expenditureReportStringBuilder.AppendFormat("\n{0,30} : {1,5}", "Subtotal expenditure for month", budgetCalculator.SumTotal());
            expenditureReportStringBuilder.AppendFormat("\n{0,30} : {1,5}", "Total budget remaining:", budgetCalculator.CalculateBudgetRemainder());

            var expenditureSubtotalOverviewMessage = "\n\nSubtotals for each expenditure category: \n";
            Console.WriteLine(expenditureSubtotalOverviewMessage);
            expenditureReportStringBuilder.Append(expenditureSubtotalOverviewMessage);

           _expenditureCategoriesToCalculate.ToList().ForEach(e => {
                Console.WriteLine("{0,20} : {1,5}", e, budgetCalculator.SumTotal(e));
                expenditureReportStringBuilder.AppendFormat("\n{0,20} : {1,5}", e, budgetCalculator.SumTotal(e));
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

        private static void getExpenditureTotalsFromStandardIn(BudgetCalculator budgetCalculator)
        {
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