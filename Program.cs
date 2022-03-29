using System;
using System.Collections.Generic;

namespace Tax
{
    class Program
    {
        static void Main(string[] args)
        {
            int totalIn, totalMinus;
            double resultMonth, resultYear;

            Console.WriteLine("Please input the total count you get:");
            string instring = Console.ReadLine();
            if (!Int32.TryParse(instring, out totalIn))
                Console.WriteLine("Please input a number");

            Console.WriteLine("Please input the minus value:");
            instring = Console.ReadLine();
            if (!Int32.TryParse(instring, out totalMinus))
                Console.WriteLine("Please input a number");

            resultMonth = CalByMonth(totalIn,totalMinus);
            resultYear = CalByYear(totalIn, totalMinus);

            Console.WriteLine("By Month Tax Result Total:" + resultMonth);
            Console.WriteLine("By Month Tax Result Average:" + resultMonth / 12);

            Console.WriteLine("By Year Tax Result Total:" + resultYear);
            Console.WriteLine("By Year Tax Result Average:" + resultYear / 12);
            Console.Read();
        }

        private static double CalByMonth(int totalIn, int totalMinus)
        {
            double result = 0;
            double taxSumBase = totalIn / 12;

            double[] monthAvg = new double[12];

            List<TaxBase> baseList = GetYearBaseList();
            for (int i = 0; i < 12; i++)
            {
                double taxSum = taxSumBase * (i + 1) - 5000 * (i + 1) - totalMinus * (i + 1);
                double subSum = 0;

                foreach (TaxBase refbase in baseList)
                {
                    if (taxSum > refbase.MinBase && taxSum < refbase.MaxBase)
                    {
                        monthAvg[i] = taxSum * refbase.PercentBase - refbase.SubBase;
                        break;
                    }
                }

                if (i > 0)
                {

                    for (int j = 0; j < i; j++)
                    {
                        subSum += monthAvg[j];
                    }
                }
                monthAvg[i] -= subSum;

                result += monthAvg[i];
            }

            return result;
        }

        private static double CalByYear(int totalIn, int totalMinus)
        {
            double result = 0;
            double taxSum = totalIn - 5000 * 12 - totalMinus * 12;

            List<TaxBase> baseList = GetYearBaseList();
            foreach (TaxBase refbase in baseList)
            {
                if (taxSum > refbase.MinBase && taxSum < refbase.MaxBase)
                {
                    result = taxSum * refbase.PercentBase - refbase.SubBase;
                    break;
                }
            }

            return result;
        }

        public static List<TaxBase> GetMonthBaseList()
        {
            List<TaxBase> monthList = new List<TaxBase>();
            monthList.Add(new TaxBase() { MinBase = 0, MaxBase = 3000, PercentBase = 0.03, SubBase = 0 });
            monthList.Add(new TaxBase() { MinBase = 3000, MaxBase = 12000, PercentBase = 0.1, SubBase = 210 });
            monthList.Add(new TaxBase() { MinBase = 12000, MaxBase = 25000, PercentBase = 0.2, SubBase = 1410 });
            monthList.Add(new TaxBase() { MinBase = 25000, MaxBase = 35000, PercentBase = 0.25, SubBase = 2660 });
            monthList.Add(new TaxBase() { MinBase = 35000, MaxBase = 55000, PercentBase = 0.3, SubBase = 4410 });
            monthList.Add(new TaxBase() { MinBase = 55000, MaxBase = 80000, PercentBase = 0.35, SubBase = 7160 });
            monthList.Add(new TaxBase() { MinBase = 80000, MaxBase = int.MaxValue, PercentBase = 0.45, SubBase = 15160 });
            return monthList;
        }

        public static List<TaxBase> GetYearBaseList()
        {
            List<TaxBase> yearList = new List<TaxBase>();
            yearList.Add(new TaxBase() { MinBase = 0, MaxBase = 36000, PercentBase = 0.03, SubBase = 0 });
            yearList.Add(new TaxBase() { MinBase = 36000, MaxBase = 144000, PercentBase = 0.1, SubBase = 2520 });
            yearList.Add(new TaxBase() { MinBase = 144000, MaxBase = 300000, PercentBase = 0.2, SubBase = 16920 });
            yearList.Add(new TaxBase() { MinBase = 300000, MaxBase = 420000, PercentBase = 0.25, SubBase = 31920 });
            yearList.Add(new TaxBase() { MinBase = 420000, MaxBase = 660000, PercentBase = 0.3, SubBase = 52920 });
            yearList.Add(new TaxBase() { MinBase = 660000, MaxBase = 960000, PercentBase = 0.35, SubBase = 85920 });
            yearList.Add(new TaxBase() { MinBase = 960000, MaxBase = int.MaxValue, PercentBase = 0.45, SubBase = 181920 });
            return yearList;
        }
    }

    public class TaxBase
    {
        public int MinBase { get; set; }

        public int MaxBase { get; set; }

        public double PercentBase { get; set; }

        public int SubBase { get; set; }

        public bool IsInPeriod { get; set; }
    }

}
