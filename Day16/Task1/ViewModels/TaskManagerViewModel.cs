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
        private readonly NotificationService _notificationService;
        private readonly ChatService _chatService;
        private readonly AuthService _authService;

        private TaskModel? _selectedTask;
        private bool _isLoading;
        private string _loadingText = "Загрузка...";
        private string _notificationText = string.Empty;
        private string _statusMessage = string.Empty;

        public ObservableCollection<TaskModel> Tasks { get; } = new();

        public string CurrentUser =>
            _authService.CurrentUser?.DisplayName ?? "Гость";

        public TaskModel? SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(nameof(SelectedTask)); }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        public string LoadingText
        {
            get => _loadingText;
            set { _loadingText = value; OnPropertyChanged(nameof(LoadingText)); }
        }

        public string NotificationText
        {
            get => _notificationText;
            set { _notificationText = value; OnPropertyChanged(nameof(NotificationText)); }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(nameof(StatusMessage)); }
        }

        public ICommand LoadCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand OpenChatCommand { get; }

        public TaskManagerViewModel(AuthService authService)
        {
            _authService = authService;
            _taskService = new TaskService();
            _notificationService = new NotificationService();
            _chatService = new ChatService();

            LoadCommand = new RelayCommand(async _ => await LoadTasksAsync());
            AddCommand = new RelayCommand(async _ => await AddTaskAsync());
            DeleteCommand = new RelayCommand(
                async _ => await DeleteTaskAsync(),
                _ => SelectedTask != null);

            // Открытие чата — передаём нужные сервисы
            OpenChatCommand = new RelayCommand(_ =>
            {
                var chat = new ChatWindow(_chatService, _authService);
                chat.Show();
            });

            _notificationService.NotificationReceived += msg =>
                Application.Current.Dispatcher.Invoke(
                    () => NotificationText = $"🔔 {msg}");

            _notificationService.StartListening();
            _chatService.StartServer();

            _ = LoadTasksAsync();
        }

        private async Task LoadTasksAsync()
        {
            if (_authService.CurrentUser == null) return;

            IsLoading = true;
            var anim = AnimateLoadingAsync();

            try
            {
                Tasks.Clear();
                var list = await _taskService
                    .GetTasksByUserAsync(_authService.CurrentUser.Id);

                foreach (var t in list) Tasks.Add(t);
                StatusMessage = $"Загружено задач: {Tasks.Count}";
            }
            finally
            {
                IsLoading = false;
                await anim;
                LoadingText = "Загрузка...";
            }
        }

        private async Task AnimateLoadingAsync()
        {
            string[] dots = { "Загрузка.", "Загрузка..", "Загрузка..." };
            int i = 0;
            while (IsLoading)
            {
                LoadingText = dots[i++ % 3];
                await Task.Delay(300);
            }
        }

        private async Task AddTaskAsync()
        {
            if (_authService.CurrentUser == null) return;

            var task = new TaskModel
            {
                Title = "Новая задача",
                Status = "Ожидание",
                Category = "Общее",
                Deadline = DateTime.Now.AddDays(7),
                UserId = _authService.CurrentUser.Id
            };

            await _taskService.AddTaskAsync(task);
            Tasks.Add(task);

            _notificationService.SendNotification(
                _authService.CurrentUser.DisplayName, task.Title);

            StatusMessage = $"Задача «{task.Title}» добавлена.";
        }

        private async Task DeleteTaskAsync()
        {
            if (SelectedTask == null) return;

            var res = MessageBox.Show(
                $"Удалить «{SelectedTask.Title}»?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (res == MessageBoxResult.Yes)
            {
                await _taskService.DeleteTaskAsync(SelectedTask.Id);
                StatusMessage = $"Задача «{SelectedTask.Title}» удалена.";
                Tasks.Remove(SelectedTask);
                SelectedTask = null;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string prop)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}