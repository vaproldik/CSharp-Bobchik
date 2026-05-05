using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Task1.Commands;
using Task1.Models;
using Task1.Services;

namespace Task1.ViewModels
{
    public class TaskManagerViewModel : INotifyPropertyChanged
    {
        private readonly TaskService _taskService;
        private TaskModel? _selectedTask;
        private bool _isLoading;
        private string _loadingText = "Загрузка...";

        public ObservableCollection<TaskModel> Tasks { get; set; }
        public ObservableCollection<TaskCategoryModel> Categories { get; set; }

        public TaskModel? SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public string LoadingText
        {
            get => _loadingText;
            set
            {
                _loadingText = value;
                OnPropertyChanged(nameof(LoadingText));
            }
        }

        public ICommand LoadTasksCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        public TaskManagerViewModel()
        {
            _taskService = new TaskService();
            Tasks = new ObservableCollection<TaskModel>();
            Categories = new ObservableCollection<TaskCategoryModel>();

            LoadTasksCommand = new RelayCommand(async _ => await LoadTasksAsync());
            AddTaskCommand = new RelayCommand(_ => AddTask());
            DeleteTaskCommand = new RelayCommand(_ => DeleteTask(), _ => SelectedTask != null);

            _ = LoadTasksAsync();
        }

        public async Task LoadTasksAsync()
        {
            IsLoading = true;

            var animationTask = AnimateLoadingAsync();

            Categories.Clear();
            var categories = await _taskService.GetCategoriesAsync();
            foreach (var category in categories)
                Categories.Add(category);

            Tasks.Clear();
            var tasks = await _taskService.GetTasksAsync(new System.Collections.Generic.List<TaskCategoryModel>(Categories));
            foreach (var task in tasks)
                Tasks.Add(task);

            IsLoading = false;
            await animationTask;
            LoadingText = "Загрузка...";
        }

        private async Task AnimateLoadingAsync()
        {
            string[] states =
            {
                "Загрузка.",
                "Загрузка..",
                "Загрузка..."
            };

            int index = 0;

            while (IsLoading)
            {
                LoadingText = states[index];
                index = (index + 1) % states.Length;
                await Task.Delay(300);
            }
        }

        private void AddTask()
        {
            TaskCategoryModel? category = Categories.Count > 0 ? Categories[0] : null;

            var newTask = new TaskModel
            {
                Id = Tasks.Count + 1,
                Title = "Новая задача",
                Status = "Ожидание",
                Deadline = DateTime.Now.AddDays(7),
                Category = category
            };

            _taskService.AddTask(Tasks, newTask);
        }

        private void DeleteTask()
        {
            if (SelectedTask == null)
                return;

            var result = MessageBox.Show(
                $"Удалить задачу \"{SelectedTask.Title}\"?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _taskService.DeleteTask(Tasks, SelectedTask);
                SelectedTask = null;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}