using PracticeWork;

class Program
{
    static void Main()
    {
        Company myOffice = new Company();
        myOffice.Employees = new Employee[]
        {
                new Employee { Name = "Артем", Position = "Разработчик", Salary = 2500 },
                new Employee { Name = "Елена", Position = "Менеджер", Salary = 1800 },
                new Employee { Name = "Дмитрий", Position = "Разработчик", Salary = 2500 },
                new Employee { Name = "Анна", Position = "Дизайнер", Salary = 1500 }
        };

        Console.WriteLine("Сотрудники с максимальной зарплатой:");
        Employee[] richStaff = myOffice.GetHighestPaidEmployees();
        foreach (var e in richStaff)
        {
            Console.WriteLine($"- {e.GetInfo()}");
        }

        string searchPos = "Разработчик";
        Console.WriteLine($"\nПоиск по должности '{searchPos}':");
        Employee[] devs = myOffice.GetEmployeesByPosition(searchPos);
        foreach (var e in devs)
        {
            Console.WriteLine($"- {e.Name} (Зарплата: {e.Salary})");
        }
    }
}