using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using Task1.Models;

namespace Task1.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; } = null!;
        public DbSet<UserModel> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string folder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Data");

            Directory.CreateDirectory(folder);

            string dbPath = Path.Combine(folder, "tasks.db");
            options.UseSqlite($"Data Source={dbPath}");
        }
    }
}