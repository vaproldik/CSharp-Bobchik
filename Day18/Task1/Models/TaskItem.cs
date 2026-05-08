using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Task1.Models
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string _title = string.Empty;
        private string _status = string.Empty;
        private string _category = string.Empty;
        private string _description = string.Empty;

        [Key]
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
                OnPropertyChanged(nameof(IsActive));
            }
        }

        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(nameof(Category)); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        public DateTime Deadline { get; set; } = DateTime.Now.AddDays(7);

        // Вычисляемые свойства (не хранятся в БД)
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string DeadlineText => Deadline.ToString("dd.MM.yyyy");

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsActive => Status == "В работе";

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
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