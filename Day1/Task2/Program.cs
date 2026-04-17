class Program
{
    static void Main()
    {
        Console.Write("Введите целое число: ");
        int number = int.Parse(Console.ReadLine());

        bool isEvenTwoDigit = (number >= 10 && number <= 99) && (number % 2 == 0);

        Console.WriteLine($"Результат: {isEvenTwoDigit}");
    }
}