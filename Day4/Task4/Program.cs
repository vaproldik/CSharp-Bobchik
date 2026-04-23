public static class StringHelper
{
    public static string RemoveSpaces(this string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return input.Replace(" ", "");
    }
}

class Program
{
    static void Main()
    {
        string myText = "С++ это круто, но C# удобнее!";

        string result = myText.RemoveSpaces();

        Console.WriteLine($"До:   \"{myText}\"");
        Console.WriteLine($"После: \"{result}\"");
    }
}