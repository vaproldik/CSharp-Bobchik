public class EmployeeFileReader
{
    private string filePath = "file.data";

    public List<Employee> ReadEmployees()
    {
        List<Employee> employees = new List<Employee>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не найден!");
            return employees;
        }

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 3)
            {
                string name = parts[0];
                string dept = parts[1];
                decimal salary = decimal.Parse(parts[2]);

                employees.Add(new Employee(name, dept, salary));
            }
        }
        return employees;
    }
}
