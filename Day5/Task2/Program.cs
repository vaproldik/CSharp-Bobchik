class Program
{
    static void Main(string[] args)
    {
        Employee[] staff1 = new Employee[]
        {
                new Employee { FullName = "Иванов И.И.", Salary = 2500 },
                new Employee { FullName = "Петров П.П.", Salary = 3100 }
        };

        Employee[] staff2 = new Employee[]
        {
                new Employee { FullName = "Сидоров С.С.", Salary = 1800 }
        };

        Client vipClient = new Client { Name = "ОАО 'Беларуськалий'" };

        Company[] companies = new Company[2];
        companies[0] = new Company("ТехноМир", "Разработка ПО", staff1);
        companies[1] = new Company("СтройИнвест", "Проектирование", staff2);

        companies[0].CurrentClient = vipClient;

        Console.WriteLine("Запуск бизнес-логики компаний...");
        foreach (Company comp in companies)
        {
            comp.CalculateTotalSalary();
            Console.WriteLine();
        }
    }
}