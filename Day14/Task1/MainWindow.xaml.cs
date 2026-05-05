using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Task1.ViewModels;

namespace Task1
{
    public partial class MainWindow : Window
    {
        private MainViewModel? ViewModel => DataContext as MainViewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TasksListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel?.SelectedTask != null)
                ViewModel.EditTaskCommand.Execute(null);
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel == null) return;
            string value = (FilterComboBox.SelectedItem as ComboBoxItem)?
                           .Content?.ToString() ?? "Все";
            ViewModel.FilterStatus = value;
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null) ViewModel.FilterStatus = "Все";
        }

        private void ShowInProgress_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null) ViewModel.FilterStatus = "В работе";
        }

        private void ShowDone_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null) ViewModel.FilterStatus = "Выполнено";
        }

        private void ShowPostponed_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null) ViewModel.FilterStatus = "Отложено";
        }
    }
}