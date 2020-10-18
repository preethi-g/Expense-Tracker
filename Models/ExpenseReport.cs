using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpenseTracker.Models
{
    public class ExpenseReport
    {   [Key]   
        public int ItemID { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpenseDate { get; set; }

    }
}