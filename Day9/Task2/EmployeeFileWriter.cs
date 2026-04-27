public class EmployeeFileWriter
{
    private string filePath = "file.data";

    public void WriteEmployees(List<Employee> employees, char separator)
    {
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            foreach (var emp in employees)
            {
                string line = $"{emp.Name}{separator}{emp.Department}{separator}{emp.Salary}";
                writer.WriteLine(line);
            }
        }
        Console.WriteLine("Сотрудники успешно записаны в file.data");
    }
}
