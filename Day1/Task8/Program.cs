class Program
{
    static void Main()
    {
        Console.Write("Введите число A: ");
        double a = double.Parse(Console.ReadLine());
        Console.Write("Введите N: ");
        int n = int.Parse(Console.ReadLine());

        double result = 1;
        for (int i = 1; i <= n; i++)
        {
            result *= a;
            Console.WriteLine($"{a} в степени {i} = {result:F4}");
        }
    }
}