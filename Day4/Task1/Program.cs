class MathUtils
{
    public static int FindGCD(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        while (a != 0 && b != 0)
        {
            if (a > b) a %= b;
            else b %= a;
        }
        return a + b;
    }
}

class Program
{
    static void Main()
    {
        int num1 = 56;
        int num2 = 98;
        int result = MathUtils.FindGCD(num1, num2);

        Console.WriteLine($"Числа: {num1} и {num2}");
        Console.WriteLine($"Наибольший общий делитель: {result}");
    }
}