class Calculator
{
    public static void Sum(in int a, in int b, out int result)
    {
        result = a + b;
    }

    public static void Sum(in double a, in double b, out double result)
    {
        result = a + b;
    }
}

class Program
{
    static void Main()
    {
        int i1 = 10, i2 = 20, iRes;
        Calculator.Sum(in i1, in i2, out iRes);
        Console.WriteLine($"Сумма int: {i1} + {i2} = {iRes}");

        double d1 = 2.5, d2 = 3.7, dRes;
        Calculator.Sum(in d1, in d2, out dRes);
        Console.WriteLine($"Сумма double: {d1} + {d2} = {dRes}");
    }
}