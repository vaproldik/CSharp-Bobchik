using ExpenseTracker.Models;
using ExpenseTracker.Services;
using ExpenseTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public IActionResult Index()
        {
            var expenses = _expenseService.GetAll();
            return View(expenses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ExpenseViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var expense = new Expense
            {
                Amount = model.Amount,
                Category = model.Category,
                Date = model.Date
            };

            _expenseService.Add(expense);

            TempData["Success"] = "Расход успешно добавлен!";

            return RedirectToAction("Index");
        }

        // ✅ Добавили [Route] чтобы работал URL /Expenses/Filter/{category}
        [HttpGet]
        [Route("Expenses/Filter/{category}")]
        public IActionResult Filter(string category)
        {
            var filtered = _expenseService.FilterByCategory(category);
            return View("Index", filtered);
        }
    }
}