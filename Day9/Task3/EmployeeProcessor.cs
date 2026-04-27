public class EmployeeProcessor
{
    private List<Employee> _employees;

    public EmployeeProcessor(List<Employee> employees)
    {
        _employees = employees;
    }

    public void FindByDepartment(string department)
    {
        Console.WriteLine($"--- Поиск сотрудников отдела: {department} ---");
        bool found = false;
        foreach (var emp in _employees)
        {
            if (emp.Department.Equals(department, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Имя: {emp.Name}, Зарплата: {emp.Salary}");
                found = true;
            }
        }
        if (!found) Console.WriteLine("Сотрудники не найдены.");
    }
}
