class Program
{
    static void Main()
    {
        Console.Write("Введите количество чисел n: ");
        int n = int.Parse(Console.ReadLine());

        double maxAbs = 0;

        for (int i = 1; i <= n; i++)
        {
            Console.Write($"Введите число a{i}: ");
            double current = double.Parse(Console.ReadLine());

            if (Math.Abs(current) > maxAbs)
            {
                maxAbs = Math.Abs(current);
            }
        }

        Console.WriteLine($"Максимальный модуль: {maxAbs}");
    }
}