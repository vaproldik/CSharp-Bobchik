public abstract class Employee
{
    public string Name { get; set; }
    public abstract double CalculateSalary(); 

    public virtual void DisplayInfo() 
    {
        Console.WriteLine($"Имя: {Name} | Зарплата: {CalculateSalary()}");
    }
}

public class Manager : Employee
{
    public double BasePay { get; set; }
    public override double CalculateSalary() => BasePay;
}

public class Developer : Employee
{
    public double Rate { get; set; }
    public int Hours { get; set; }
    public override double CalculateSalary() => Rate * Hours;

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("-> Должность: Разработчик");
    }
}

class Program
{
    static void Main()
    {
        Employee[] staff = new Employee[]
        {
                new Manager { Name = "Олег", BasePay = 5000 },
                new Developer { Name = "Павел", Rate = 25, Hours = 160 }
        };

        foreach (var person in staff)
        {
            person.DisplayInfo();
            Console.WriteLine();
        }
    }
}