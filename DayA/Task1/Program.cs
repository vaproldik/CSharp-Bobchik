class Program
{
    static void Main()
    {
        Console.WriteLine("Вычисление стоимости покупки.");

        Console.Write("Цена тетради (руб.) —> ");
        double notebookPrice = double.Parse(Console.ReadLine());

        Console.Write("Количество тетрадей —> ");
        int notebookCount = int.Parse(Console.ReadLine());

        Console.Write("Цена карандаша (руб.) —> ");
        double pencilPrice = double.Parse(Console.ReadLine());

        Console.Write("Количество карандашей —> ");
        int pencilCount = int.Parse(Console.ReadLine());

        double totalPrice = (notebookPrice * notebookCount) + (pencilPrice * pencilCount);

        Console.WriteLine($"Стоимость покупки: {totalPrice:F2} руб.");
    }
}