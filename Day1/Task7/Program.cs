class Program
{
    static void Main()
    {
        Console.Write("A: "); int a = int.Parse(Console.ReadLine());
        Console.Write("B: "); int b = int.Parse(Console.ReadLine());
        Console.Write("X: "); int x = int.Parse(Console.ReadLine());
        Console.Write("Y: "); int y = int.Parse(Console.ReadLine());

        Console.Write("Цикл for: ");
        for (int i = a; i <= b; i++)
        {
            if (i % 10 == x || i % 10 == y) Console.Write(i + " ");
        }

        Console.Write("\nЦикл while: ");
        int j = a;
        while (j <= b)
        {
            if (j % 10 == x || j % 10 == y) Console.Write(j + " ");
            j++;
        }

        Console.Write("\nЦикл do-while: ");
        int k = a;
        if (k <= b)
        {
            do
            {
                if (k % 10 == x || k % 10 == y) Console.Write(k + " ");
                k++;
            } while (k <= b);
        }
    }
}