class Program
{
    static void Main()
    {
        double x = 4.3;

        double top = 1 + Math.Sqrt(Math.Abs(3 - x * x));
        double bottom = Math.Atan(x * x);
        double term1 = top / bottom;

        double term2 = Math.Exp(Math.Sin(Math.Sqrt(x)));

        double y = term1 - term2;

        Console.WriteLine("Результат y = " + y);
    }
}