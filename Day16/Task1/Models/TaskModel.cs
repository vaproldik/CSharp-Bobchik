using System;
using System.ComponentModel;

namespace Task1.Models
{
    public class TaskModel : INotifyPropertyChanged
    {
        private string _title = string.Empty;
        private string _status = string.Empty;

        public int Id { get; set; }
        public int UserId { get; set; } 

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(StatusBrush));
            }
        }

        public string Category { get; set; } = string.Empty;
        public DateTime Deadline { get; set; } = DateTime.Now.AddDays(7);
        public string Description { get; set; } = string.Empty;

        public string DeadlineText => Deadline.ToString("dd.MM.yyyy");

        public string StatusBrush => Status switch
        {
            "В работе" => "#4299E1",
            "Выполнено" => "#48BB78",
            "Ожидание" => "#ED8936",
            _ => "#A0AEC0"
        };

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string prop)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}