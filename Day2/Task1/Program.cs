class Program
{
    static void Main()
    {
        double[] numbers = { 10.5, 20.3, 15.0, 7.2, 12.0 };
        double sum = 0;

        for (int i = 0; i < numbers.Length; i++)
        {
            sum += numbers[i];
        }

        double average = sum / numbers.Length;

        Console.WriteLine("Среднее арифметическое: " + average);
    }
}