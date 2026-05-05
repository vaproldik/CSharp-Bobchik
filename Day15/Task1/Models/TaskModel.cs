using System;
using System.ComponentModel;

namespace Task1.Models
{
    public class TaskModel : INotifyPropertyChanged
    {
        private string _title = string.Empty;
        private string _status = string.Empty;
        private DateTime _deadline;
        private TaskCategoryModel? _category;

        public int Id { get; set; }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
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

        public DateTime Deadline
        {
            get => _deadline;
            set
            {
                _deadline = value;
                OnPropertyChanged(nameof(Deadline));
                OnPropertyChanged(nameof(DeadlineText));
            }
        }

        public TaskCategoryModel? Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        public string CategoryName => Category?.Name ?? "Без категории";

        public string DeadlineText => Deadline.ToString("dd.MM.yyyy");

        public string StatusBrush
        {
            get
            {
                return Status switch
                {
                    "В работе" => "#4299E1",
                    "Выполнено" => "#48BB78",
                    "Ожидание" => "#ED8936",
                    _ => "#A0AEC0"
                };
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}