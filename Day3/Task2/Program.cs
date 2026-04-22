public class Employee
{
    public string Name { get; set; }
    public double Salary { get; set; }
}

public static class ArrayUtils
{
    public static double CalculateAverageSalary(Employee[] employees)
    {
        if (employees == null || employees.Length == 0) return 0;

        double sum = 0;
        foreach (var emp in employees)
        {
            sum += emp.Salary;
        }
        return sum / employees.Length;
    }
}

class Program
{
    static void Main()
    {
        Employee[] employees = {
                new Employee { Name = "Pavel", Salary = 1200.50 },
                new Employee { Name = "Dmitry", Salary = 1500.00 },
                new Employee { Name = "Elena", Salary = 1350.75 },
                new Employee { Name = "Alex", Salary = 900.25 }
            };

        Console.WriteLine("Список сотрудников:");
        foreach (var emp in employees)
        {
            Console.WriteLine($"- {emp.Name}: {emp.Salary}");
        }

        double average = ArrayUtils.CalculateAverageSalary(employees);

        Console.WriteLine("\nРасчет окончен.");
        Console.WriteLine($"Средняя зарплата всех сотрудников: {average:F2}");
    }
}