using System.Windows;
using System.Windows.Controls;
using Task1.Models;

namespace Task1.Views
{
    public partial class TaskDialog : Window
    {
        public TaskItem? Result { get; private set; }

        private readonly TaskItem? _editTarget;

        public TaskDialog()
        {
            InitializeComponent();
        }

        public TaskDialog(TaskItem task) : this()
        {
            _editTarget = task;
            DialogTitle.Text = "Редактировать задачу";
            SaveButton.Content = "Обновить";

            TitleBox.Text = task.Title;
            DescriptionBox.Text = task.Description;
            DeadlinePicker.SelectedDate = task.Deadline;

            SetComboBox(PriorityComboBox, task.Priority);
            SetComboBox(StatusComboBox, task.Status);
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                MessageBox.Show("Введите название задачи.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                TitleBox.Focus();
                return;
            }

            string priority = (PriorityComboBox.SelectedItem as ComboBoxItem)?
                              .Content?.ToString() ?? "Средний";
            string status = (StatusComboBox.SelectedItem as ComboBoxItem)?
                              .Content?.ToString() ?? "В работе";

            if (_editTarget != null)
            {
                _editTarget.Title = TitleBox.Text.Trim();
                _editTarget.Priority = priority;
                _editTarget.Status = status;
                _editTarget.Description = DescriptionBox.Text.Trim();
                _editTarget.Deadline = DeadlinePicker.SelectedDate;
            }
            else
            {
                Result = new TaskItem
                {
                    Title = TitleBox.Text.Trim(),
                    Priority = priority,
                    Status = status,
                    Description = DescriptionBox.Text.Trim(),
                    Deadline = DeadlinePicker.SelectedDate
                };
            }

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
            => DialogResult = false;
    }
}