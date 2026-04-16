class Program
{
    static void Main()
    {
        Console.WriteLine("Введите координаты вершин треугольника:");
        Console.Write("x1 = "); double x1 = double.Parse(Console.ReadLine());
        Console.Write("y1 = "); double y1 = double.Parse(Console.ReadLine());
        Console.Write("x2 = "); double x2 = double.Parse(Console.ReadLine());
        Console.Write("y2 = "); double y2 = double.Parse(Console.ReadLine());
        Console.Write("x3 = "); double x3 = double.Parse(Console.ReadLine());
        Console.Write("y3 = "); double y3 = double.Parse(Console.ReadLine());

        double sideA = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        double sideB = Math.Sqrt(Math.Pow(x3 - x2, 2) + Math.Pow(y3 - y2, 2));
        double sideC = Math.Sqrt(Math.Pow(x1 - x3, 2) + Math.Pow(y1 - y3, 2));

        double perimeterP = sideA + sideB + sideC;
        double semiPerimeter = perimeterP / 2;

        double areaS = 0.5 * Math.Abs((x1 - x3) * (y2 - y3) - (x2 - x3) * (y1 - y3));

        Console.WriteLine($"\nПериметр треугольника: {perimeterP:F2}");
        Console.WriteLine($"Полупериметр: {semiPerimeter:F2}");
        Console.WriteLine($"Площадь треугольника: {areaS:F2}");
    }
}
