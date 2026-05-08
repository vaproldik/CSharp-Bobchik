using System.ComponentModel;
using System.Threading.Tasks;
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

        public AuthService AuthService => _authService;

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> LoginAsync(string password)
        {
            var (success, message) = await _authService.LoginAsync(Username, password);
            Message = message;
            return success;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string prop)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}