using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ExpenseTracker.DAL;
using ExpenseTracker.Models;
namespace ExpenseTracker.Controllers
{
    public class IncomeController : Controller
    {
        private ExpenseContext db = new ExpenseContext();

        // GET: Income
        public ActionResult Index()
        {
            return View(db.Income.ToList());
        }

        // GET: Income/Details/5
       
        // GET: Income/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Income/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "S_no,IncomeMoney,IncomeDate")] Income income)
        {
            if (ModelState.IsValid)
            {
                db.Income.Add(income);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(income);
        }

        // GET: Income/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Income income = db.Income.Find(id);
            if (income == null)
            {
                return HttpNotFound();
            }
            return View(income);
        }

        // POST: Income/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "S_no,IncomeMoney,IncomeDate")] Income income)
        {
            if (ModelState.IsValid)
            {
                db.Entry(income).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(income);
        }

        // GET: Income/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Income income = db.Income.Find(id);
            if (income == null)
            {
                return HttpNotFound();
            }
            return View(income);
        }

        // POST: Income/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Income income = db.Income.Find(id);
            db.Income.Remove(income);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Compare()
        {
            return View();
        }


        public JsonResult GetComparisonList()
        {
            List<List<object>> comparisonListData = new List<List<object>>();
            IncomeVsExpenseData obj = new IncomeVsExpenseData();
            comparisonListData = obj.CalculateIncomeVsExpense();
            return Json(comparisonListData);
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
