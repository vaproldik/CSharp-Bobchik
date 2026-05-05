using System;
using System.ComponentModel;

namespace Task1.Models
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string _title = string.Empty;
        private string _priority = string.Empty;
        private string _status = string.Empty;
        private string _description = string.Empty;
        private DateTime? _deadline;

        public int Id { get; set; }

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public string Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged(nameof(Priority));
                OnPropertyChanged(nameof(PriorityColor));
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(StatusColor));
                OnPropertyChanged(nameof(StatusTextColor));
                OnPropertyChanged(nameof(IsCompleted));
                OnPropertyChanged(nameof(NextStatus));
            }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        public DateTime? Deadline
        {
            get => _deadline;
            set
            {
                _deadline = value;
                OnPropertyChanged(nameof(Deadline));
                OnPropertyChanged(nameof(DeadlineText));
            }
        }

        public string DeadlineText => _deadline.HasValue
            ? _deadline.Value.ToString("dd.MM.yyyy")
            : "Не задан";

        public string StatusColor => Status switch
        {
            "В работе" => "#BEE3F8",
            "Выполнено" => "#C6F6D5",
            "Отложено" => "#E9D8FD",
            _ => "#EDF2F7"
        };

        public string StatusTextColor => Status switch
        {
            "В работе" => "#2B6CB0",
            "Выполнено" => "#276749",
            "Отложено" => "#553C9A",
            _ => "#4A5568"
        };

        public string PriorityColor => Priority switch
        {
            "Высокий" => "#FED7D7",
            "Средний" => "#FEFCBF",
            "Низкий" => "#C6F6D5",
            _ => "#EDF2F7"
        };

        public bool IsCompleted => Status == "Выполнено";

        public string NextStatus => Status switch
        {
            "В работе" => "Выполнено",
            "Выполнено" => "Отложено",
            "Отложено" => "В работе",
            _ => "В работе"
        };

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string prop)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}