using System;
using FinalDSA.Controllers;
using FinalDSA.Models;
using FinalDSA.Views;

namespace ExpenseTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Đảm bảo hiển thị tiếng Việt tốt
            RunProgram();
        }

        static void RunProgram()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║             QUẢN LÝ CHI TIÊU CÁ NHÂN               ║");
            Console.WriteLine("╠════════════════════════════════════════════════════╣");
            Console.WriteLine("║ 23133056: Phan Trọng Phú                           ║");
            Console.WriteLine("║ 23133061: Phan Trọng Quí                           ║");
            Console.WriteLine("║ 23133030: Đỗ Kiến Hưng                             ║");
            Console.WriteLine("║ 23110086: Nguyễn Văn Quang Duy                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");


            // Yêu cầu người dùng nhập giới hạn chi tiêu
            double spendingLimit = GetSpendingLimit();
            ExpenseManager expenseManager = new ExpenseManager(spendingLimit);
            ExpenseView expenseView = new ExpenseView();
            ExpenseController expenseController = new ExpenseController(expenseManager, expenseView);

            // Chạy vòng lặp chương trình
            expenseController.Run();
        }

        static double GetSpendingLimit()
        {
            Console.ForegroundColor = ConsoleColor.Blue; // Thay đổi màu chữ thành xanh dương
            Console.Write("\nNhập giới hạn chi tiêu hàng tháng của bạn: ");
            // Đặt lại màu về mặc định
            Console.ResetColor();
            while (true)
            {
                try
                {
                    double limit = double.Parse(Console.ReadLine());
                    if (limit >= 0)
                        return limit;
                    else
                        Console.WriteLine("Giới hạn phải là số dương. Vui lòng nhập lại: ");
                }
                catch
                {
                    Console.WriteLine("Vui lòng nhập một số hợp lệ: ");
                }
            }
        }
    }
}
