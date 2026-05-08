using System.ComponentModel;
using System.Threading.Tasks;
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

        public RegisterViewModel(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> RegisterAsync(string password)
        {
            var (success, message) = await _authService.RegisterAsync(
                Username, password, DisplayName);
            Message = message;
            return success;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string prop)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}