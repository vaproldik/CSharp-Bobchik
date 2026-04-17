class Program
{
    static void Main()
    {
        Console.Write("Введите цену за 1 кг конфет: ");
        int pricePerKg = int.Parse(Console.ReadLine());

        for (int i = 1; i <= 10; i++)
        {
            int totalCost = i * pricePerKg;
            Console.WriteLine($"Стоимость {i} кг: {totalCost}");
        }
    }
}