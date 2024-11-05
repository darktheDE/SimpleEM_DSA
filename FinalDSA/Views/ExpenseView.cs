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
            Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-25} {4,-30}", "STT", "Danh mục", "Số tiền", "Ngày", "Mô tả");
            Console.WriteLine(new string('-', 110));

            for (int i = 0; i < expenses.Count; i++)
            {
                Console.WriteLine("{0,-5} {1,-20} {2,-15:F2} {3,-25} {4,-30}",
                    i + 1,                                  // STT
                    expenses[i].Category,                   // Danh mục
                    expenses[i].Amount,                     // Số tiền
                    expenses[i].Date.ToString("yyyy-MM-dd HH:mm:ss"), // Ngày
                    expenses[i].Description);               // Mô tả
            }
        }


        public void DisplayRemainingSpending(double remaining)
        {
            Console.WriteLine($"\nSố tiền còn lại trong giới hạn chi tiêu: {remaining}");
        }

        public void DisplayExpenseTable(List<Expense> expenses)
        {
            // In bảng chi tiêu hiện tại trong khung bao quanh
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║              BẢNG DANH SÁCH CHI TIÊU               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            // In tiêu đề bảng
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ {0,-5} {1,-20} {2,-15} {3,-25} {4,-30} ║", "STT", "Danh mục", "Số tiền", "Ngày", "Mô tả");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");

            // Duyệt qua danh sách chi tiêu và in từng mục
            for (int i = 0; i < expenses.Count; i++)
            {
                Console.WriteLine("║ {0,-5} {1,-20} {2,-15:F2} {3,-25} {4,-30} ║",
                    i + 1,                                  // STT
                    expenses[i].Category,                   // Danh mục
                    expenses[i].Amount,                     // Số tiền
                    expenses[i].Date.ToString("yyyy-MM-dd HH:mm:ss"), // Ngày
                    expenses[i].Description);               // Mô tả
            }

            // Kết thúc bảng
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");

        }

        public void DisplayWarning(string message)
        {
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║CẢNH BÁO : BẠN ĐÃ VƯỢT QUÁ CHI TIÊU!!!              ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
        }

        public void DisplayMenu()
        {
            // Tiêu đề
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║          XIN CHÀO BẠN! CHÀO MỪNG ĐẾN VỚI           ║");
            Console.WriteLine("║           CHƯƠNG TRÌNH QUẢN LÝ CHI TIÊU            ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            // Tùy chọn menu
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║Bạn muốn thực hiện chức năng nào sau đây?           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.WriteLine("║ 1 │ Thêm, xóa và sửa chi tiêu                      ║");
            Console.WriteLine("║ 2 │ Hiển thị và sắp xếp chi tiêu theo các mục      ║");
            Console.WriteLine("║ 3 │ Đánh giá mức độ sử dụng của bạn                ║");
            Console.WriteLine("║ 0 │ Thoát chương trình                             ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
        }

        public int GetUserChoice()
        {
            Console.Write("\nNhập lựa chọn của bạn (1-3, hoặc 0 để thoát): ");
            return int.Parse(Console.ReadLine());
        }

        public Expense GetExpenseInput()
        {
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.Write(    "║Nhập danh mục: ");
            string category = Console.ReadLine();
            Console.Write(    "║Nhập số tiền: ");
            double amount = double.Parse(Console.ReadLine());
            DateTime date = DateTime.Now;  // Real-time
            Console.Write(    "║Nhập mô tả: ");
            string description = Console.ReadLine();
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            return new Expense(category, amount, date, description);
        }

        public int GetExpenseIndex()
        {
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║Nhập chỉ mục chi tiêu:                              ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            return int.Parse(Console.ReadLine());
        }

        public double GetSpendingLimit()
        {
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║Nhập giới hạn chi tiêu của bạn                      ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            return double.Parse(Console.ReadLine());
        }
    }
}
