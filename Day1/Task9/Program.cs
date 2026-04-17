class Program
{
    static void Main()
    {
        double a = 0;
        double b = Math.PI / 4;
        int m = 10;
        double h = (b - a) / m;

        Console.WriteLine("    x   |    f(x)   ");
        Console.WriteLine("------------------");
        for (int i = 0; i <= m; i++)
        {
            double x = a + i * h;
            double y = Math.Tan(x);
            Console.WriteLine($"{x:F5} | {y:F5}");
        }
    }
}