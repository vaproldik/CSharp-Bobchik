using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1.Data;
using Task1.Models;

namespace Task1.Repositories
{
    public class TaskRepository : IRepository<TaskItem>
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        // Получить все задачи
        public async Task<List<TaskItem>> GetAllAsync()
            => await _context.Tasks.ToListAsync();

        // Получить задачи конкретного пользователя
        public async Task<List<TaskItem>> GetByUserAsync(int userId)
            => await _context.Tasks
                             .Where(t => t.UserId == userId)
                             .ToListAsync();

        // Добавить задачу
        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            await SaveAsync();
        }

        // Обновить задачу
        public async Task UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await SaveAsync();
        }

        // Удалить задачу
        public async Task DeleteAsync(TaskItem task)
        {
            _context.Tasks.Remove(task);
            await SaveAsync();
        }

        // Сохранить изменения в БД
        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}