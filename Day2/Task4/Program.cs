class Program
{
    static void Main()
    {
        double[,] profits = new double[10, 12]; 
        Random rnd = new Random();

        int septemberIndex = 8;
        double totalSeptemberProfit = 0;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                profits[i, j] = rnd.Next(1000, 5000);
            }
            totalSeptemberProfit += profits[i, septemberIndex];
        }

        Console.Write("Введите контрольное число для проверки дохода: ");
        double limit = double.Parse(Console.ReadLine());

        bool exceeded = totalSeptemberProfit > limit;
        Console.WriteLine($"Общий доход за сентябрь: {totalSeptemberProfit}");
        Console.WriteLine($"Доход превысил лимит? {exceeded}");
    }
}