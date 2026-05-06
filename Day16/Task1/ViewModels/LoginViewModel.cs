using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Task1.Commands;
using Task1.Services;

namespace Task1.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;

        private string _username = string.Empty;
        private string _message = string.Empty;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(nameof(Message)); }
        }

        public ICommand LoginCommand { get; }
        public ICommand OpenRegCommand { get; }

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;

            LoginCommand = new RelayCommand(p =>
            {
                // Пароль передаём через CommandParameter из PasswordBox
                string password = p?.ToString() ?? string.Empty;
                var (success, msg) = _authService.Login(Username, password);
                Message = msg;

                if (success)
                {
                    var mainWindow = new MainWindow(_authService);
                    mainWindow.Show();

                    // Закрываем окно входа
                    foreach (Window w in Application.Current.Windows)
                        if (w is LoginWindow) { w.Close(); break; }
                }
            });

            OpenRegCommand = new RelayCommand(_ =>
            {
                var regWindow = new RegisterWindow(_authService);
                regWindow.ShowDialog();
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string prop)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}