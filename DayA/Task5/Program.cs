class Program
{
    static void Main()
    {
        Console.Write("Введите трехзначное число: ");
        int number = int.Parse(Console.ReadLine());

        int firstDigit = number / 100;

        int remainingPart = number % 100;

        int resultNumber = remainingPart * 10 + firstDigit;

        Console.WriteLine($"Полученное число: {resultNumber}");
    }
}
