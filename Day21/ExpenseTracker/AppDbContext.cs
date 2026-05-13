using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ExpenseTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Таблица расходов в базе данных
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Начальные данные для тестирования
            modelBuilder.Entity<Expense>().HasData(
                new Expense
                {
                    Id = 1,
                    Title = "Продукты в магазине",
                    Amount = 2500.00m,
                    Date = new DateTime(2024, 1, 10),
                    Category = "Еда"
                },
                new Expense
                {
                    Id = 2,
                    Title = "Билет на автобус",
                    Amount = 150.00m,
                    Date = new DateTime(2024, 1, 11),
                    Category = "Транспорт"
                },
                new Expense
                {
                    Id = 3,
                    Title = "Кино с друзьями",
                    Amount = 800.00m,
                    Date = new DateTime(2024, 1, 12),
                    Category = "Развлечения"
                }
            );
        }
    }
}