using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Task1.Models;

namespace Task1.Services
{
    public class TaskService
    {
        private readonly string _tasksFilePath;
        private List<TaskModel> _allTasks = new();

        public TaskService()
        {
            string dataDir = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Data");

            Directory.CreateDirectory(dataDir);
            _tasksFilePath = Path.Combine(dataDir, "tasks.json");
        }

        // -------------------------------------------------------
        // Загрузка задач текущего пользователя (async)
        // -------------------------------------------------------
        public async Task<List<TaskModel>> GetTasksByUserAsync(int userId)
        {
            await Task.Delay(2000); // отложенная загрузка по заданию
            await LoadFromFileAsync();

            return _allTasks
                .Where(t => t.UserId == userId)
                .ToList();
        }

        // -------------------------------------------------------
        // Добавить задачу
        // -------------------------------------------------------
        public async Task AddTaskAsync(TaskModel task)
        {
            await LoadFromFileAsync();

            task.Id = _allTasks.Count > 0
                ? _allTasks.Max(t => t.Id) + 1 : 1;

            _allTasks.Add(task);
            await SaveToFileAsync();
        }

        // -------------------------------------------------------
        // Обновить задачу
        // -------------------------------------------------------
        public async Task UpdateTaskAsync(TaskModel updatedTask)
        {
            await LoadFromFileAsync();

            var existing = _allTasks.FirstOrDefault(t => t.Id == updatedTask.Id);
            if (existing != null)
            {
                existing.Title = updatedTask.Title;
                existing.Status = updatedTask.Status;
                existing.Category = updatedTask.Category;
                existing.Deadline = updatedTask.Deadline;
                existing.Description = updatedTask.Description;
            }

            await SaveToFileAsync();
        }

        // -------------------------------------------------------
        // Удалить задачу
        // -------------------------------------------------------
        public async Task DeleteTaskAsync(int taskId)
        {
            await LoadFromFileAsync();
            _allTasks.RemoveAll(t => t.Id == taskId);
            await SaveToFileAsync();
        }

        // -------------------------------------------------------
        // Работа с файлом
        // -------------------------------------------------------
        private async Task LoadFromFileAsync()
        {
            if (!File.Exists(_tasksFilePath))
            {
                _allTasks = new List<TaskModel>();
                return;
            }

            try
            {
                string json = await File.ReadAllTextAsync(_tasksFilePath);

                _allTasks = JsonSerializer.Deserialize<List<TaskModel>>(json)
                            ?? new List<TaskModel>();
            }
            catch
            {
                _allTasks = new List<TaskModel>();
            }
        }

        private async Task SaveToFileAsync()
        {
            string json = JsonSerializer.Serialize(_allTasks,
                new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(_tasksFilePath, json);
        }
    }
}