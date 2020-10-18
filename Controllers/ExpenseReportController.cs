using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExpenseTracker.DAL;

namespace ExpenseTracker.Models
{
    public class ExpenseReportController : Controller
    {
        private ExpenseContext db = new ExpenseContext();
        private ExpenseData obj = new ExpenseData();
        List<string> list = new List<string> { "Food", "Travel", "Health", "Shopping", "Utility bills and rent", "Education" };

        // GET: ExpenseReport
        public ActionResult Index(string id)
        {

            var res = db.ExpenseReports.ToList();
            if(!String.IsNullOrEmpty(id))
            {
                res = res.Where(s => s.ItemName.Contains(id)).ToList();
                return View(res);
            }
            return View(res);

        }


        // GET: ExpenseReport/Create
        public ActionResult Create()
        {
           // var list = new List<string> { "Food", "Travel", "Health", "Shopping","Utility bills and rent","Education"};
            ViewBag.list = list;
            return View();
        }

        // POST: ExpenseReport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,ItemName,Category,Amount,ExpenseDate")] ExpenseReport expenseReport)
        {
            if (ModelState.IsValid)
            {
                db.ExpenseReports.Add(expenseReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expenseReport);
        }

        // GET: ExpenseReport/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseReport expenseReport = db.ExpenseReports.Find(id);
            if (expenseReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.list = list;
            return View(expenseReport);
        }

        // POST: ExpenseReport/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,ItemName,Category,Amount,ExpenseDate")] ExpenseReport expenseReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expenseReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseReport);
        }

        // GET: ExpenseReport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseReport expenseReport = db.ExpenseReports.Find(id);
            if (expenseReport == null)
            {
                return HttpNotFound();
            }
            return View(expenseReport);
        }

        // POST: ExpenseReport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpenseReport expenseReport = db.ExpenseReports.Find(id);
            db.ExpenseReports.Remove(expenseReport);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: ExpenseReport/ExpenseSummery
        public ActionResult ExpenseSummery()
        {
           
            var str= obj.GetWeeklyUpdate();
            ViewBag.len = str.Length;
            ViewBag.item = str;
            if (str.Length>0)
            {
                ViewBag.b = 1;
            }
          
           return View();
        }


        public JsonResult GetMonthlyExpense()
        {
            Dictionary<string, decimal?> dictMonthlySum = new Dictionary<string, decimal?>();
            var obj = new ExpenseData();
        dictMonthlySum = obj.CalculateMonthlyExpense();
            return Json(dictMonthlySum, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetYearlyExpense()
        {
            List<List<decimal?>> dictYearlySum = new List<List<decimal?>>();
           
            List<decimal?> list1 = obj.CalculateYearly("Food");
            List<decimal?> list2 = obj.CalculateYearly("Shopping");
            List<decimal?> list3 = obj.CalculateYearly("Travel");
            List<decimal?> list4 = obj.CalculateYearly("Health");
            List<decimal?> list5 = obj.CalculateYearly("Utility bills and rent");
            List<decimal?> list6 = obj.CalculateYearly("Education");

            dictYearlySum.Add(list1);
            dictYearlySum.Add(list2);
            dictYearlySum.Add(list3);
            dictYearlySum.Add(list4);
            dictYearlySum.Add(list5);
            dictYearlySum.Add(list6);


            return Json(dictYearlySum);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
