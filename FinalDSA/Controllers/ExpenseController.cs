﻿using FinalDSA.Models;
using FinalDSA.Views;
using System;

namespace FinalDSA.Controllers
{
    public class ExpenseController
    {
        private ExpenseManager _manager;
        private ExpenseView _view;

        public ExpenseController(ExpenseManager manager, ExpenseView view)
        {
            _manager = manager;
            _view = view;
        }

        public void Run()
        {
            int choice;
            do
            {
                _view.DisplayMenu();
                choice = _view.GetUserChoice();

                switch (choice)
                {
                    case 1:
                        HandleExpenseOptions();
                        break;
                    case 2:
                        HandleSortOptions();
                        break;
                    case 3:
                        _manager.EvaluateSpending();
                        break;
                    case 0:
                        Console.WriteLine("\nCảm ơn bạn đã sử dụng chương trình. Tạm biệt!");
                        break;
                    default:
                        Console.WriteLine("\nLựa chọn không hợp lệ. Vui lòng thử lại.");
                        break;
                }
            } while (choice != 0);
        }

        private void HandleExpenseOptions()
        {
            Console.Clear();
            //Trong Phu thêm vô nà
            Console.WriteLine("\u001b[31mĐây là màu đỏ\u001b[0m");
            //
            Console.WriteLine("1. Thêm chi tiêu");
            Console.WriteLine("2. Xóa chi tiêu");
            Console.WriteLine("3. Sửa chi tiêu");
            Console.Write("\nChọn chức năng: ");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Expense newExpense = _view.GetExpenseInput();
                    _manager.AddExpense(newExpense.Category, newExpense.Amount, newExpense.Date, newExpense.Description);
                    break;
                case 2:
                    _manager.DisplayExpenses();
                    int removeIndex = _view.GetExpenseIndex();
                    _manager.RemoveExpense(removeIndex);
                    break;
                case 3:
                    _manager.DisplayExpenses();
                    int editIndex = _view.GetExpenseIndex();
                    Expense editedExpense = _view.GetExpenseInput();
                    _manager.EditExpense(editIndex, editedExpense.Category, editedExpense.Amount, editedExpense.Date, editedExpense.Description);
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
            }
        }

        private void HandleSortOptions()
        {
            Console.Clear();
            Console.WriteLine("1. Sắp xếp theo danh mục");
            Console.WriteLine("2. Sắp xếp theo số tiền");
            Console.Write("\nChọn chức năng: ");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    _manager.SortByCategory();
                    break;
                case 2:
                    _manager.SortByAmount();
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
            }
        }
    }
}
