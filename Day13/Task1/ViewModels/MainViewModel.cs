using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Task1.Commands;
using Task1.Models;
using Task1.Views;

namespace Task1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // -------------------------------------------------------
        // Коллекции
        // -------------------------------------------------------
        public ObservableCollection<TaskItem> AllTasks { get; } = new ObservableCollection<TaskItem>();
        public ObservableCollection<TaskItem> FilteredTasks { get; } = new ObservableCollection<TaskItem>();

        // -------------------------------------------------------
        // Выбранная задача
        // -------------------------------------------------------
        private TaskItem _selectedTask;
        public TaskItem SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(nameof(SelectedTask)); }
        }

        // -------------------------------------------------------
        // Фильтр по статусу
        // -------------------------------------------------------
        private string _filterStatus = "Все";
        public string FilterStatus
        {
            get => _filterStatus;
            set { _filterStatus = value; OnPropertyChanged(nameof(FilterStatus)); ApplyFilter(); }
        }

        // -------------------------------------------------------
        // Строка поиска
        // -------------------------------------------------------
        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); ApplyFilter(); }
        }

        // -------------------------------------------------------
        // Счётчики для статусной строки
        // -------------------------------------------------------
        private string _statusMessage = "Готово";
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(nameof(StatusMessage)); }
        }

        public string TotalCount => $"Всего: {AllTasks.Count}";
        public string InProgressCount => $"В работе: {AllTasks.Count(t => t.Status == "В работе")}";
        public string DoneCount => $"Выполнено: {AllTasks.Count(t => t.Status == "Выполнено")}";

        // -------------------------------------------------------
        // Команды
        // -------------------------------------------------------
        public ICommand AddTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        // Команды меню
        public ICommand ExitCommand { get; }
        public ICommand AboutCommand { get; }

        private int _nextId = 1;

        // -------------------------------------------------------
        // Конструктор
        // -------------------------------------------------------
        public MainViewModel()
        {
            AddTaskCommand = new RelayCommand(_ => AddTask());
            EditTaskCommand = new RelayCommand(_ => EditTask(), _ => SelectedTask != null);
            DeleteTaskCommand = new RelayCommand(_ => DeleteTask(), _ => SelectedTask != null);
            ExitCommand = new RelayCommand(_ => Application.Current.Shutdown());
            AboutCommand = new RelayCommand(_ => MessageBox.Show(
                "Task Manager v2.0\nУправление задачами",
                "О программе", MessageBoxButton.OK, MessageBoxImage.Information));

            LoadSampleData();
            ApplyFilter();
        }

        // -------------------------------------------------------
        // Загрузка тестовых данных
        // -------------------------------------------------------
        private void LoadSampleData()
        {
            AddToCollection(new TaskItem { Title = "Разработать главное меню", Priority = "Высокий", Status = "В работе", Description = "Создать навигационное меню" });
            AddToCollection(new TaskItem { Title = "Написать документацию", Priority = "Низкий", Status = "Отложено", Description = "Описать все модули системы" });
            AddToCollection(new TaskItem { Title = "Провести тестирование", Priority = "Средний", Status = "В работе", Description = "Протестировать сценарии" });
            AddToCollection(new TaskItem { Title = "Исправить баг в авторизации", Priority = "Высокий", Status = "Выполнено", Description = "Ошибка при пустом пароле" });
            AddToCollection(new TaskItem { Title = "Обновить зависимости", Priority = "Низкий", Status = "Выполнено", Description = "Обновить NuGet-пакеты" });
        }

        private void AddToCollection(TaskItem task)
        {
            task.Id = _nextId++;
            AllTasks.Add(task);
        }

        // -------------------------------------------------------
        // Фильтрация
        // -------------------------------------------------------
        private void ApplyFilter()
        {
            FilteredTasks.Clear();
            string search = SearchText?.Trim().ToLower() ?? "";

            foreach (var task in AllTasks)
            {
                bool statusMatch = FilterStatus == "Все" || task.Status == FilterStatus;
                bool searchMatch = string.IsNullOrEmpty(search)
                                   || task.Title.ToLower().Contains(search)
                                   || (task.Description?.ToLower().Contains(search) ?? false);

                if (statusMatch && searchMatch)
                    FilteredTasks.Add(task);
            }

            RefreshCounters();
        }

        private void RefreshCounters()
        {
            OnPropertyChanged(nameof(TotalCount));
            OnPropertyChanged(nameof(InProgressCount));
            OnPropertyChanged(nameof(DoneCount));
        }

        // -------------------------------------------------------
        // Логика команд
        // -------------------------------------------------------
        private void AddTask()
        {
            var dialog = new TaskDialog { Owner = Application.Current.MainWindow };

            if (dialog.ShowDialog() == true)
            {
                AddToCollection(dialog.Result);
                ApplyFilter();
                StatusMessage = $"Задача «{dialog.Result.Title}» добавлена.";
            }
        }

        private void EditTask()
        {
            if (SelectedTask == null) return;

            var dialog = new TaskDialog(SelectedTask) { Owner = Application.Current.MainWindow };

            if (dialog.ShowDialog() == true)
            {
                ApplyFilter();
                StatusMessage = $"Задача «{SelectedTask.Title}» обновлена.";
            }
        }

        private void DeleteTask()
        {
            if (SelectedTask == null) return;

            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить задачу:\n«{SelectedTask.Title}»?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                string title = SelectedTask.Title;
                AllTasks.Remove(SelectedTask);
                SelectedTask = null;
                ApplyFilter();
                StatusMessage = $"Задача «{title}» удалена.";
            }
        }

        // -------------------------------------------------------
        // INotifyPropertyChanged
        // -------------------------------------------------------
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}