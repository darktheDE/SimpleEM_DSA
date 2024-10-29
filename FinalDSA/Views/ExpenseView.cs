using System;
using System.Collections.Generic;
using FinalDSA.Models;

namespace FinalDSA.Views
{
    public class ExpenseView
    {
        public void DisplayExpenses(List<Expense> expenses)
        {
            Console.WriteLine("\nDanh sách chi tiêu:");
            for (int i = 0; i < expenses.Count; i++)
            {
                Console.WriteLine($"{i}. Danh mục: {expenses[i].Category}, Số tiền: {expenses[i].Amount}, Ngày: {expenses[i].Date.ToString("yyyy-MM-dd HH:mm:ss")}, Mô tả: {expenses[i].Description}");
            }
        }

        public void DisplayRemainingSpending(double remaining)
        {
            Console.WriteLine($"\nSố tiền còn lại trong giới hạn chi tiêu: {remaining}");
        }

        public void DisplayExpenseTable(List<Expense> expenses)
        {
            Console.WriteLine("\nBảng danh sách chi tiêu hiện tại:");
            Console.WriteLine("{0,-15} {1,-10} {2,-25} {3,-30}", "Danh mục", "Số tiền", "Ngày", "Mô tả");
            Console.WriteLine(new string('-', 70));

            foreach (var expense in expenses)
            {
                Console.WriteLine("{0,-15} {1,-10} {2,-25} {3,-30}",
                    expense.Category, expense.Amount, expense.Date.ToString("yyyy-MM-dd HH:mm:ss"), expense.Description);
            }
            Console.WriteLine(new string('-', 70));
        }

        public void DisplayWarning(string message)
        {
            Console.WriteLine("\nCẢNH BÁO: " + message);
        }

        public void DisplayMenu()
        {
            Console.WriteLine(new string('*', 50));
            Console.WriteLine("*\tXIN CHÀO BẠN! CHÀO MỪNG ĐẾN VỚI\t\t *");
            Console.WriteLine("*\t  CHƯƠNG TRÌNH QUẢN LÝ CHI TIÊU\t\t *");
            Console.WriteLine(new string('*', 50));
            Console.WriteLine("\nBạn muốn thực hiện chức năng nào sau đây?");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("| 1 | Thêm, xóa và sửa chi tiêu");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("| 2 | Hiển thị danh sách và sắp xếp chi tiêu theo các mục");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("| 3 | Đánh giá mức độ sử dụng của bạn");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("| 0 | Thoát chương trình");
            Console.WriteLine(new string('-', 60));
        }

        public int GetUserChoice()
        {
            Console.Write("\nNhập lựa chọn của bạn (1-3, hoặc 0 để thoát): ");
            return int.Parse(Console.ReadLine());
        }

        public Expense GetExpenseInput()
        {
            Console.Write("Nhập danh mục: ");
            string category = Console.ReadLine();
            Console.Write("Nhập số tiền: ");
            double amount = double.Parse(Console.ReadLine());
            DateTime date = DateTime.Now;  // Real-time
            Console.Write("Nhập mô tả: ");
            string description = Console.ReadLine();

            return new Expense(category, amount, date, description);
        }

        public int GetExpenseIndex()
        {
            int expends = 0;
            while (true)
            {
                Console.Write("Nhập chỉ mục chi tiêu: ");
                if (int.TryParse(Console.ReadLine(), out expends))
                    break;
                Console.WriteLine("Vui lòng nhập một số thực hợp lệ.");
            }
            return expends;
        }

        public double GetSpendingLimit()
        {
            double spendingExpends = 0;
            while (true)
            {
                Console.Write("Nhập chỉ mục chi tiêu: ");
                if (double.TryParse(Console.ReadLine(), out spendingExpends))
                    break;
                Console.WriteLine("Vui lòng nhập một số thực hợp lệ.");
            }
            Console.Write("\nNhập giới hạn chi tiêu hàng tháng của bạn: ");
            return spendingExpends;
        }
    }
}
