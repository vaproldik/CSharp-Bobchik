class Program
{
    static void Main()
    {
        string input = "строка для примера, это задание 6 - работа с символами.";
        string result = "";

        for (int i = 0; i < input.Length; i++)
        {
            if (!char.IsPunctuation(input[i]))
            {
                result += input[i];
            }
        }

        Console.WriteLine("Исходная строка: " + input);
        Console.WriteLine("Результат: " + result);
    }
}