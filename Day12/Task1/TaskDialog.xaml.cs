using System.Windows;
using System.Windows.Controls;

namespace Task1
{
    public partial class TaskDialog : Window
    {
        public TaskItem Task { get; private set; }

        private readonly TaskItem _editTarget;

        public TaskDialog()
        {
            InitializeComponent();
            Task = new TaskItem();
        }

        public TaskDialog(TaskItem task) : this()
        {
            _editTarget = task;
            DialogTitle.Text = "Редактировать задачу";
            SaveButton.Content = "Обновить";

            TitleBox.Text = task.Title;
            DescriptionBox.Text = task.Description;

            SelectComboBoxItem(PriorityComboBox, task.Priority);
            SelectComboBoxItem(StatusComboBox, task.Status);
        }

        private void SelectComboBoxItem(ComboBox comboBox, string value)
        {
            foreach (ComboBoxItem item in comboBox.Items)
            {
                if (item.Content?.ToString() == value)
                {
                    comboBox.SelectedItem = item;
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

            string priority =
                (PriorityComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string status =
                (StatusComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            if (_editTarget != null)
            {
                _editTarget.Title = TitleBox.Text.Trim();
                _editTarget.Priority = priority;
                _editTarget.Status = status;
                _editTarget.Description = DescriptionBox.Text.Trim();
            }
            else
            {
                Task.Title = TitleBox.Text.Trim();
                Task.Priority = priority;
                Task.Status = status;
                Task.Description = DescriptionBox.Text.Trim();
            }

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}