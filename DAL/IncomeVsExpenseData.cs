using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpenseTracker.Models;

namespace ExpenseTracker.DAL
{
    public class IncomeVsExpenseData
    {
        public List<List<object>> CalculateIncomeVsExpense()
        {
            ExpenseContext db = new ExpenseContext();
            List<List<object>> comparisonList = new List<List<object>>();
            var res1 = (from i in db.Income
                        orderby i.IncomeDate.Month
                        where i.IncomeDate.Year == DateTime.Now.Year
                        select i.IncomeMoney).ToList();
            var res2 = from i in db.ExpenseReports
                       orderby i.ExpenseDate.Month
                       where i.ExpenseDate.Year == DateTime.Now.Year
                       group i by i.ExpenseDate.Month into g
                       select new
                       {
                           total2 = g.Sum(s => s.Amount)
                       };
            List<object> incomeList = new List<object>();
            foreach (var i in res1)
            {
                incomeList.Add(i);
            }
            List<object> exp = new List<object>();
            foreach (var item in res2)
            {
                exp.Add(item.total2);
            }
            comparisonList.Add(incomeList);
            comparisonList.Add(exp);
            return comparisonList;
        }
    }
}