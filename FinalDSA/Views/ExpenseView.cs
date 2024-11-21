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
        public void DisplayExpenses(EList<Expense> expenses)
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

        public void DisplayExpenseTable(EList<Expense> expenses)
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
            Console.WriteLine("║ 5 │ Tìm kiếm chi tiêu                              ║");
            Console.WriteLine("║ 0 │ Thoát chương trình                             ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
        }

        public int GetUserChoice()
        {
            while (true)
            {
                Console.Write("\nNhập lựa chọn của bạn (1-3, hoặc 0 để thoát): ");
                string? input = Console.ReadLine(); // Sử dụng kiểu nullable string

                if (input != null && int.TryParse(input, out int choice))
                {
                    return choice; // Trả về nếu người dùng nhập hợp lệ
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại."); // Thông báo nếu nhập sai
                    // Đặt lại màu về mặc định
                    Console.ResetColor();
                }
            }
        }


        public Expense GetExpenseInput()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("╔════════════════════════════════════════════════════╗");

            // Nhập danh mục và kiểm tra giá trị null
            Console.Write("║Nhập danh mục: ");
            string? category = Console.ReadLine() ?? "Không xác định";
            double amount;
            while (true)
            {
                // Nhập số tiền và xử lý ngoại lệ khi người dùng nhập sai
                Console.Write("║Nhập số tiền: ");
                string? amountInput = Console.ReadLine();
                if (amountInput != null && double.TryParse(amountInput, out amount) && amount > 0)
                {
                    break; // Thoát vòng lặp khi nhập hợp lệ
                }
                else
                {
                    Console.WriteLine("Số tiền không hợp lệ. Vui lòng nhập lại.");
                }
            }

            // Kiểm tra lựa chọn của người dùng
            DateTime date = DateTime.Now;
            while (true)
            {
                Console.Write("Chọn 1(thời gian thực), 2(tự nhập): ");

                if (int.TryParse(Console.ReadLine(), out int option))
                {
                    switch (option)
                    {
                        case 1:
                            date = DateTime.Now; // Thời gian thực
                            Console.WriteLine($"Thời gian hiện tại: {date}");
                            break; // Thoát khỏi vòng lặp
                        case 2:
                            // Xử lý khi người dùng muốn nhập ngày và giờ thủ công
                            while (true)
                            {
                                Console.Write("Nhập ngày (theo định dạng yyyy-MM-dd): ");
                                string? inputDate = Console.ReadLine();

                                // Kiểm tra nếu người dùng không nhập gì
                                if (string.IsNullOrWhiteSpace(inputDate))
                                {
                                    Console.WriteLine("Ngày không thể bỏ trống. Vui lòng nhập lại.");
                                    continue;
                                }

                                // Kiểm tra định dạng ngày
                                if (!DateTime.TryParseExact(inputDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                                {
                                    Console.WriteLine("Định dạng ngày không hợp lệ. Vui lòng nhập lại theo định dạng yyyy-MM-dd.");
                                    continue;
                                }

                                // Kiểm tra tính hợp lệ của ngày (ngày không thể là tương lai)
                                if (parsedDate > DateTime.Now)
                                {
                                    Console.WriteLine("Ngày không thể là ngày trong tương lai. Vui lòng nhập lại.");
                                    continue;
                                }

                                // Nhập giờ
                                while (true)
                                {
                                    Console.Write("Nhập giờ (theo định dạng HH:mm:ss): ");
                                    string? inputTime = Console.ReadLine();

                                    // Kiểm tra nếu người dùng không nhập gì
                                    if (string.IsNullOrWhiteSpace(inputTime))
                                    {
                                        Console.WriteLine("Giờ không thể bỏ trống. Vui lòng nhập lại.");
                                        continue;
                                    }

                                    // Kiểm tra định dạng giờ
                                    if (!TimeSpan.TryParseExact(inputTime, @"hh\:mm\:ss", null, out TimeSpan parsedTime))
                                    {
                                        Console.WriteLine("Định dạng giờ không hợp lệ. Vui lòng nhập lại theo định dạng HH:mm:ss.");
                                        continue;
                                    }

                                    // Kiểm tra nếu giờ là 24:00:00, vì TimeSpan không cho phép giá trị này
                                    if (parsedTime.Hours == 24 && parsedTime.Minutes == 0 && parsedTime.Seconds == 0)
                                    {
                                        Console.WriteLine("Giờ không hợp lệ (không thể là 24:00:00). Vui lòng nhập lại.");
                                        continue;
                                    }

                                    // Ghép ngày và giờ lại với nhau để tạo thành DateTime đầy đủ
                                    date = parsedDate.Add(parsedTime);
                                    Console.WriteLine($"Ngày và giờ đã nhập: {date}");
                                    break; // Thoát khỏi vòng lặp
                                }

                                break; // Thoát khỏi vòng lặp nhập ngày giờ thủ công
                            }
                            break;
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                }
                break;
            }

            // Nhập mô tả và kiểm tra giá trị null
            Console.Write("║Nhập mô tả: ");
            string? description = Console.ReadLine() ?? "Không có mô tả";
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            return new Expense(category, amount, date, description);
        }



        public int GetExpenseIndex()
        {
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║Nhập chỉ mục chi tiêu:                              ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            int expends;
            while (true)
            {
                Console.Write("Nhập chỉ mục chi tiêu: ");
                if (int.TryParse(Console.ReadLine(), out expends))
                    break;

                Console.WriteLine("Vui lòng nhập một số nguyên hợp lệ.");
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nNhập giới hạn chi tiêu hàng tháng của bạn: ");
                Console.ResetColor();

                string? input = Console.ReadLine();

                // Kiểm tra null và parse double từ input
                if (!string.IsNullOrEmpty(input) && double.TryParse(input, out spendingLimit))
                {
                    if (spendingLimit >= 0)
                    {
                        return spendingLimit; // Trả về giá trị nếu hợp lệ
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Giới hạn phải là một số dương. Vui lòng nhập lại.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Định dạng không hợp lệ. Vui lòng nhập một số thực.");
                    Console.ResetColor();
                }
            }
        }

        public void DisplayCategoryPercentagesAsBarChart(EList<(string category, double percentage, double amount)> categoryPercentages)
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
