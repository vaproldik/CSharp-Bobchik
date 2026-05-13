using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Services;
using ExpenseTracker.ViewModels;

namespace ExpenseTracker.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IExpenseService _expenseService;

        // Внедрение зависимостей через конструктор
        public ExpensesController(AppDbContext context, IExpenseService expenseService)
        {
            _context = context;
            _expenseService = expenseService;
        }

        // ─────────────────────────────────────────────────────────────
        // GET: /Expenses/Index — главная страница со списком и фильтрами
        // ─────────────────────────────────────────────────────────────
        public async Task<IActionResult> Index(
            string filterCategory = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null)
        {
            // Получаем расходы с учётом фильтров через сервис
            var expenses = _expenseService.GetFilteredExpenses(
                filterCategory, dateFrom, dateTo);

            // Передаём категории для выпадающего списка
            ViewBag.Categories = _expenseService.GetAllCategories();

            // Передаём общую сумму отфильтрованных расходов
            ViewBag.TotalAmount = expenses.Sum(e => e.Amount);

            // Передаём текущие значения фильтров обратно во View
            ViewBag.CurrentCategory = filterCategory;
            ViewBag.DateFrom = dateFrom?.ToString("yyyy-MM-dd");
            ViewBag.DateTo = dateTo?.ToString("yyyy-MM-dd");

            // Проверяем наличие уведомления (TempData от предыдущей операции)
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }

            return View(expenses);
        }

        // ─────────────────────────────────────────────────────────────
        // GET: /Expenses/Filter/{category} — маршрут фильтра по категории
        // ─────────────────────────────────────────────────────────────
        [Route("Expenses/Filter/{category}")]
        public IActionResult Filter(string category)
        {
            var expenses = _expenseService.GetFilteredExpenses(category: category);

            ViewBag.Categories = _expenseService.GetAllCategories();
            ViewBag.TotalAmount = expenses.Sum(e => e.Amount);
            ViewBag.CurrentCategory = category;

            return View("Index", expenses);
        }

        // ─────────────────────────────────────────────────────────────
        // GET: /Expenses/Create — форма добавления расхода
        // ─────────────────────────────────────────────────────────────
        public IActionResult Create()
        {
            ViewBag.Categories = _expenseService.GetAllCategories();
            return View(new ExpenseViewModel { Date = DateTime.Today });
        }

        // ─────────────────────────────────────────────────────────────
        // POST: /Expenses/Create — сохранение нового расхода
        // ─────────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Маппинг ViewModel → Model
                var expense = new Expense
                {
                    Title = model.Title,
                    Amount = model.Amount,
                    Date = model.Date,
                    Category = model.Category
                };

                _context.Add(expense);
                await _context.SaveChangesAsync();

                // Уведомление через TempData (сохраняется между запросами)
                TempData["SuccessMessage"] =
                    $"✅ Расход «{expense.Title}» на сумму {expense.Amount:N2} ₽ успешно добавлен!";

                return RedirectToAction(nameof(Index));
            }

            // Если валидация не прошла — вернуть форму с ошибками
            ViewBag.Categories = _expenseService.GetAllCategories();
            return View(model);
        }

        // ─────────────────────────────────────────────────────────────
        // GET: /Expenses/Edit/5 — форма редактирования расхода
        // ─────────────────────────────────────────────────────────────
        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
                return NotFound();

            // Маппинг Model → ViewModel
            var model = new ExpenseViewModel
            {
                Id = expense.Id,
                Title = expense.Title,
                Amount = expense.Amount,
                Date = expense.Date,
                Category = expense.Category
            };

            ViewBag.Categories = _expenseService.GetAllCategories();
            return View(model);
        }

        // ─────────────────────────────────────────────────────────────
        // POST: /Expenses/Edit/5 — сохранение изменений расхода
        // ─────────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var expense = await _context.Expenses.FindAsync(id);

                if (expense == null)
                    return NotFound();

                // Обновляем поля
                expense.Title = model.Title;
                expense.Amount = model.Amount;
                expense.Date = model.Date;
                expense.Category = model.Category;

                _context.Update(expense);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] =
                    $"✏️ Расход «{expense.Title}» успешно обновлён!";

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _expenseService.GetAllCategories();
            return View(model);
        }

        // ─────────────────────────────────────────────────────────────
        // GET: /Expenses/Delete/5 — страница подтверждения удаления
        // ─────────────────────────────────────────────────────────────
        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
                return NotFound();

            return View(expense);
        }

        // ─────────────────────────────────────────────────────────────
        // POST: /Expenses/Delete/5 — подтверждение удаления
        // ─────────────────────────────────────────────────────────────
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
                return NotFound();

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] =
                $"🗑️ Расход «{expense.Title}» удалён.";

            return RedirectToAction(nameof(Index));
        }
    }
}