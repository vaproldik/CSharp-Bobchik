using System;
using System.Collections.Generic;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public interface IExpenseService
    {
        decimal GetTotalForPeriod(DateTime dateFrom, DateTime dateTo);

        decimal GetTotalByCategory(string category);

        IEnumerable<string> GetAllCategories();

        IEnumerable<Expense> GetFilteredExpenses(
            string category = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null
        );
    }
}