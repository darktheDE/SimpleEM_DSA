using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDSA.Models
{
    public struct Expense
    {
        public string Category { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Expense(string category, double amount, DateTime date, string description)
        {
            Category = category;
            Amount = amount;
            Date = date;
            Description = description;
        }
    }

}
