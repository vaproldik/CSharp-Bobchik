public class Company
{
    public string CompanyName;
    private Department _department;
    private Employee[] _employees;
    public Client CurrentClient { get; set; }

    public Company(string name, string deptName, Employee[] employees)
    {
        CompanyName = name;
        _department = new Department(deptName);
        _employees = employees;
    }

    public void CalculateTotalSalary()
    {
        double total = 0;
        foreach (Employee emp in _employees)
        {
            total += emp.Salary;
        }
        Console.WriteLine($"--- Отчет по компании {CompanyName} ---");
        Console.WriteLine($"Отдел: {_department.DeptName}");
        Console.WriteLine($"Общая сумма зарплат: {total} BYN");

        if (CurrentClient != null)
        {
            Console.WriteLine($"Работаем с клиентом: {CurrentClient.Name}");
        }
    }
}