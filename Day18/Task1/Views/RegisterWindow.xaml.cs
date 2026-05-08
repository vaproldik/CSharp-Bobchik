using System.Windows;
using System.Windows.Media;
using Task1.Services;
using Task1.ViewModels;

namespace Task1.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly RegisterViewModel _viewModel;

        public RegisterWindow(AuthService authService)
        {
            InitializeComponent();
            _viewModel = new RegisterViewModel(authService);
            DataContext = _viewModel;
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Username = UsernameBox.Text.Trim();
            _viewModel.DisplayName = DisplayNameBox.Text.Trim();
            string password = PwdBox.Password;

            bool success = await _viewModel.RegisterAsync(password);

            MessageText.Text = _viewModel.Message;
            MessageText.Foreground = success
                ? Brushes.Green
                : Brushes.Red;
        }
    }
}