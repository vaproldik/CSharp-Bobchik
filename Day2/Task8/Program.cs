class Program
{
    static void Main()
    {
        Console.Write("Введите название переменной (идентификатор): ");
        string id = Console.ReadLine();

        if (string.IsNullOrEmpty(id))
        {
            Console.WriteLine("Ошибка: строка пуста.");
            return;
        }

        bool isValidStart = char.IsLetter(id[0]) || id[0] == '_';

        if (isValidStart)
        {
            Console.WriteLine($"Строка '{id}' может быть идентификатором.");
        }
        else
        {
            Console.WriteLine($"Строка '{id}' НЕ может быть идентификатором (начинается с недопустимого символа).");
        }
    }
}