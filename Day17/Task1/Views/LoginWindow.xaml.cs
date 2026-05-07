using System.Windows;
using Task1.Services;
using Task1.ViewModels;

namespace Task1
{
    public partial class LoginWindow : Window
    {
        private readonly AuthService _authService;

        public LoginWindow()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PwdBox.Password;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageText.Text = "Заполните все поля!";
                return;
            }

            var (success, message) = _authService.Login(username, password);
            MessageText.Text = message;

            if (success)
            {
                var mainWindow = new MainWindow();
                mainWindow.DataContext = new TaskManagerViewModel(_authService);
                mainWindow.Show();
                this.Close();
            }
        }

        private void OpenRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var regWindow = new RegisterWindow(_authService);
            regWindow.ShowDialog();
        }
    }
}