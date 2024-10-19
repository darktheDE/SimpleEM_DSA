using System;

namespace FinalDSA.Models
{
    public class Expense
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
