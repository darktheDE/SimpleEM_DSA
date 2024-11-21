// Models/ExpenseManager.cs
using FinalDSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;

namespace FinalDSA.Models
{
    public class ExpenseManager
    {
        private EList<Expense> _expenses;
        private double _spendingLimit;
        private readonly string _filePath = "data.txt";

        /// <summary>
        /// Quản lý các chi tiêu, bao gồm việc thêm, sửa, xóa, tìm kiếm, sắp xếp các khoản chi tiêu và đánh giá tình trạng chi tiêu.
        /// </summary>
        public ExpenseManager(double spendingLimit)
        {
            _expenses = new EList<Expense>();
            _spendingLimit = spendingLimit;
            LoadData();
        }

        /// <summary>
        /// Tải dữ liệu chi tiêu và giới hạn chi tiêu từ tệp tin.
        /// </summary>
        private void LoadData()
        {
            if (File.Exists(_filePath))
            {
                string[] lines = File.ReadAllLines(_filePath);
                if (lines.Length > 0)
                {
                    // Đọc spendingLimit từ dòng đầu tiên
                    double.TryParse(lines[0], out _spendingLimit);

                    // Đọc các chi tiêu từ các dòng tiếp theo
                    for (int i = 1; i < lines.Length; i++)
                    {
                        var parts = lines[i].Split(", ");
                        if (parts.Length == 4)
                        {
                            string category = parts[0];
                            double.TryParse(parts[1], out double amount);
                            DateTime.TryParse(parts[2], out DateTime date);
                            string description = parts[3];
                            _expenses.Add(new Expense(category, amount, date, description));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Lưu các chi tiêu và giới hạn chi tiêu vào tệp tin.
        /// </summary>
        private void SaveData()
        {
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                // Ghi spendingLimit vào dòng đầu tiên
                writer.WriteLine(_spendingLimit);

                // Ghi danh sách chi tiêu
                foreach (var expense in _expenses)
                {
                    writer.WriteLine($"{expense.Category}, {expense.Amount}, {expense.Date}, {expense.Description}");
                }
            }
        }

        /// <summary>
        /// Thêm một khoản chi tiêu mới vào danh sách.
        /// </summary>
        /// <param name="category">Danh mục chi tiêu.</param>
        /// <param name="amount">Số tiền chi tiêu.</param>
        /// <param name="date">Ngày chi tiêu.</param>
        /// <param name="description">Mô tả chi tiêu.</param>
        public void AddExpense(string category, double amount, DateTime date, string description)
        {
            Expense expense = new Expense(category, amount, date, description);
            _expenses.Add(expense);
            SaveData();
            Console.WriteLine("\nChi tiêu đã được thêm thành công!");
            EvaluateSpending();
        }

        /// <summary>
        /// Xóa một khoản chi tiêu từ danh sách theo chỉ mục.
        /// </summary>
        /// <param name="index">Chỉ mục của khoản chi tiêu cần xóa.</param>
        public void RemoveExpense(int index)
        {
            if (index >= 0 && index < _expenses.Count)
            {
                _expenses.RemoveAt(index);
                SaveData();
                Console.WriteLine("\nChi tiêu đã được xóa thành công!");
            }
            else
            {
                Console.WriteLine("\nChỉ mục không hợp lệ!");
            }
        }

        /// <summary>
        /// Sửa thông tin của một khoản chi tiêu trong danh sách.
        /// </summary>
        /// <param name="index">Chỉ mục của khoản chi tiêu cần sửa.</param>
        /// <param name="category">Danh mục chi tiêu mới.</param>
        /// <param name="amount">Số tiền chi tiêu mới.</param>
        /// <param name="date">Ngày chi tiêu mới.</param>
        /// <param name="description">Mô tả chi tiêu mới.</param>
        public void EditExpense(int index, string category, double amount, DateTime date, string description)
        {
            if (index >= 0 && index < _expenses.Count)
            {
                _expenses[index].Category = category;
                _expenses[index].Amount = amount;
                _expenses[index].Date = date;
                _expenses[index].Description = description;
                SaveData();
                Console.WriteLine("\nChi tiêu đã được sửa thành công!");
            }
            else
            {
                Console.WriteLine("\nChỉ mục không hợp lệ!");
            }
        }

        /// <summary>
        /// Hiển thị tất cả các chi tiêu trong danh sách.
        /// </summary>
        public void DisplayExpenses()
        {
            if (_expenses.Count > 0)
            {
                // In bảng chi tiêu hiện tại trong khung bao quanh
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║              BẢNG DANH SÁCH CHI TIÊU               ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");

                // In tiêu đề bảng
                Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║ {0,-5} {1,-20} {2,-15} {3,-25} {4,-30} ║", "STT", "Danh mục", "Số tiền", "Ngày", "Mô tả");
                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════╝");

                // Duyệt qua danh sách chi tiêu và in từng mục
                for (int i = 0; i < _expenses.Count; i++)
                {
                    Console.WriteLine("║ {0,-5} {1,-20} {2,-15:F2} {3,-25} {4,-30} ║",
                        i + 1,                                  // STT
                        _expenses[i].Category,                   // Danh mục
                        _expenses[i].Amount,                     // Số tiền
                        _expenses[i].Date.ToString("yyyy-MM-dd HH:mm:ss"), // Ngày
                        _expenses[i].Description);               // Mô tả
                }

                // Kết thúc bảng
                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            }
            else
            {
                Console.WriteLine("\nChưa có chi tiêu nào.");
            }
        }

        /// <summary>
        /// Sắp xếp danh sách chi tiêu theo danh mục sử dụng thuật toán Bubble Sort.
        /// </summary>
        public void SortByCategory()
        {
            // Sử dụng Bubble Sort để sắp xếp danh sách _expenses theo Category
            for (int i = 0; i < _expenses.Count - 1; i++)
            {
                for (int j = 0; j < _expenses.Count - i - 1; j++)
                {
                    // So sánh hai phần tử liên tiếp theo Category
                    if (string.Compare(_expenses[j].Category, _expenses[j + 1].Category, StringComparison.Ordinal) > 0)
                    {
                        // Hoán đổi nếu không đúng thứ tự
                        var temp = _expenses[j];
                        _expenses[j] = _expenses[j + 1];
                        _expenses[j + 1] = temp;
                    }
                }
            }

            // Hiển thị danh sách đã sắp xếp
            Console.WriteLine("\nDanh sách đã được sắp xếp theo danh mục.");
            DisplayExpenses();
        }

        /// <summary>
        /// Sắp xếp danh sách chi tiêu theo số tiền sử dụng thuật toán Insertion Sort.
        /// </summary>
        public void SortByAmount()
        {
            // Sử dụng Insertion Sort để sắp xếp danh sách _expenses theo số tiền
            for (int i = 1; i < _expenses.Count; i++)
            {
                var currentExpense = _expenses[i];
                int j = i - 1;

                // Di chuyển các phần tử lớn hơn currentExpense lên phía sau
                while (j >= 0 && _expenses[j].Amount > currentExpense.Amount)
                {
                    _expenses[j + 1] = _expenses[j];
                    j--;
                }

                // Chèn currentExpense vào vị trí đúng
                _expenses[j + 1] = currentExpense;
            }

            Console.WriteLine("\nDanh sách đã được sắp xếp theo số tiền.");
            DisplayExpenses();
        }

        /// <summary>
        /// Sắp xếp danh sách chi tiêu theo ngày sử dụng thuật toán Selection Sort.
        /// </summary>
        public void SortByDate()
        {
            // Sử dụng Selection Sort để sắp xếp danh sách _expenses theo ngày
            for (int i = 0; i < _expenses.Count - 1; i++)
            {
                int minIndex = i;
                // Tìm phần tử có ngày nhỏ nhất trong phần chưa sắp xếp
                for (int j = i + 1; j < _expenses.Count; j++)
                {
                    if (_expenses[j].Date < _expenses[minIndex].Date)
                    {
                        minIndex = j;
                    }
                }

                // Hoán đổi phần tử có ngày nhỏ nhất với phần tử hiện tại
                if (minIndex != i)
                {
                    var temp = _expenses[i];
                    _expenses[i] = _expenses[minIndex];
                    _expenses[minIndex] = temp;
                }
            }

            Console.WriteLine("\nDanh sách đã được sắp xếp theo ngày.");
            DisplayExpenses();
        }


        // Thuộc tính hoặc trường lưu kết quả tìm kiếm
        private EList<Expense> _searchResults = new EList<Expense>();

        /// <summary>
        /// Tìm kiếm các khoản chi tiêu theo danh mục bằng Linear search
        /// </summary>
        /// <param name="searchCategory">Danh mục chi tiêu cần tìm kiếm.</param>
        public void SearchByCategory(string searchCategory)
        {
            // Chuyển đổi `searchCategory` sang chữ thường để tránh phân biệt hoa/thường
            searchCategory = searchCategory.ToLower();

            // Duyệt qua danh sách _expenses và lọc ra các khoản chi tiêu khớp
            var searchResults = new EList<Expense>();
            foreach (var expense in _expenses)
            {
                if (expense.Category.ToLower().Contains(searchCategory))
                {
                    searchResults.Add(expense);
                }
            }

            if (searchResults.Count > 0)
            {
                Console.WriteLine($"\nKết quả tìm kiếm cho danh mục \"{searchCategory}\":");

                // Tạm thời hiển thị các kết quả tìm kiếm
                var originalExpenses = _expenses; // Lưu danh sách gốc
                _expenses = searchResults;        // Gán danh sách kết quả vào _expenses

                DisplayExpenses();                // Hiển thị kết quả

                _expenses = originalExpenses;     // Khôi phục danh sách gốc sau khi hiển thị
            }
            else
            {
                Console.WriteLine($"Không tìm thấy khoản chi tiêu nào trong danh mục \"{searchCategory}\".");
            }
        }

        /// <summary>
        /// Tìm kiếm các khoản chi tiêu theo mô tả bằng Linear search
        /// </summary>
        /// <param name="searchDescription">Mô tả chi tiêu cần tìm kiếm.</param>
        public void SearchByDescription(string searchDescription)
        {
            // Chuyển mô tả tìm kiếm về chữ thường để so sánh không phân biệt hoa thường
            searchDescription = searchDescription.ToLower();

            // Linear search qua toàn bộ danh sách
            EList<Expense> searchResults = new EList<Expense>();

            foreach (var expense in _expenses)
            {
                if (expense.Description.ToLower().Contains(searchDescription)) // Kiểm tra mô tả có chứa từ khóa
                {
                    searchResults.Add(expense); // Thêm phần tử vào kết quả tìm kiếm
                }
            }

            // Kiểm tra kết quả tìm kiếm
            if (searchResults.Count > 0)
            {
                Console.WriteLine($"\nKết quả tìm kiếm cho mô tả \"{searchDescription}\":");

                // Tạm thời gán kết quả tìm kiếm vào _expenses
                _expenses = searchResults;

                // Hiển thị các kết quả tìm kiếm
                DisplayExpenses();
            }
            else
            {
                Console.WriteLine($"Không tìm thấy khoản chi tiêu nào với mô tả \"{searchDescription}\".");
            }
        }

        /// <summary>
        /// Tìm kiếm các khoản chi tiêu theo ngày bằng Binary Search
        /// </summary>
        /// <param name="inputDate">Ngày cần tìm kiếm theo định dạng chuỗi.</param>
        public void SearchByDate(string inputDate)
        {
            if (DateTime.TryParse(inputDate, out DateTime searchDate))
            {
                // Sắp xếp danh sách chi tiêu theo ngày (nếu chưa sắp xếp)
                _expenses.Sort((a, b) => a.Date.CompareTo(b.Date));

                // Tìm kiếm ngày trong danh sách đã sắp xếp bằng Binary Search
                int left = 0;
                int right = _expenses.Count - 1;
                EList<Expense> filteredExpenses = new EList<Expense>();

                while (left <= right)
                {
                    int middle = (left + right) / 2;
                    int comparison = _expenses[middle].Date.Date.CompareTo(searchDate.Date);

                    if (comparison == 0)
                    {
                        // Tìm thấy ngày khớp tại middle, thêm vào kết quả
                        filteredExpenses.Add(_expenses[middle]);

                        // Tiếp tục tìm kiếm trong các phần tử bên trái
                        int tempLeft = middle - 1;
                        while (tempLeft >= 0 && _expenses[tempLeft].Date.Date == searchDate.Date)
                        {
                            filteredExpenses.Add(_expenses[tempLeft]);  // Thêm kết quả vào cuối danh sách
                            tempLeft--;
                        }

                        // Tiếp tục tìm kiếm trong các phần tử bên phải
                        int tempRight = middle + 1;
                        while (tempRight < _expenses.Count && _expenses[tempRight].Date.Date == searchDate.Date)
                        {
                            filteredExpenses.Add(_expenses[tempRight]);  // Thêm kết quả vào cuối danh sách
                            tempRight++;
                        }
                        break; // Kết thúc tìm kiếm sau khi đã tìm thấy tất cả các kết quả khớp
                    }
                    else if (comparison < 0)
                    {
                        left = middle + 1;
                    }
                    else
                    {
                        right = middle - 1;
                    }
                }

                // Kiểm tra kết quả tìm kiếm
                if (filteredExpenses.Count > 0)
                {
                    Console.WriteLine($"\nKết quả tìm kiếm cho ngày \"{searchDate.ToShortDateString()}\":");

                    // Hiển thị các kết quả tìm kiếm
                    _expenses = filteredExpenses;  // Tạm thời gán kết quả tìm kiếm vào _expenses
                    DisplayExpenses();  // Hiển thị kết quả

                    // Khôi phục lại danh sách gốc sau khi hiển thị kết quả (sắp xếp lại theo ngày)
                    _expenses.Sort((a, b) => a.Date.CompareTo(b.Date));  // Sắp xếp lại theo ngày để giữ trật tự ban đầu
                }
                else
                {
                    Console.WriteLine($"Không tìm thấy khoản chi tiêu nào với ngày \"{searchDate.ToShortDateString()}\".");
                }
            }
            else
            {
                Console.WriteLine("Định dạng ngày không hợp lệ. Vui lòng thử lại.");
            }
        }


        /// <summary>
        /// Đánh giá tình trạng chi tiêu dựa trên giới hạn chi tiêu và tổng số tiền đã chi.
        /// </summary>
        public void EvaluateSpending()
        {
            double totalSpent = _expenses.Sum(e => e.Amount);
            double remaining = _spendingLimit - totalSpent;

            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║              TỔNG QUAN VỀ CHI TIÊU                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            // Hiển thị thông tin
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine($"     Tổng số tiền đã chi tiêu: {totalSpent}                            ");
            Console.WriteLine($"     Giới hạn chi tiêu        : {_spendingLimit}                       ");
            Console.WriteLine($"     Số tiền còn lại          : {remaining}                            ");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            if (remaining < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red; // Thay đổi màu chữ thành xanh đỏ
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║CẢNH BÁO : BẠN ĐÃ VƯỢT QUÁ CHI TIÊU!!!              ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.ResetColor();

            }
            if (remaining/totalSpent < 0.3)
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Thay đổi màu chữ thành vàng
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║CẢNH BÁO : BẠN ĐÃ VƯỢT 70% CHI TIÊU!!!              ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.ResetColor();

            }
            Console.Write("Nhấn nút bất kì để tiếp tục: ");
            Console.ReadKey();
            
        }
        /// <summary>
        /// Tính toán tỷ lệ phần trăm chi tiêu theo từng danh mục.
        /// </summary>
        /// <returns>
        /// Một danh sách các bộ giá trị (category, percentage, amount) trong đó:
        /// - category: Tên danh mục chi tiêu.
        /// - percentage: Tỷ lệ phần trăm chi tiêu của danh mục so với tổng chi tiêu.
        /// - amount: Tổng số tiền đã chi tiêu cho danh mục.
        /// </returns>
        public EList<(string category, double percentage, double amount)> GetCategoryPercentages()
        {
            EList<(string category, double percentage, double amount)> result = new EList<(string, double, double)>();

            double totalSpent = _expenses.Sum(expense => expense.Amount);
            foreach (var categoryGroup in _expenses.GroupBy(expense => expense.Category))
            {
                double categoryTotal = categoryGroup.Sum(expense => expense.Amount);
                double percentage = (categoryTotal / totalSpent) * 100;
                result.Add((categoryGroup.Key, percentage, categoryTotal));
            }

            return result;
        }
        /// <summary>
        /// Đặt lại danh sách chi tiêu hàng tháng và thiết lập giới hạn chi tiêu mới.
        /// </summary>
        /// <param name="newSpendingLimit">Giới hạn chi tiêu mới cho tháng tiếp theo.</param>
        /// <remarks>
        /// Phương thức này xóa toàn bộ dữ liệu chi tiêu hiện tại và thay đổi giới hạn chi tiêu.
        /// Hiển thị thông báo khi hoàn tất.
        /// </remarks>
        public void ResetMonthlyExpenses(double newSpendingLimit)
        {
            _expenses.Clear();
            _spendingLimit = newSpendingLimit;
            Console.WriteLine("Đã làm mới chi tiêu hàng tháng và thiết lập giới hạn mới.");
        }

    }
}
