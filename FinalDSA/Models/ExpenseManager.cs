using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDSA.Models
{
    public class ExpenseManager
    {
        public List<Expense> Expenses { get; private set; }
        public double SpendingLimit { get; private set; }

        public ExpenseManager(double spendingLimit)
        {
            Expenses = new List<Expense>();
            SpendingLimit = spendingLimit;
        }

        public void AddExpense(string category, double amount, DateTime date, string description)
        {
            Expenses.Add(new Expense(category, amount, date, description));
            CheckSpendingLimit();
        }

        public void RemoveExpense(int index)
        {
            if (index >= 0 && index < Expenses.Count)
            {
                Expenses.RemoveAt(index);
            }
        }

        public void EditExpense(int index, string newCategory, double newAmount, DateTime newDate, string newDescription)
        {
            if (index >= 0 && index < Expenses.Count)
            {
                Expenses[index] = new Expense(newCategory, newAmount, newDate, newDescription);
                CheckSpendingLimit();
            }
        }

        public void CheckSpendingLimit()
        {
            double totalSpent = Expenses.Sum(e => e.Amount);
            if (totalSpent > SpendingLimit)
            {
                Console.WriteLine("CẢNH BÁO: Bạn đã vượt quá giới hạn chi tiêu!");
            }
        }

        public void SortByDate() => Expenses = MergeSort(Expenses, (e1, e2) => e1.Date.CompareTo(e2.Date));
        public void SortByCategory() => Expenses = MergeSort(Expenses, (e1, e2) => e1.Category.CompareTo(e2.Category));
        public void SortByAmount() => Expenses = MergeSort(Expenses, (e1, e2) => e1.Amount.CompareTo(e2.Amount));

        private List<Expense> MergeSort(List<Expense> list, Comparison<Expense> comparison)
        {
            if (list.Count <= 1) return list;

            int mid = list.Count / 2;
            List<Expense> left = MergeSort(list.GetRange(0, mid), comparison);
            List<Expense> right = MergeSort(list.GetRange(mid, list.Count - mid), comparison);

            return Merge(left, right, comparison);
        }

        private List<Expense> Merge(List<Expense> left, List<Expense> right, Comparison<Expense> comparison)
        {
            List<Expense> result = new List<Expense>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if (comparison(left[i], right[j]) <= 0)
                {
                    result.Add(left[i]);
                    i++;
                }
                else
                {
                    result.Add(right[j]);
                    j++;
                }
            }

            result.AddRange(left.GetRange(i, left.Count - i));
            result.AddRange(right.GetRange(j, right.Count - j));

            return result;
        }

        public double GetTotalSpent() => Expenses.Sum(e => e.Amount);
    }

}
