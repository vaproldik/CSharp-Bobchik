class Program
{
    static void Main()
    {
        Console.Write("Введите размер матрицы N: ");
        int n = int.Parse(Console.ReadLine());
        int[,] matrix = new int[n, n];
        Random rnd = new Random();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = rnd.Next(1, 10);
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }

        Console.Write("Введите число C: ");
        int c = int.Parse(Console.ReadLine());

        double sumSquares = 0;
        for (int i = 0; i < n; i++)
        {
            double rowSum = 0;
            for (int j = 0; j < n; j++)
            {
                if (matrix[i, j] > c)
                    sumSquares += Math.Pow(matrix[i, j], 2);

                rowSum += matrix[i, j];
            }
            Console.WriteLine($"Среднее строки {i}: {rowSum / n}");
        }
        Console.WriteLine("Сумма квадратов элементов > C: " + sumSquares);
    }
}