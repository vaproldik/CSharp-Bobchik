using System.Windows;
using System.Windows.Media;
using Task1.Services;

namespace Task1
{
    public partial class RegisterWindow : Window
    {
        private readonly AuthService _authService;



        public RegisterWindow(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string displayName = DisplayNameBox.Text.Trim();
            string password = PwdBox.Password;

            // Простая проверка на пустые поля
            if (string.IsNullOrWhiteSpace(username))
            {
                ShowMessage("Введите имя пользователя!", false);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowMessage("Введите пароль!", false);
                return;
            }

            var (success, message) = _authService.Register(
                username, password, displayName);

            ShowMessage(message, success);
        }

        private void ShowMessage(string text, bool isSuccess)
        {
            MessageText.Text = text;
            MessageText.Foreground = isSuccess
                ? Brushes.Green
                : Brushes.Red;
        }
    }
}