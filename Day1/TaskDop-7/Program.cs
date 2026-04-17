class Program
{
    static void Main()
    {
        Console.Write("Введите число n: ");
        int n = int.Parse(Console.ReadLine());
        int count = 0;

        for (int i = 1; i <= Math.Abs(n); i++)
        {
            if (n % i == 0) count++;
        }

        Console.WriteLine($"Количество делителей: {count}");
    }
}