using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Task1.Services;

namespace Task1
{
    public partial class ChatWindow : Window
    {
        private readonly ChatService _chatService;
        private readonly AuthService _authService;

        private readonly ObservableCollection<string> _messages = new();

        public ChatWindow(ChatService chatService, AuthService authService)
        {
            InitializeComponent();

            _chatService = chatService;
            _authService = authService;

            MessagesListBox.ItemsSource = _messages;

            _chatService.MessageReceived += msg =>
            {
                Dispatcher.Invoke(() =>
                {
                    _messages.Add(msg);
                    MessagesListBox.ScrollIntoView(msg);
                });
            };
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            await SendAsync();
        }

        private async void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                await SendAsync();
        }

        private async Task SendAsync()
        {
            string text = InputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(text)) return;

            string username = _authService.CurrentUser?.DisplayName ?? "Гость";
            string formatted = $"[Вы → {username}]: {text}";

            _messages.Add(formatted);
            MessagesListBox.ScrollIntoView(formatted);

            await _chatService.SendMessageAsync(username, text);

            InputBox.Clear();
        }
    }
}