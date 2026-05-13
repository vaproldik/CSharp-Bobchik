using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _context;

        // Внедрение зависимости через конструктор (DI)
        public ExpenseService(AppDbContext context)
        {
            _context = context;
        }

        // Подсчёт суммы расходов за указанный период
        public decimal GetTotalForPeriod(DateTime dateFrom, DateTime dateTo)
        {
            return _context.Expenses
                .Where(e => e.Date >= dateFrom && e.Date <= dateTo)
                .Sum(e => (decimal?)e.Amount) ?? 0m;
        }

        // Подсчёт суммы расходов по категории
        public decimal GetTotalByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return 0m;

            return _context.Expenses
                .Where(e => e.Category == category)
                .Sum(e => (decimal?)e.Amount) ?? 0m;
        }

        // Получение уникальных категорий из базы данных
        public IEnumerable<string> GetAllCategories()
        {
            // Стандартные категории + те что есть в базе
            var defaultCategories = new List<string>
            {
                "Еда",
                "Транспорт",
                "Жильё",
                "Развлечения",
                "Здоровье",
                "Одежда",
                "Связь",
                "Образование",
                "Прочее"
            };

            var dbCategories = _context.Expenses
                .Select(e => e.Category)
                .Distinct()
                .ToList();

            // Объединяем и убираем дубликаты
            return defaultCategories
                .Union(dbCategories)
                .OrderBy(c => c)
                .ToList();
        }

        // Получение расходов с фильтрацией по категории и датам
        public IEnumerable<Expense> GetFilteredExpenses(
            string category = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null)
        {
            IQueryable<Expense> query = _context.Expenses;

            // Фильтр по категории
            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(e => e.Category == category);

            // Фильтр по дате от
            if (dateFrom.HasValue)
                query = query.Where(e => e.Date >= dateFrom.Value);

            // Фильтр по дате до
            if (dateTo.HasValue)
                query = query.Where(e => e.Date <= dateTo.Value);

            // Сортировка по дате (сначала новые)
            return query.OrderByDescending(e => e.Date).ToList();
        }
    }
}