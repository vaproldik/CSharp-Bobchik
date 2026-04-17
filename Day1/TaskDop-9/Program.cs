class Program
{
    static void Main()
    {
        Console.WriteLine("Числа, равные утроенному произведению своих цифр:");
        for (int i = 10; i <= 99; i++)
        {
            int firstDigit = i / 10;
            int secondDigit = i % 10;
            int productTriple = 3 * firstDigit * secondDigit;

            if (i == productTriple)
            {
                Console.WriteLine(i);
            }
        }
    }
}