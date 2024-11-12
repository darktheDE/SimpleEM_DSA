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
                string? input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out value))
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
                string? input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input) && double.TryParse(input, out value))
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
                string? input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input) && float.TryParse(input, out value))
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
                choice = InputInteger("\nNhập lựa chọn của bạn (1-4, hoặc 0 để thoát): ");

                switch (choice)
                {
                    case 1:
                        HandleExpenseOptions();
                        break;
                    case 2:
                        HandleSortOptions();
                        break;
                    case 3:
                        HandleEvaluateSpending();
                        _manager.EvaluateSpending();
                        break;
                    case 4:
                        HandleResetMonthlyExpenses();
                        break;
                    case 5:
                        HandleSearchExpenses();
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

        private void HandleSearchExpenses()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║Bạn muốn thực hiện chức năng nào sau đây?           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.WriteLine("║ 1 │ Tìm chi tiêu theo mô tả                        ║");
            Console.WriteLine("║ 2 │ Tìm chi tiêu theo danh mục                     ║");
            Console.WriteLine("║ 3 │ Tìm chi tiêu theo thời gian                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            int option = InputInteger("\nChọn chức năng: ");
            switch (option)
            {
                case 1:
                    Console.Write("Nhập danh mục cần tìm: ");
                    string? searchCategory = Console.ReadLine()?.ToLower();
                    if (searchCategory != null)
                        _manager.SearchByCategory(searchCategory);
                    break;
                case 2:
                    Console.Write("Nhập mô tả cần tìm: ");
                    string? searchDescription = Console.ReadLine()?.ToLower();
                    if (searchDescription != null)
                        _manager.SearchByDescription(searchDescription);
                    break;
                case 3:
                    Console.Write("Nhập ngày cần tìm (theo định dạng yyyy-MM-dd): ");
                    string? inputDate = Console.ReadLine();
                    if (inputDate != null)
                        _manager.SearchByDate(inputDate);
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
            }
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
                    int removeIndex = InputInteger("Nhập chỉ mục chi tiêu để xóa: ") - 1;
                    _manager.RemoveExpense(removeIndex);
                    break;
                case 3:
                    _manager.DisplayExpenses();
                    int editIndex = InputInteger("Nhập chỉ mục chi tiêu để sửa: ") - 1;
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

        private void HandleEvaluateSpending()
        {
            Console.Clear();
            var categoryPercentages = _manager.GetCategoryPercentages();
            _view.DisplayCategoryPercentagesAsBarChart(categoryPercentages);
        }

        private void HandleResetMonthlyExpenses()
        {
            Console.WriteLine("Làm mới chi tiêu hàng tháng:");
            double newLimit = _view.GetSpendingLimit();
            _manager.ResetMonthlyExpenses(newLimit);
        }
    }
}
