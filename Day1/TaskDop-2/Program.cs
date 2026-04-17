class Program
{
    static void Main()
    {
        Console.WriteLine("Числа, сумма квадратов цифр которых кратна 13:");
        for (int i = 10; i <= 99; i++)
        {
            int firstDigit = i / 10;
            int secondDigit = i % 10;
            int sumSquares = (firstDigit * firstDigit) + (secondDigit * secondDigit);

            if (sumSquares % 13 == 0)
            {
                Console.WriteLine(i);
            }
        }
    }
}