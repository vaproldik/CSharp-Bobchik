class A
{
    public int a;
    public int b;

    public A(int firstValue, int secondValue)
    {
        a = firstValue;
        b = secondValue;
    }

    public double CalculateDivision()
    {
        if (b == 0)
        {
            Console.WriteLine("Ошибка: деление на ноль!");
            return 0;
        }
        return (double)a / b;
    }

    public double CalculateExpression()
    {
        return Math.Pow(a, 2) + b;
    }
}

class Program
{
    static void Main()
    {
        A myObj = new A(10, 2);
        Console.WriteLine($"Поле a: {myObj.a}, Поле b: {myObj.b}");
        Console.WriteLine($"Частное: {myObj.CalculateDivision()}");
        Console.WriteLine($"Результат выражения (a^2 + b): {myObj.CalculateExpression()}");
    }
}