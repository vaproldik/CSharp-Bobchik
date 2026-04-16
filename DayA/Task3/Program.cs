class Program
{
    static void Main()
    {
        Console.Write("Введите значение угла альфа (в градусах): ");
        double alpha = double.Parse(Console.ReadLine());

        Console.Write("Введите значение угла бета (в градусах): ");
        double beta = double.Parse(Console.ReadLine());

        double a = alpha * Math.PI / 180;
        double b = beta * Math.PI / 180;

        double z1 = (Math.Sin(a) + Math.Cos(2 * b - a)) / (Math.Cos(a) - Math.Sin(2 * b - a));

        double z2 = (1 + Math.Sin(2 * b)) / Math.Cos(2 * b);
        Console.WriteLine($"Результат z1: {z1:F4}");
        Console.WriteLine($"Результат z2: {z2:F4}");
    }
}