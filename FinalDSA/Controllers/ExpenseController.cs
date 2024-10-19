using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Run()
        {
            int choice;
            do
            {
                _view.DisplayMenu();
                Console.Write("\nNhập lựa chọn của bạn: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        HandleExpenseOptions();
                        break;
                    case 2:
                        HandleSortOptions();
                        break;
                    case 3:
                        EvaluateSpending();
                        break;
                }
            } while (choice != 0);
        }

        private void HandleExpenseOptions()
        {
            Console.WriteLine("1. Thêm chi tiêu");
            Console.WriteLine("2. Xóa chi tiêu");
            Console.WriteLine("3. Sửa chi tiêu");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.Write("Danh mục: ");
                    string category = Console.ReadLine();
                    Console.Write("Số tiền: ");
                    double amount = double.Parse(Console.ReadLine());
                    DateTime date = DateTime.Now;
                    Console.Write("Mô tả: ");
                    string description = Console.ReadLine();
                    _manager.AddExpense(category, amount, date, description);
                    break;
                case 2:
                    _view.DisplayExpenses(_manager.Expenses);
                    Console.Write("Nhập chỉ mục chi tiêu cần xóa: ");
                    int removeIndex = int.Parse(Console.ReadLine());
                    _manager.RemoveExpense(removeIndex);
                    break;
                case 3:
                    _view.DisplayExpenses(_manager.Expenses);
                    Console.Write("Nhập chỉ mục chi tiêu cần sửa: ");
                    int editIndex = int.Parse(Console.ReadLine());
                    Console.Write("Danh mục mới: ");
                    string newCategory = Console.ReadLine();
                    Console.Write("Số tiền mới: ");
                    double newAmount = double.Parse(Console.ReadLine());
                    DateTime newDate = DateTime.Now;
                    Console.Write("Mô tả mới: ");
                    string newDescription = Console.ReadLine();
                    _manager.EditExpense(editIndex, newCategory, newAmount, newDate, newDescription);
                    break;
            }
        }

        private void HandleSortOptions()
        {
            Console.WriteLine("1. Sắp xếp theo danh mục");
            Console.WriteLine("2. Sắp xếp theo số tiền");
            Console.WriteLine("3. Sắp xếp theo ngày");
            int option = int.Parse(Console.ReadLine());

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
            }
            _view.DisplayExpenses(_manager.Expenses);
        }

        private void EvaluateSpending()
        {
            double totalSpent = _manager.GetTotalSpent();
            Console.WriteLine($"\nTổng chi tiêu: {totalSpent}");
            _view.DisplayRemainingSpending(_manager.SpendingLimit - totalSpent);
        }
    }

}
