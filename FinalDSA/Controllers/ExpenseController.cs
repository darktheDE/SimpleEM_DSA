using System;
using FinalDSA.Models;
using FinalDSA.Views;
using System.Text;


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
        /// <summary>
        /// Hiển thị lời nhắc để người dùng nhập một giá trị số nguyên và kiểm tra tính hợp lệ của đầu vào cho đến khi người dùng nhập một số nguyên hợp lệ.
        /// </summary>
        /// <param name="prompt">Thông báo hiển thị để yêu cầu người dùng nhập dữ liệu.</param>
        /// <returns>Giá trị số nguyên mà người dùng đã nhập.</returns>
        /// <remarks>
        /// Nếu đầu vào là null, rỗng, hoặc không phải là số nguyên hợp lệ, sẽ hiển thị thông báo lỗi và yêu cầu người dùng nhập lại cho đến khi có đầu vào hợp lệ.
        /// </remarks>

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
        /// <summary>
        /// Chạy vòng lặp chính của chương trình, hiển thị menu chính và xử lý các lựa chọn của người dùng.
        /// </summary>
        /// <remarks>
        /// Vòng lặp chính này tiếp tục chạy cho đến khi người dùng chọn thoát chương trình (lựa chọn 0).
        /// Người dùng có thể chọn các tùy chọn để quản lý chi tiêu, sắp xếp, đánh giá chi tiêu, 
        /// đặt lại giới hạn chi tiêu hàng tháng hoặc tìm kiếm chi tiêu.
        /// Nếu lựa chọn không hợp lệ, một thông báo lỗi sẽ được hiển thị.
        /// </remarks>

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
        /// <summary>
        /// Xử lý chức năng tìm kiếm các chi tiêu theo tiêu chí do người dùng lựa chọn.
        /// </summary>
        /// <remarks>
        /// Hiển thị menu các chức năng tìm kiếm chi tiêu, cho phép người dùng chọn:
        /// tìm theo mô tả, theo danh mục, hoặc theo ngày tháng.
        /// Người dùng cũng có thể quay lại menu chính hoặc thoát ứng dụng.
        /// </remarks>

        private void HandleSearchExpenses()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Boolean endHandle = false;
            while (!endHandle) {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║Bạn muốn thực hiện chức năng nào sau đây?           ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.WriteLine("║ 1 │ Tìm chi tiêu theo danh mục                     ║");
                Console.WriteLine("║ 2 │ Tìm chi tiêu theo mô tả                        ║");
                Console.WriteLine("║ 3 │ Tìm chi tiêu theo thời gian                    ║");
                Console.WriteLine("║ 4 │ Thoát ra menu chính                            ║");
                Console.WriteLine("║ 0 │ Đóng ứng dụng                                  ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");

                int option = InputInteger("\nChọn chức năng: ");
                switch (option)
                {
                    case 1:
                        Console.Write("Nhập danh mục cần tìm: ");
                        string searchCategory = Console.ReadLine().ToLower(); // Chuyển về chữ thường để tìm kiếm không phân biệt hoa thường
                        _manager.SearchByCategory(searchCategory);
                        break;
                    case 2:
                        Console.Write("Nhập mô tả cần tìm: ");
                        string searchDescription = Console.ReadLine().ToLower(); // Chuyển về chữ thường để tìm kiếm không phân biệt hoa thường
                        _manager.SearchByDescription(searchDescription);
                        break;
                    case 3:
                        Console.Write("Nhập ngày cần tìm (theo định dạng yyyy-MM-dd): ");
                        string inputDate = Console.ReadLine();
                        _manager.SearchByDate(inputDate);
                        break;
                    case 4:
                        endHandle = true;
                        break;
                    case 0:
                        Console.WriteLine("\nCảm ơn bạn đã sử dụng chương trình. Tạm biệt!");
                        Environment.Exit(0); // Đóng chương trình
                        break;
                    default:
                        endHandle = true;
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
                Console.Write("Nhấn nút bất kì để tiếp tục: ");
                Console.ReadKey();
            }
        }
        /// <summary>
        /// Xử lý các tùy chọn quản lý chi tiêu do người dùng chọn, bao gồm thêm, xóa, và chỉnh sửa chi tiêu.
        /// </summary>
        /// <remarks>
        /// Hiển thị menu các tùy chọn quản lý chi tiêu, cho phép người dùng chọn:
        /// - Thêm một chi tiêu mới
        /// - Xóa một chi tiêu theo chỉ mục
        /// - Chỉnh sửa thông tin chi tiêu theo chỉ mục
        /// Người dùng cũng có thể quay lại menu chính hoặc thoát ứng dụng.
        /// </remarks>

        private void HandleExpenseOptions()
        {
            Boolean endHandle =false;
            while (!endHandle) {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║Bạn muốn thực hiện chức năng nào sau đây?           ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.WriteLine("║ 1 │ Thêm chi tiêu                                  ║");
                Console.WriteLine("║ 2 │ Xóa chi tiêu                                   ║");
                Console.WriteLine("║ 3 │ Chỉnh sửa chi tiêu                             ║");
                Console.WriteLine("║ 4 │ Thoát ra menu chính                            ║");
                Console.WriteLine("║ 0 │ Đóng ứng dụng                                  ║");
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
                    case 4:
                        endHandle = true;
                        break;
                    case 0:
                        Console.WriteLine("\nCảm ơn bạn đã sử dụng chương trình. Tạm biệt!");
                        Environment.Exit(0); // Đóng chương trình
                        break;
                    default:
                        endHandle = true;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLựa chọn không hợp lệ. Vui lòng thử lại.");
                        Console.ResetColor();
                        break;
                }
                Console.Write("Nhấn nút bất kì để tiếp tục: ");
                Console.ReadKey();
            }
        }
        /// <summary>
        /// Xử lý các tùy chọn sắp xếp chi tiêu do người dùng chọn, bao gồm sắp xếp theo danh mục, số tiền, hoặc thời gian.
        /// </summary>
        /// <remarks>
        /// Hiển thị menu các tùy chọn sắp xếp chi tiêu, cho phép người dùng chọn:
        /// - Sắp xếp chi tiêu theo danh mục
        /// - Sắp xếp chi tiêu theo số tiền
        /// - Sắp xếp chi tiêu theo thời gian
        /// Người dùng cũng có thể quay lại menu chính hoặc thoát ứng dụng.
        /// </remarks>

        private void HandleSortOptions()
        {
            Boolean endHandle = false;
            while (!endHandle) {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║Bạn muốn thực hiện chức năng nào sau đây?           ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.WriteLine("║ 1 │ Sắp xếp theo danh mục                          ║");
                Console.WriteLine("║ 2 │ Sắp xếp theo tiền                              ║");
                Console.WriteLine("║ 3 │ Sắp xếp theo thời gian                         ║");
                Console.WriteLine("║ 4 │ Thoát ra menu chính                            ║");
                Console.WriteLine("║ 0 │ Đóng ứng dụng                                  ║");
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
                    case 4:
                        endHandle = true;
                        break;
                    case 0:
                        Console.WriteLine("\nCảm ơn bạn đã sử dụng chương trình. Tạm biệt!");
                        Environment.Exit(0); // Đóng chương trình
                        break;
                    default:
                        endHandle = true;
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
                Console.Write("Nhấn nút bất kì để tiếp tục: ");
                Console.ReadKey();
            }
        }
        /// <summary>
        /// Đánh giá chi tiêu của người dùng bằng cách tính phần trăm chi tiêu theo từng danh mục và hiển thị biểu đồ cột.
        /// </summary>
        /// <remarks>
        /// Phương thức này làm mới giao diện, lấy phần trăm chi tiêu theo từng danh mục từ _manager,
        /// sau đó sử dụng _view để hiển thị kết quả dưới dạng biểu đồ cột.
        /// </remarks>

        private void HandleEvaluateSpending()
        {
            Console.Clear();
            var categoryPercentages = _manager.GetCategoryPercentages();
            _view.DisplayCategoryPercentagesAsBarChart(categoryPercentages);
        }
        /// <summary>
        /// Đặt lại giới hạn chi tiêu hàng tháng của người dùng với giá trị mới.
        /// </summary>
        /// <remarks>
        /// Hiển thị thông báo làm mới chi tiêu hàng tháng, lấy giới hạn chi tiêu mới từ _view, 
        /// và sử dụng _manager để cập nhật giới hạn chi tiêu hàng tháng.
        /// </remarks>

        private void HandleResetMonthlyExpenses()
        {
            Console.WriteLine("Làm mới chi tiêu hàng tháng:");
            double newLimit = _view.GetSpendingLimit();
            _manager.ResetMonthlyExpenses(newLimit);
        }
    }
}
