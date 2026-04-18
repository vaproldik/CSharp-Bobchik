class Program
{
    static void Main()
    {
        Console.Write("Введите размер массива n: ");
        int n = int.Parse(Console.ReadLine());
        double[] a = new double[n];
        Random rnd = new Random();

        for (int i = 0; i < n; i++)
        {
            a[i] = Math.Round(rnd.NextDouble() * 20 - 10, 2);
        }

        double sum = 0;
        foreach (double x in a) sum += x;
        double avg = sum / n;

        for (int i = 0; i < n; i++)
        {
            if (a[i] < 0)
                a[i] += 0.5;
            else if (a[i] >= 0 && a[i] < avg)
                a[i] = 0.1;
        }

        Array.Sort(a);
        Console.WriteLine("Массив после преобразования и сортировки: " + string.Join(" | ", a));

        Console.Write("Введите число k для поиска: ");
        double k = double.Parse(Console.ReadLine());
        int index = Array.BinarySearch(a, k);

        if (index >= 0)
            Console.WriteLine($"Число {k} найдено на позиции {index}");
        else
            Console.WriteLine("Число не найдено");
    }
}