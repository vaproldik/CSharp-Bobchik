class GeometryProvider
{
    public static void RectPS(double x1, double y1, double x2, double y2, out double P, out double S)
    {
        double width = Math.Abs(x2 - x1);
        double height = Math.Abs(y2 - y1);
        P = 2 * (width + height);
        S = width * height;
    }
}

class Program
{
    static void Main()
    {
        double x1 = 1, y1 = 5, x2 = 4, y2 = 1;
        double perimeter, area;

        GeometryProvider.RectPS(x1, y1, x2, y2, out perimeter, out area);

        Console.WriteLine($"Вершины: ({x1},{y1}) и ({x2},{y2})");
        Console.WriteLine($"Периметр: {perimeter}");
        Console.WriteLine($"Площадь: {area}");
    }
}