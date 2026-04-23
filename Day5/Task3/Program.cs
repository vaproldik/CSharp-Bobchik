class Program
{
    static void Main(string[] args)
    {
        Employee[] staff = new Employee[]
        {
                new Director { Name = "Иван Степанович" },
                new Engineer { Name = "Алексей" },
                new Director { Name = "Мария Ивановна" },
                new Engineer { Name = "Дмитрий" }
        };

        Console.WriteLine("--- Полный список персонала ---");
        foreach (var person in staff)
        {
            Console.WriteLine($"Сотрудник: {person.Name}");
        }

        Console.WriteLine("\n--- Результат фильтрации (только управленцы) ---");

        foreach (Employee person in staff)
        
            if (person is IManager manager)
            {
                manager.Manage(); 
            }
        }
    }