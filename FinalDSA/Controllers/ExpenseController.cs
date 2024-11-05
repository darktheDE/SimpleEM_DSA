using System;
using FinalDSA.Models;
using FinalDSA.Views;

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

        // Phương thức an toàn để nhập số nguyên
        private int InputInteger(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value))
                    return value;
                Console.WriteLine("Vui lòng nhập một số nguyên hợp lệ.");
            }
        }

        // Phương thức an toàn để nhập số thực
        private double InputDouble(string prompt)
        {
            double value;
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out value))
                    return value;
                Console.WriteLine("Vui lòng nhập một số thực hợp lệ.");
            }
        }

        // Phương thức an toàn để nhập số thực (float)
        private float InputFloat(string prompt)
        {
            float value;
            while (true)
            {
                Console.Write(prompt);
                if (float.TryParse(Console.ReadLine(), out value))
                    return value;
                Console.WriteLine("Vui lòng nhập một số thực (float) hợp lệ.");
            }
        }

        public void Run()
        {
            int choice;
            do
            {
                _view.DisplayMenu();
                choice = InputInteger("\nNhập lựa chọn của bạn (1-3, hoặc 0 để thoát): ");

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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLựa chọn không hợp lệ. Vui lòng thử lại.");
                        Console.ResetColor();
                        break;
                }
            } while (choice != 0);
        }

        private void HandleExpenseOptions()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║Bạn muốn thực hiện chức năng nào sau đây?           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.WriteLine("║ 1 │ Thêm chi tiêu                                  ║");
            Console.WriteLine("║ 2 │ Xóa chi tiêu                                   ║");
            Console.WriteLine("║ 3 │ Chỉnh sửa chi tiêu                             ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            int option = InputInteger("\nChọn chức năng: ");

            switch (option)
            {
                case 1:
                    Expense newExpense = _view.GetExpenseInput();
                    _manager.AddExpense(newExpense.Category, newExpense.Amount, newExpense.Date, newExpense.Description);
                    break;
                case 2:
                    _manager.DisplayExpenses();
                    int removeIndex = InputInteger("Nhập chỉ mục chi tiêu để xóa: ");
                    _manager.RemoveExpense(removeIndex);
                    break;
                case 3:
                    _manager.DisplayExpenses();
                    int editIndex = InputInteger("Nhập chỉ mục chi tiêu để sửa: ");
                    Expense editedExpense = _view.GetExpenseInput();
                    _manager.EditExpense(editIndex, editedExpense.Category, editedExpense.Amount, editedExpense.Date, editedExpense.Description);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLựa chọn không hợp lệ. Vui lòng thử lại.");
                    Console.ResetColor();
                    break;
            }
        }

        private void HandleSortOptions()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║Bạn muốn thực hiện chức năng nào sau đây?           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.WriteLine("║ 1 │ Sắp xếp theo danh mục                          ║");
            Console.WriteLine("║ 2 │ Sắp xếp theo tiền                              ║");
            Console.WriteLine("║ 3 │ Sắp xếp theo thời gian                         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            int option = InputInteger("\nChọn chức năng: ");
            switch (option)
            {
                case 1:
                    _manager.SortByCategory();
                    break;
                case 2:
                    _manager.SortByAmount();
                    break;
                case 3:
                    _manager.SortByDate();
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
            }
        }
    }
}
