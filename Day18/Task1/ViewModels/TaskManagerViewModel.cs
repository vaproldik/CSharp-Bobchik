using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Task1.Commands;
using Task1.Data;
using Task1.Models;
using Task1.Repositories;
using Task1.Services;
using Task1.Views;

namespace Task1.ViewModels
{
    public class TaskManagerViewModel : INotifyPropertyChanged
    {
        private readonly TaskRepository _repository;
        private readonly AuthService _authService;

        private TaskItem? _selectedTask;
        private bool _isLoading;
        private string _loadingText = "Загрузка...";
        private string _statusMessage = string.Empty;

        public ObservableCollection<TaskItem> Tasks { get; } = new();

        public string CurrentUser =>
            _authService.CurrentUser?.DisplayName ?? "Гость";

        public TaskItem? SelectedTask
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
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        public string LoadingText
        {
            get => _loadingText;
            set { _loadingText = value; OnPropertyChanged(nameof(LoadingText)); }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(nameof(StatusMessage)); }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CompleteTaskCommand { get; }
        public ICommand SetInProgressCommand { get; }

        public TaskManagerViewModel(AuthService authService)
        {
            _authService = authService;
            _repository = new TaskRepository(new AppDbContext());

            AddCommand = new AsyncRelayCommand(AddTaskAsync);

            EditCommand = new AsyncRelayCommand(
                EditTaskAsync,
                () => SelectedTask != null);

            DeleteCommand = new AsyncRelayCommand(
                DeleteTaskAsync,
                () => SelectedTask != null);

            CompleteTaskCommand = new AsyncRelayCommand(
                () => ChangeStatusAsync("Выполнено"),
                () => SelectedTask != null);

            SetInProgressCommand = new AsyncRelayCommand(
                () => ChangeStatusAsync("В работе"),
                () => SelectedTask != null);

            // Загрузка при старте — автоматически
            _ = LoadTasksAsync();
        }

        // -------------------------------------------------------
        // Загрузка задач из БД
        // -------------------------------------------------------
        private async Task LoadTasksAsync()
        {
            if (_authService.CurrentUser == null) return;

            IsLoading = true;
            Task animTask = AnimateLoadingAsync();

            try
            {
                var list = await _repository
                    .GetByUserAsync(_authService.CurrentUser.Id);

                Tasks.Clear();
                foreach (var t in list)
                    Tasks.Add(t);

                StatusMessage = $"Задач в базе: {Tasks.Count}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка загрузки: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
                await animTask;
                LoadingText = "Загрузка...";
            }
        }

        // -------------------------------------------------------
        // Добавить — диалог → сохранить в БД
        // -------------------------------------------------------
        private async Task AddTaskAsync()
        {
            if (_authService.CurrentUser == null) return;

            // Открываем диалог без Owner чтобы не было вылета
            var dialog = new TaskDialog();

            if (dialog.ShowDialog() == true && dialog.Result != null)
            {
                dialog.Result.UserId = _authService.CurrentUser.Id;

                // AddAsync внутри вызывает SaveChangesAsync
                await _repository.AddAsync(dialog.Result);

                Tasks.Add(dialog.Result);
                StatusMessage = $"Задача «{dialog.Result.Title}» добавлена.";
            }
        }

        // -------------------------------------------------------
        // Редактировать — диалог с данными → обновить в БД
        // -------------------------------------------------------
        private async Task EditTaskAsync()
        {
            if (SelectedTask == null) return;

            // Передаём задачу в диалог для редактирования
            var dialog = new TaskDialog(SelectedTask);

            if (dialog.ShowDialog() == true)
            {
                // UpdateAsync внутри вызывает SaveChangesAsync
                await _repository.UpdateAsync(SelectedTask);

                // Перезагружаем чтобы UI обновился
                await LoadTasksAsync();
                StatusMessage = $"Задача обновлена в БД.";
            }
        }

        // -------------------------------------------------------
        // Удалить — подтверждение → удалить из БД
        // -------------------------------------------------------
        private async Task DeleteTaskAsync()
        {
            if (SelectedTask == null) return;

            var result = MessageBox.Show(
                $"Удалить задачу «{SelectedTask.Title}»?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                string title = SelectedTask.Title;

                // DeleteAsync внутри вызывает SaveChangesAsync
                await _repository.DeleteAsync(SelectedTask);

                Tasks.Remove(SelectedTask);
                SelectedTask = null;
                StatusMessage = $"Задача «{title}» удалена.";
            }
        }

        // -------------------------------------------------------
        // Смена статуса
        // -------------------------------------------------------
        private async Task ChangeStatusAsync(string newStatus)
        {
            if (SelectedTask == null) return;

            SelectedTask.Status = newStatus;

            // UpdateAsync → SaveChangesAsync
            await _repository.UpdateAsync(SelectedTask);

            StatusMessage = $"«{SelectedTask.Title}» → {newStatus}";
            await LoadTasksAsync();
        }

        // -------------------------------------------------------
        // Анимация загрузки
        // -------------------------------------------------------
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string prop)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}