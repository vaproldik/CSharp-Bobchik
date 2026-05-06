using System.Windows;
using Task1.Services;
using Task1.ViewModels;

namespace Task1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(AuthService authService)
        {
            AuthService = authService;
        }

        public AuthService AuthService { get; }
    }
}