using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task1.Models;

namespace Task1.Services
{
    public class TaskService
    {
        public async Task<List<TaskCategoryModel>> GetCategoriesAsync()
        {
            await Task.Delay(1000);

            return new List<TaskCategoryModel>
            {
                new TaskCategoryModel { Id = 1, Name = "Учёба" },
                new TaskCategoryModel { Id = 2, Name = "Работа" },
                new TaskCategoryModel { Id = 3, Name = "Дом" }
            };
        }

        public async Task<List<TaskModel>> GetTasksAsync(List<TaskCategoryModel> categories)
        {
            await Task.Delay(2000);

            return new List<TaskModel>
            {
                new TaskModel
                {
                    Id = 1,
                    Title = "Подготовить отчет",
                    Status = "В работе",
                    Deadline = DateTime.Now.AddDays(2),
                    Category = categories.Find(c => c.Name == "Работа")
                },
                new TaskModel
                {
                    Id = 2,
                    Title = "Сделать домашнее задание",
                    Status = "Ожидание",
                    Deadline = DateTime.Now.AddDays(1),
                    Category = categories.Find(c => c.Name == "Учёба")
                },
                new TaskModel
                {
                    Id = 3,
                    Title = "Купить продукты",
                    Status = "Выполнено",
                    Deadline = DateTime.Now.AddDays(3),
                    Category = categories.Find(c => c.Name == "Дом")
                }
            };
        }

        public void AddTask(ICollection<TaskModel> tasks, TaskModel task)
        {
            tasks.Add(task);
        }

        public void DeleteTask(ICollection<TaskModel> tasks, TaskModel task)
        {
            tasks.Remove(task);
        }
    }
}