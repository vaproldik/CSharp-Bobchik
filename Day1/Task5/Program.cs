class Program
{
    static void Main()
    {
        Console.Write("Введите число M: ");
        int m = int.Parse(Console.ReadLine());

        Console.Write("Введите число N: ");
        int n = int.Parse(Console.ReadLine());

        if (n != 0 && m % n == 0)
        {
            Console.WriteLine($"Частное: {m / n}");
        }
        else
        {
            Console.WriteLine("M на N нацело не делится");
        }
    }
}