﻿// Models/ExpenseManager.cs
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
                // In bảng chi tiêu hiện tại trong khung bao quanh
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║              BẢNG DANH SÁCH CHI TIÊU               ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");

                // In tiêu đề bảng
                Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║ {0,-5} {1,-20} {2,-15} {3,-25} {4,-30} ║", "STT", "Danh mục", "Số tiền", "Ngày", "Mô tả");
                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════╝");

                // Duyệt qua danh sách chi tiêu và in từng mục
                for (int i = 0; i < _expenses.Count; i++)
                {
                    Console.WriteLine("║ {0,-5} {1,-20} {2,-15:F2} {3,-25} {4,-30} ║",
                        i + 1,                                  // STT
                        _expenses[i].Category,                   // Danh mục
                        _expenses[i].Amount,                     // Số tiền
                        _expenses[i].Date.ToString("yyyy-MM-dd HH:mm:ss"), // Ngày
                        _expenses[i].Description);               // Mô tả
                }

                // Kết thúc bảng
                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            }
            else
            {
                Console.WriteLine("\nChưa có chi tiêu nào.");
            }
        }

        public void SortByCategory()
        {
            //Sử dụng hàm
            _expenses = _expenses.OrderBy(e => e.Category).ToList();
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

            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║              TỔNG QUAN VỀ CHI TIÊU                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            // Hiển thị thông tin
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine($"║ Tổng số tiền đã chi tiêu: {totalSpent}                            ");
            Console.WriteLine($"║ Giới hạn chi tiêu        : {_spendingLimit}                       ");
            Console.WriteLine($"║ Số tiền còn lại          : {remaining}                            ");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            if (remaining < 0)
            {
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║CẢNH BÁO : BẠN ĐÃ VƯỢT QUÁ CHI TIÊU!!!              ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
            }
        }
    }
}
