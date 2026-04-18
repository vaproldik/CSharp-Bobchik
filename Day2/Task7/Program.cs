class Program
{
    static void Main()
    {
        string text = "C# это объектно-ориентированный язык программирования";

        string[] words = text.Split(' ');

        Console.WriteLine("Исходный текст: " + text);
        Console.WriteLine("Список полученных слов:");

        for (int i = 0; i < words.Length; i++)
        {
            Console.WriteLine($"Слово [{i}]: {words[i]}");
        }
    }
}