using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public interface IExpenseService
    {
        List<Expense> GetAll();
        void Add(Expense expense);
        List<Expense> FilterByCategory(string category);
        decimal GetTotalByPeriod(DateTime start, DateTime end);
    }
}