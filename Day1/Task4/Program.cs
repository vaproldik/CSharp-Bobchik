class Program
{
    static void Main()
    {
        Console.Write("Введите значение x: ");
        double x = double.Parse(Console.ReadLine());
        double y;

        if (x >= 1 && x <= 3)
        {
            y = 2 * Math.Pow(x, 2) - 3 * Math.Exp(Math.Sin(x));
        }
        else if (x > 3)
        {
            y = 3 * Math.Tan(x);
        }
        else
        {
            Console.WriteLine("Значение x не входит в заданные диапазоны.");
            return;
        }

        Console.WriteLine($"При x = {x}, значение y = {y:F4}");
    }
}