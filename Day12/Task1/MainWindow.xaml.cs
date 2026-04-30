using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Task1
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<TaskItem> _allTasks;
        private ObservableCollection<TaskItem> _filteredTasks;
        private int _nextId = 1;

        public MainWindow()
        {
            InitializeComponent();
            _allTasks = new ObservableCollection<TaskItem>();
            _filteredTasks = new ObservableCollection<TaskItem>();
            TasksListBox.ItemsSource = _filteredTasks;

            LoadSampleData();
            ApplyFilter();
        }

        private void LoadSampleData()
        {
            AddTask(new TaskItem
            {
                Title = "Разработать главное меню",
                Priority = "Высокий",
                Status = "В работе",
                Description = "Создать навигационное меню приложения"
            });
            AddTask(new TaskItem
            {
                Title = "Написать документацию",
                Priority = "Низкий",
                Status = "Отложено",
                Description = "Описать все модули системы"
            });
            AddTask(new TaskItem
            {
                Title = "Провести тестирование",
                Priority = "Средний",
                Status = "В работе",
                Description = "Протестировать все сценарии использования"
            });
            AddTask(new TaskItem
            {
                Title = "Исправить баг в авторизации",
                Priority = "Высокий",
                Status = "Выполнено",
                Description = "Исправлена ошибка при вводе пустого пароля"
            });
            AddTask(new TaskItem
            {
                Title = "Обновить зависимости",
                Priority = "Низкий",
                Status = "Выполнено",
                Description = "Обновить NuGet-пакеты до актуальных версий"
            });
        }

        private void AddTask(TaskItem task)
        {
            task.Id = _nextId++;
            _allTasks.Add(task);
        }

        private void ApplyFilter()
        {
            if (FilterStatusComboBox == null || SearchBox == null) return;

            string selectedStatus =
                (FilterStatusComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string searchText = SearchBox.Text?.Trim().ToLower() ?? "";

            _filteredTasks.Clear();

            foreach (var task in _allTasks)
            {
                bool statusMatch = selectedStatus == "Все" || task.Status == selectedStatus;
                bool searchMatch = string.IsNullOrEmpty(searchText)
                                   || task.Title.ToLower().Contains(searchText)
                                   || (task.Description?.ToLower().Contains(searchText) ?? false);

                if (statusMatch && searchMatch)
                    _filteredTasks.Add(task);
            }

            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            TotalCount.Text = $"Всего: {_allTasks.Count}";
            InProgressCount.Text = $"В работе: {_allTasks.Count(t => t.Status == "В работе")}";
            DoneCount.Text = $"Выполнено: {_allTasks.Count(t => t.Status == "Выполнено")}";
        }

        private void CreateTask_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TaskDialog { Owner = this };

            if (dialog.ShowDialog() == true)
            {
                AddTask(dialog.Task);
                ApplyFilter();
                StatusText.Text = $"Задача «{dialog.Task.Title}» добавлена.";
            }
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListBox.SelectedItem is TaskItem selected)
                OpenEditDialog(selected);
            else
                MessageBox.Show("Выберите задачу для редактирования.",
                                "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TasksListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TasksListBox.SelectedItem is TaskItem selected)
                OpenEditDialog(selected);
        }

        private void OpenEditDialog(TaskItem task)
        {
            var dialog = new TaskDialog(task) { Owner = this };

            if (dialog.ShowDialog() == true)
            {
                ApplyFilter();
                StatusText.Text = $"Задача «{task.Title}» обновлена.";
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TasksListBox.SelectedItem is TaskItem selected)
            {
                var result = MessageBox.Show(
                    $"Удалить задачу «{selected.Title}»?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _allTasks.Remove(selected);
                    ApplyFilter();
                    StatusText.Text = $"Задача «{selected.Title}» удалена.";
                }
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления.",
                                "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void FilterStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }
    }
}