using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpenseTracker.Models
{
    public class Income
    {
        [Key]
        public int S_no { get; set; }
        [Required]
        public decimal IncomeMoney { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime IncomeDate { get; set; }
    }
}