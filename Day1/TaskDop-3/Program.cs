class Program
{
    static void Main()
    {
        Console.Write("Введите число n: ");
        int n = int.Parse(Console.ReadLine());
        Console.Write("Введите цифру k: ");
        int k = int.Parse(Console.ReadLine());

        int count = 0;
        int temp = Math.Abs(n); 

        while (temp > 0)
        {
            if (temp % 10 == k) count++;
            temp /= 10;
        }

        Console.WriteLine($"Цифра {k} встречается {count} раз.");
    }
}