class Program
{
    static void Main(string[] args)
    {
        UpperCaseConverter upper = new UpperCaseConverter();
        LowerCaseConverter lower = new LowerCaseConverter();

        TextProcessor processor = upper.ConvertToUpper;
        Console.WriteLine("В больших буквах: " + processor("привет мир"));

        processor = lower.ConvertToLower;
        Console.WriteLine("В маленьких буквах: " + processor("ПРИВЕТ МИР"));
    }
}