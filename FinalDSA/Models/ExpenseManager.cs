using FinalDSA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalDSA.Models
{
    public class ExpenseManager
    {
        private List<Expense> _expenses;
        private double _spendingLimit;
        private readonly string _filePath = "data.txt";

        // Khai báo _searchResults là trường của lớp
        private List<Expense> _searchResults;

        public ExpenseManager(double spendingLimit)
        {
            _expenses = new List<Expense>();
            _spendingLimit = spendingLimit;
            _searchResults = new List<Expense>(); // Khởi tạo _searchResults
            LoadData();
        }

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

        public void AddExpense(string category, double amount, DateTime date, string description)
        {
            Expense expense = new Expense(category, amount, date, description);
            _expenses.Add(expense);
            SaveData();
            Console.WriteLine("\nChi tiêu đã được thêm thành công!");
            EvaluateSpending();
        }

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

        public static List<T> OrderBy<T>(List<T> list, Func<T, object> keySelector)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count - 1 - i; j++)
                {
                    if (Comparer<object>.Default.Compare(keySelector(list[j]), keySelector(list[j + 1])) > 0)
                    {
                        var temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
            return list;
        }

        public static List<T> FindAll<T>(List<T> list, Func<T, bool> predicate)
        {
            List<T> result = new List<T>();
            foreach (var item in list)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public static Dictionary<TKey, List<T>> GroupBy<T, TKey>(List<T> list, Func<T, TKey> keySelector)
        {
            Dictionary<TKey, List<T>> grouped = new Dictionary<TKey, List<T>>();

            foreach (var item in list)
            {
                TKey key = keySelector(item);
                if (!grouped.ContainsKey(key))
                {
                    grouped[key] = new List<T>();
                }
                grouped[key].Add(item);
            }

            return grouped;
        }

        public void DisplayExpenses()
        {
            if (_expenses.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║              BẢNG DANH SÁCH CHI TIÊU               ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");

                Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║ {0,-5} {1,-20} {2,-15} {3,-25} {4,-30} ║", "STT", "Danh mục", "Số tiền", "Ngày", "Mô tả");
                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════╝");

                for (int i = 0; i < _expenses.Count; i++)
                {
                    Console.WriteLine("║ {0,-5} {1,-20} {2,-15:F2} {3,-25} {4,-30} ║",
                        i + 1, _expenses[i].Category, _expenses[i].Amount, _expenses[i].Date.ToString("yyyy-MM-dd HH:mm:ss"), _expenses[i].Description);
                }

                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            }
            else
            {
                Console.WriteLine("\nChưa có chi tiêu nào.");
            }
        }

        public void SortByCategory()
        {
            _expenses = OrderBy(_expenses, e => e.Category);
            DisplayExpenses();
        }

        public void SortByAmount()
        {
            _expenses = OrderBy(_expenses, e => e.Amount);
            Console.WriteLine("\nDanh sách đã được sắp xếp theo số tiền.");
            DisplayExpenses();
        }

        public void SortByDate()
        {
            _expenses = OrderBy(_expenses, e => e.Date);
            Console.WriteLine("\nDanh sách đã được sắp xếp theo ngày.");
            DisplayExpenses();
        }

        // Sửa lại phương thức tìm kiếm
        public void SearchByCategory(string searchCategory)
        {
            _searchResults = FindAll(_expenses, expense => expense.Category.ToLower().Contains(searchCategory.ToLower()));

            if (_searchResults.Count > 0)
            {
                Console.WriteLine($"\nKết quả tìm kiếm cho danh mục \"{searchCategory}\":");

                var originalExpenses = _expenses;
                _expenses = _searchResults;
                DisplayExpenses();
                _expenses = originalExpenses;
            }
            else
            {
                Console.WriteLine($"Không tìm thấy khoản chi tiêu nào trong danh mục \"{searchCategory}\".");
            }
        }

        public void SearchByDescription(string searchDescription)
        {
            _searchResults = FindAll(_expenses, expense => expense.Description.ToLower().Contains(searchDescription.ToLower()));

            if (_searchResults.Count > 0)
            {
                Console.WriteLine($"\nKết quả tìm kiếm cho mô tả \"{searchDescription}\":");

                var originalExpenses = _expenses;
                _expenses = _searchResults;
                DisplayExpenses();
                _expenses = originalExpenses;
            }
            else
            {
                Console.WriteLine($"Không tìm thấy khoản chi tiêu nào với mô tả \"{searchDescription}\".");
            }
        }

        public void SearchByDate(string inputDate)
        {
            if (DateTime.TryParse(inputDate, out DateTime searchDate))
            {
                var filteredExpenses = FindAll(_expenses, expense => expense.Date.Date == searchDate.Date);
                _expenses = filteredExpenses;
                DisplayExpenses();
            }
            else
            {
                Console.WriteLine("Định dạng ngày không hợp lệ. Vui lòng thử lại.");
            }
        }

        public void EvaluateSpending()
        {
            double totalSpent = _expenses.Sum(e => e.Amount);
            double remaining = _spendingLimit - totalSpent;

            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║              TỔNG QUAN VỀ CHI TIÊU                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine($"     Tổng số tiền đã chi tiêu: {totalSpent}                            ");
            Console.WriteLine($"     Giới hạn chi tiêu        : {_spendingLimit}                       ");
            Console.WriteLine($"     Số tiền còn lại          : {remaining}                            ");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            if (remaining < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║CẢNH BÁO : BẠN ĐÃ VƯỢT QUÁ CHI TIÊU!!!              ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.ResetColor();
            }
            if (remaining / totalSpent < 0.3)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║CẢNH BÁO : BẠN ĐÃ VƯỢT 70% CHI TIÊU!!!              ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.ResetColor();
            }
        }

        public List<(string category, double percentage, double amount)> GetCategoryPercentages()
        {
            List<(string category, double percentage, double amount)> result = new List<(string, double, double)>();

            double totalSpent = _expenses.Sum(expense => expense.Amount);
            foreach (var categoryGroup in GroupBy(_expenses, expense => expense.Category))
            {
                double categoryTotal = categoryGroup.Value.Sum(expense => expense.Amount);
                double percentage = (categoryTotal / totalSpent) * 100;
                result.Add((categoryGroup.Key, percentage, categoryTotal));
            }

            return result;
        }

        public void ResetMonthlyExpenses(double newSpendingLimit)
        {
            _expenses.Clear();
            _spendingLimit = newSpendingLimit;
            Console.WriteLine("Đã làm mới chi tiêu hàng tháng và thiết lập giới hạn mới.");
        }
    }
}
