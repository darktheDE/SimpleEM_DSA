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
            Console.WriteLine("******************************");
            Console.WriteLine("* QUẢN LÝ CHI TIÊU CÁ NHÂN    *");
            Console.WriteLine("******************************");

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
            while (true)
            {
                try
                {
                    double limit = 0;
                    while (true)
                    {
                        Console.Write("\nNhập giới hạn chi tiêu hàng tháng của bạn: ");
                        if (double.TryParse(Console.ReadLine(), out limit))
                            break;
                        Console.WriteLine("Vui lòng nhập một số thực hợp lệ.");
                    }
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
