using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.ViewModels
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название расхода")]
        [StringLength(200, ErrorMessage = "Не более 200 символов")]
        [Display(Name = "Название")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите сумму")]
        [Range(0.01, 1000000, ErrorMessage = "Сумма должна быть от 0.01 до 1 000 000")]
        [Display(Name = "Сумма (₽)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Укажите дату")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Выберите категорию")]
        [Display(Name = "Категория")]
        public string Category { get; set; } = string.Empty; 

        [Display(Name = "Фильтр по категории")]
        public string? FilterCategory { get; set; } 

        [DataType(DataType.Date)]
        [Display(Name = "Дата от")]
        public DateTime? DateFrom { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата до")]
        public DateTime? DateTo { get; set; } 
    }
}