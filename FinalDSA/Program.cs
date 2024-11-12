﻿using System;
using System.IO;
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
            double spendingLimit;
            string filePath = "data.txt";

            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                // Đọc giới hạn chi tiêu từ file nếu có dữ liệu
                string[] noidung_txt = File.ReadAllLines(filePath);
                double.TryParse(noidung_txt[0], out spendingLimit);
            }
            else
            {
                // Nếu tệp không tồn tại hoặc trống, yêu cầu người dùng nhập và ghi vào tệp
                spendingLimit = GetSpendingLimit();
                // Đặt lại màu về mặc định
                Console.ResetColor();
                File.WriteAllText(filePath, spendingLimit.ToString());
            }
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
                        Console.ForegroundColor = ConsoleColor.Blue; // Thay đổi màu chữ thành xanh dương
                        Console.Write("\nNhập giới hạn chi tiêu hàng tháng của bạn: ");
                        if (double.TryParse(Console.ReadLine(), out limit))
                            break;
                        Console.ForegroundColor = ConsoleColor.Red; // Thay đổi màu chữ thành đỏ
                        Console.WriteLine("Vui lòng nhập một số thực hợp lệ.");
                    }
                    if (limit >= 0)
                        return limit;
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Giới hạn phải là số dương. Vui lòng nhập lại: ");
                    }           
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Vui lòng nhập một số hợp lệ: ");
                } 
            }

        }
    }
}
