using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.ViewModels
{
    public class ExpenseViewModel
    {
        [Required]
        [Range(0.01, 1000000, ErrorMessage = "Сумма должна быть от 0.01 до 1 000 000")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Категория обязательна")]
        public string? Category { get; set; }

        [Required(ErrorMessage = "Дата обязательна")]
        public DateTime Date { get; set; }
    }
}