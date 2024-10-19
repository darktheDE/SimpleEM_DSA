using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Console.WriteLine($"{i}. Danh mục: {expenses[i].Category}, Số tiền: {expenses[i].Amount}, Ngày: {expenses[i].Date}, Mô tả: {expenses[i].Description}");
            }
        }

        public void DisplaySpendingLimitWarning()
        {
            Console.WriteLine("\nCẢNH BÁO: Bạn đã vượt quá giới hạn chi tiêu!");
        }

        public void DisplayRemainingSpending(double remaining)
        {
            Console.WriteLine($"\nSố tiền còn lại trong giới hạn chi tiêu: {remaining}");
        }

        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Thêm, xóa và sửa chi tiêu");
            Console.WriteLine("2. Hiển thị và sắp xếp chi tiêu");
            Console.WriteLine("3. Đánh giá mức độ sử dụng");
            Console.WriteLine("0. Thoát chương trình");
        }
    }

}
