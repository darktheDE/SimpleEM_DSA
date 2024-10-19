using FinalDSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalDSA.Models
{
    public class ExpenseManager
    {
        private List<Expense> _expenses;
        private double _spendingLimit;

        public ExpenseManager(double spendingLimit)
        {
            _expenses = new List<Expense>();
            _spendingLimit = spendingLimit;
        }

        public void AddExpense(string category, double amount, DateTime date, string description)
        {
            Expense expense = new Expense(category, amount, date, description);
            _expenses.Add(expense);
            Console.WriteLine("\nChi tiêu đã được thêm thành công!");
            EvaluateSpending();
        }

        public void RemoveExpense(int index)
        {
            if (index >= 0 && index < _expenses.Count)
            {
                _expenses.RemoveAt(index);
                Console.WriteLine("\nChi tiêu đã được xóa thành công!");
            }
            else
            {
                Console.WriteLine("\nChỉ mục không hợp lệ!");
            }
        }

        public void EditExpense(int index, string category, double amount, DateTime date, string description)
        {
            if (index >= 0 && index < _expenses.Count)
            {
                _expenses[index].Category = category;
                _expenses[index].Amount = amount;
                _expenses[index].Date = date;
                _expenses[index].Description = description;
                Console.WriteLine("\nChi tiêu đã được sửa thành công!");
            }
            else
            {
                Console.WriteLine("\nChỉ mục không hợp lệ!");
            }
        }

        public void DisplayExpenses()
        {
            if (_expenses.Count > 0)
            {
                Console.WriteLine("\nDanh sách chi tiêu:");
                for (int i = 0; i < _expenses.Count; i++)
                {
                    var expense = _expenses[i];
                    Console.WriteLine($"{i}. {expense.Category}, {expense.Amount}, {expense.Date}, {expense.Description}");
                }
            }
            else
            {
                Console.WriteLine("\nChưa có chi tiêu nào.");
            }
        }

        public void SortByCategory()
        {
            _expenses = _expenses.OrderBy(e => e.Category).ToList();
            Console.WriteLine("\nDanh sách đã được sắp xếp theo danh mục.");
            DisplayExpenses();
        }

        public void SortByAmount()
        {
            _expenses = _expenses.OrderBy(e => e.Amount).ToList();
            Console.WriteLine("\nDanh sách đã được sắp xếp theo số tiền.");
            DisplayExpenses();
        }

        public void SortByDate()
        {
            _expenses = _expenses.OrderBy(e => e.Date).ToList();
            Console.WriteLine("\nDanh sách đã được sắp xếp theo ngày.");
            DisplayExpenses();
        }

        public void EvaluateSpending()
        {
            double totalSpent = _expenses.Sum(e => e.Amount);
            double remaining = _spendingLimit - totalSpent;

            Console.WriteLine($"\nTổng số tiền đã chi tiêu: {totalSpent}");
            Console.WriteLine($"Giới hạn chi tiêu: {_spendingLimit}");
            Console.WriteLine($"Số tiền còn lại: {remaining}");

            if (remaining < 0)
            {
                Console.WriteLine("\nCẢNH BÁO: Bạn đã vượt quá giới hạn chi tiêu!");
            }
        }
    }
}
