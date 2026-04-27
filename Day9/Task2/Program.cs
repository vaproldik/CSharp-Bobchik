class Program
{
    static void Main()
    {
        List<Employee> staff = new List<Employee>
        {
            new Employee("Павел", "IT", 2500),
            new Employee("Анна", "Бухгалтерия", 1800),
            new Employee("Дмитрий", "IT", 2200)
        };

        EmployeeFileWriter writer = new EmployeeFileWriter();
        writer.WriteEmployees(staff, '|');
    }
}