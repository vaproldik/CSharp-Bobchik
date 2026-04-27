class Program
{
    static void Main()
    {
        EmployeeFileReader reader = new EmployeeFileReader();
        List<Employee> list = reader.ReadEmployees();

        EmployeeProcessor processor = new EmployeeProcessor(list);
        processor.FindByDepartment("IT");
    }
}