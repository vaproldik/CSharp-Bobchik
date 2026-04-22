using PracticeWork;

public class Company
{
    public Employee[] Employees { get; set; }

    public Employee[] GetHighestPaidEmployees()
    {
        if (Employees == null || Employees.Length == 0) return new Employee[0];

        decimal maxSalary = 0;
        foreach (var e in Employees)
        {
            if (e.Salary > maxSalary) maxSalary = e.Salary;
        }

        int count = 0;
        foreach (var e in Employees) if (e.Salary == maxSalary) count++;

        Employee[] winners = new Employee[count];
        int index = 0;
        foreach (var e in Employees)
        {
            if (e.Salary == maxSalary) winners[index++] = e;
        }
        return winners;
    }

    public Employee[] GetEmployeesByPosition(string position)
    {
        int count = 0;
        foreach (var e in Employees)
        {
            if (e.Position.Equals(position, StringComparison.OrdinalIgnoreCase)) count++;
        }

        Employee[] result = new Employee[count];
        int index = 0;
        foreach (var e in Employees)
        {
            if (e.Position.Equals(position, StringComparison.OrdinalIgnoreCase))
            {
                result[index++] = e;
            }
        }
        return result;
    }
}