class Program
{
    static void Main()
    {
        Console.Write("Введите число: ");
        int number = int.Parse(Console.ReadLine());
        bool isOrdered = true;

        int lastDigit = -1; 

        while (number > 0)
        {
            int currentDigit = number % 10;
            if (currentDigit < lastDigit)
            {
                isOrdered = false;
                break;
            }
            lastDigit = currentDigit;
            number /= 10;
        }

        Console.WriteLine($"Упорядочены по возрастанию справа налево: {isOrdered}");
    }
}