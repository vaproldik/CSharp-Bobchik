using System.Windows;
using System.Windows.Controls;
using Task1.ViewModels;

namespace Task1
{
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Двойной клик — редактировать
        private void TasksListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ViewModel?.SelectedTask != null)
                ViewModel.EditTaskCommand.Execute(null);
        }

        // Фильтр из ToolBar
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel == null) return;
            string value = (FilterComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            ViewModel.FilterStatus = value ?? "Все";
        }

        // Пункты меню — быстрые фильтры
        private void ShowAll_Click(object sender, RoutedEventArgs e)
            => ViewModel.FilterStatus = "Все";

        private void ShowInProgress_Click(object sender, RoutedEventArgs e)
            => ViewModel.FilterStatus = "В работе";

        private void ShowDone_Click(object sender, RoutedEventArgs e)
            => ViewModel.FilterStatus = "Выполнено";

        // Темы (заглушки для демонстрации)
        private void ThemeLight_Click(object sender, RoutedEventArgs e)
            => Background = System.Windows.Media.Brushes.WhiteSmoke;

        private void ThemeDark_Click(object sender, RoutedEventArgs e)
            => Background = System.Windows.Media.Brushes.DimGray;
    }
}