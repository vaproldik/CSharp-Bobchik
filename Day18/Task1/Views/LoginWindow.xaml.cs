using System.Windows;
using Task1.Services;
using Task1.ViewModels;

namespace Task1.Views
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;
        private readonly AuthService _authService;

        public LoginWindow()
        {
            InitializeComponent();
            _authService = new AuthService();
            _viewModel = new LoginViewModel(_authService);
            DataContext = _viewModel;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string password = PwdBox.Password;
            bool success = await _viewModel.LoginAsync(password);
            MessageText.Text = _viewModel.Message;

            if (success)
            {
                var vm = new TaskManagerViewModel(_authService);
                var mainWindow = new MainWindow();
                mainWindow.DataContext = vm;
                mainWindow.Show();
                this.Close();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var regWindow = new RegisterWindow(_authService);
            regWindow.ShowDialog();
        }
    }
}