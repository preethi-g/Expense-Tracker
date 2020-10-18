using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ExpenseTracker.Models;

namespace ExpenseTracker.DAL
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext():base("ExpenseContext")
        {

        }
        public DbSet<ExpenseReport> ExpenseReports { get; set; }
        public DbSet<Income> Income { get; set; }
    }
}