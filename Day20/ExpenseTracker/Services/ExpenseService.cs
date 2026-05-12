using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public class ExpenseService : IExpenseService
    {
        private static List<Expense> expenses = new List<Expense>()
        {
            new Expense { Id = 1, Amount = 500, Category = "Еда", Date = DateTime.Now },
            new Expense { Id = 2, Amount = 1200, Category = "Транспорт", Date = DateTime.Now }
        };

        public List<Expense> GetAll()
        {
            return expenses;
        }

        public void Add(Expense expense)
        {
            expense.Id = expenses.Count + 1;
            expenses.Add(expense);
        }

        public List<Expense> FilterByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return expenses;

            return expenses
                .Where(e => !string.IsNullOrEmpty(e.Category) &&
                            e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        public decimal GetTotalByPeriod(DateTime start, DateTime end)
        {
            return expenses
                .Where(e => e.Date >= start && e.Date <= end)
                .Sum(e => e.Amount);
        }
    }
}