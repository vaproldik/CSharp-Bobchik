using System.Text.RegularExpressions;

class TaskRegex
{
    static void Main()
    {
        string testString = "привет Студент";

        bool hasUpper = Regex.IsMatch(testString, @"[A-ZА-Я]");

        Console.WriteLine("Текст: " + testString);
        Console.WriteLine("Есть заглавные буквы? " + (hasUpper ? "Да" : "Нет"));
    }
}