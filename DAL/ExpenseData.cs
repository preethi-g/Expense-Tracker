using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseTracker.Models;

namespace ExpenseTracker.DAL
{
    public class ExpenseData
    {
        private ExpenseContext db = new ExpenseContext();
        public Dictionary<string, decimal?> CalculateMonthlyExpense()
        {
            Dictionary<string, decimal?> MonthlySum = new Dictionary<string, decimal?>();
           
            decimal? foodSum = db.ExpenseReports.Where
               (cat => cat.Category == "Food" && (cat.ExpenseDate.Month == DateTime.Now.Month))
               .Select(cat => cat.Amount)
               .Sum();
           
           decimal? shoppingSum = db.ExpenseReports.Where
               (cat => cat.Category == "Shopping" && (cat.ExpenseDate.Month == DateTime.Now.Month))
               .Select(cat => cat.Amount)
               .Sum();
           
            decimal? travelSum = db.ExpenseReports.Where
               (cat => cat.Category == "Travel" && (cat.ExpenseDate.Month == DateTime.Now.Month))
               .Select(cat => cat.Amount)
               .Sum();
           
            decimal? healthSum = db.ExpenseReports.Where
               (cat => cat.Category == "Health" && (cat.ExpenseDate.Month == DateTime.Now.Month))
               .Select(cat => cat.Amount)
               .Sum();
           
            decimal? Utility = db.ExpenseReports.Where
               (cat => cat.Category == "Utility bills and rent" && (cat.ExpenseDate.Month == DateTime.Now.Month))
               .Select(cat => cat.Amount)
               .Sum();
           
            decimal? edu = db.ExpenseReports.Where
               (cat => cat.Category == "Education" && (cat.ExpenseDate.Month == DateTime.Now.Month))
               .Select(cat => cat.Amount)
               .Sum();

            MonthlySum.Add("Food", foodSum);
            MonthlySum.Add("Shopping",shoppingSum);
            MonthlySum.Add("Travel", travelSum);
            MonthlySum.Add("Health", healthSum);
            MonthlySum.Add("Utility bills and rent", Utility);
            MonthlySum.Add("Education", edu);
            return MonthlySum;
        }
        public List<decimal?> CalculateYearly(string category)
        {
            var q = from o in db.ExpenseReports
                    orderby o.ExpenseDate.Month
                    where o.Category == category && o.ExpenseDate.Year == DateTime.Now.Year
                    group o by o.ExpenseDate.Month into g
                    select new
                    {
                        ind=g.Key,
                        res = g.Sum(s => s.Amount)
                    };
           
            List<decimal?> l1 = new List<decimal?>();
            for(int i=0;i<12;i++)
            {
                l1.Add(0);
            }

            foreach (var i in q)
            {
                l1[i.ind - 1] = i.res;
            }
            return l1;
        }
       public string[] GetWeeklyUpdate()
        {
            //string result = "";
            var incomeForMonth = (from i in db.Income
                                  where i.IncomeDate.Month == DateTime.Now.Month && i.IncomeDate.Year == DateTime.Now.Year
                                  select i.IncomeMoney).FirstOrDefault();
            incomeForMonth = 0.2M * incomeForMonth;
            var date = DateTime.Now.AddDays(-7);
            var exp = (from e in db.ExpenseReports
                      where e.ExpenseDate > date && e.Amount > incomeForMonth
                      select e.ItemName).ToArray();

            return exp;
        }
    }
}