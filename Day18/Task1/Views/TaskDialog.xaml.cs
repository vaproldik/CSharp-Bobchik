using System;
using System.Windows;
using System.Windows.Controls;
using Task1.Models;

namespace Task1.Views
{
    public partial class TaskDialog : Window
    {
        public TaskItem? Result { get; private set; }

        private readonly TaskItem? _editTarget;

        // Создание новой задачи
        public TaskDialog()
        {
            InitializeComponent();
            DeadlinePicker.SelectedDate = DateTime.Now.AddDays(7);
        }

        // Редактирование существующей задачи
        public TaskDialog(TaskItem task) : this()
        {
            _editTarget = task;
            DialogTitle.Text = "Редактировать задачу";
            SaveButton.Content = "Обновить";

            TitleBox.Text = task.Title;
            DeadlinePicker.SelectedDate = task.Deadline;

            SetComboBox(CategoryBox, task.Category);
            SetComboBox(StatusBox, task.Status);
        }

        private void SetComboBox(ComboBox box, string value)
        {
            foreach (ComboBoxItem item in box.Items)
            {
                if (item.Content?.ToString() == value)
                {
                    box.SelectedItem = item;
                    return;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                MessageBox.Show("Введите название задачи.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                TitleBox.Focus();
                return;
            }

            string category = (CategoryBox.SelectedItem as ComboBoxItem)?
                              .Content?.ToString() ?? "Общее";
            string status = (StatusBox.SelectedItem as ComboBoxItem)?
                              .Content?.ToString() ?? "Ожидание";
            DateTime deadline = DeadlinePicker.SelectedDate
                                ?? DateTime.Now.AddDays(7);

            if (_editTarget != null)
            {
                // Редактирование — обновляем существующий объект
                _editTarget.Title = TitleBox.Text.Trim();
                _editTarget.Category = category;
                _editTarget.Status = status;
                _editTarget.Deadline = deadline;
            }
            else
            {
                // Создание — возвращаем новый объект
                Result = new TaskItem
                {
                    Title = TitleBox.Text.Trim(),
                    Category = category,
                    Status = status,
                    Deadline = deadline
                };
            }

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
            => DialogResult = false;
    }
}