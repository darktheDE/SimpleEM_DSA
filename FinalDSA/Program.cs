using FinalDSA.Controllers;
using FinalDSA.Models;
using FinalDSA.Views;
using System;
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        double spendingLimit = GetSpendingLimit();
        ExpenseManager manager = new ExpenseManager(spendingLimit);
        ExpenseView view = new ExpenseView();
        ExpenseController controller = new ExpenseController(manager, view);
        controller.Run();
    }

    static double GetSpendingLimit()
    {
        Console.Write("\nNhập giới hạn chi tiêu hàng tháng: ");
        return double.Parse(Console.ReadLine());
    }
}
