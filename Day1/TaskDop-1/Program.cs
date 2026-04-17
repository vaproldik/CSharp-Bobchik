class Program
{
    static void Main()
    {
        Console.WriteLine("Трехзначные автоморфные числа:");
        for (int i = 100; i <= 999; i++)
        {
            long square = (long)i * i;
            if (square % 1000 == i)
            {
                Console.WriteLine(i);
            }
        }
    }
}