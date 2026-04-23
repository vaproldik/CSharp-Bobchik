class RecursiveMath
{
    public static int GCD(int a, int b)
    {
        if (b == 0) return a;
        return GCD(b, a % b);
    }
}

class Program
{
    static void Main()
    {
        int a = 48, b = 18;
        int result = RecursiveMath.GCD(a, b);

        Console.WriteLine($"НОД({a}, {b}) = {result}");
    }
}