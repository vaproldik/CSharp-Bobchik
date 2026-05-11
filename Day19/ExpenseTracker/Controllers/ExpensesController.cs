using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ExpensesController : Controller
    {
        private static List<Expense> expenses = new List<Expense>()
        {
            new Expense { Id = 1, Amount = 500, Category = "Еда", Date = DateTime.Now },
            new Expense { Id = 2, Amount = 1200, Category = "Транспорт", Date = DateTime.Now },
            new Expense { Id = 3, Amount = 3000, Category = "Развлечения", Date = DateTime.Now }
        };

        public IActionResult Index()
        {
            return View(expenses);
        }

        [HttpGet]
        [Route("Expenses/Filter/{category}")]
        public IActionResult Filter(string category)
        {
            var filtered = expenses
                .Where(e => e.Category != null &&
                            e.Category.ToLower() == category.ToLower())
                .ToList();

            return View("Index", filtered);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            expense.Id = expenses.Count + 1;
            expenses.Add(expense);

            return RedirectToAction("Index");
        }
    }
}