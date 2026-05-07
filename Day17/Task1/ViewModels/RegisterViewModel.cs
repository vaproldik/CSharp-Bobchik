using System.ComponentModel;
using System.Windows.Input;
using Task1.Commands;
using Task1.Services;

namespace Task1.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;

        private string _username = string.Empty;
        private string _displayName = string.Empty;
        private string _message = string.Empty;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public string DisplayName
        {
            get => _displayName;
            set { _displayName = value; OnPropertyChanged(nameof(DisplayName)); }
        }

        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(nameof(Message)); }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel(AuthService authService)
        {
            _authService = authService;

            RegisterCommand = new RelayCommand(p =>
            {
                string password = p?.ToString() ?? string.Empty;
                var (success, msg) = _authService.Register(
                    Username, password, DisplayName);
                Message = msg;
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string prop)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}