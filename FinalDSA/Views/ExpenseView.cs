using System;
using System.Collections.Generic;
using System.IO;
using FinalDSA.Models;
using System.Text;
using FinalDSA.Controllers;
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
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║          XIN CHÀO BẠN! CHÀO MỪNG ĐẾN VỚI           ║");
            Console.WriteLine("║           CHƯƠNG TRÌNH QUẢN LÝ CHI TIÊU            ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║Bạn muốn thực hiện chức năng nào sau đây?           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.WriteLine("║ 1 │ Thêm, xóa và sửa chi tiêu                      ║");
            Console.WriteLine("║ 2 │ Hiển thị và sắp xếp chi tiêu theo các mục      ║");
            Console.WriteLine("║ 3 │ Đánh giá mức độ sử dụng của bạn                ║");
            Console.WriteLine("║ 4 │ Làm mới chi tiêu                               ║");
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
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.Write("║Nhập danh mục: ");
            string category = Console.ReadLine();
            
            Console.Write("║Nhập số tiền: ");
            double amount = double.Parse(Console.ReadLine());

            DateTime date = DateTime.Now;  // Khởi tạo mặc định với thời gian thực
            Console.Write("Chọn 1(thời gian thực), 2(tự nhập): ");
            
            int option;
            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        date = DateTime.Now;  // Thời gian thực
                        break;
                    case 2:
                        Console.Write("Nhập ngày (theo định dạng yyyy-MM-dd): ");
                        string inputDate = Console.ReadLine();

                        Console.Write("Nhập giờ (theo định dạng HH:mm:ss): ");
                        string inputTime = Console.ReadLine();

                        // Ghép chuỗi ngày và giờ lại với nhau
                        string inputDateTime = inputDate + " " + inputTime;

                        // Cố gắng chuyển đổi chuỗi nhập thành kiểu DateTime
                        if (!DateTime.TryParse(inputDateTime, out date))
                        {
                            Console.WriteLine("Định dạng ngày giờ không hợp lệ. Sử dụng thời gian thực thay thế.");
                            date = DateTime.Now;
                        }
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Sử dụng thời gian thực thay thế.");
                        date = DateTime.Now;
                        break;
                }
            }
            else
            {
                Console.WriteLine("Lựa chọn không hợp lệ. Sử dụng thời gian thực thay thế.");
            }

            Console.Write("║Nhập mô tả: ");
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
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║Nhập giới hạn chi tiêu của bạn                      ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            double spendingLimit = 0;
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nNhập giới hạn chi tiêu hàng tháng của bạn: ");
                    Console.ResetColor();

                    string input = Console.ReadLine();
                    if (double.TryParse(input, out spendingLimit))
                    {
                        if (spendingLimit >= 0)
                            return spendingLimit;
                        else
                            Console.WriteLine("Giới hạn phải là một số dương. Vui lòng nhập lại.");
                    }
                    else
                    {
                        throw new FormatException("Định dạng không hợp lệ. Vui lòng nhập một số thực.");
                    }
                }
                catch (FormatException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Lỗi: {ex.Message}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        public void DisplayCategoryPercentagesAsBarChart(List<(string category, double percentage, double amount)> categoryPercentages)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║           ĐÁNH GIÁ MỨC ĐỘ SỬ DỤNG CHI TIÊU         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ {0,-25} {1,-15} {2,-20} {3,-25}      ║", "Danh mục", "Phần trăm (%)", "Số tiền (VND)", "Biểu đồ");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════╝");
            int maxBarLength = 100;

            foreach (var item in categoryPercentages)
            {
                int barLength = (int)(item.percentage * maxBarLength / 100);

                Console.WriteLine("║ {0,-25} {1,-15:F2} {2,-20:F2} {3,-30} ║", item.category, item.percentage, item.amount, new string('#', barLength));
            }
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════╝");
        }
    }
}
