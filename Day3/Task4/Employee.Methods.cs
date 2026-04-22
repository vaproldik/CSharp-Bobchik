namespace PracticeWork
{
    public partial class Employee
    {
        public string GetInfo()
        {
            return $"{Name} | Должность: {Position} | Зарплата: {Salary:C}";
        }
    }
}