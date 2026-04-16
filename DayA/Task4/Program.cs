class Program
{
    static void Main()
    {
        Console.Write("a= ");
        double firstValue = double.Parse(Console.ReadLine());

        Console.Write("b= ");
        double secondValue = double.Parse(Console.ReadLine());

        double productResult = firstValue * secondValue;

        Console.WriteLine($"{firstValue:F1} * {secondValue:F1} = {productResult:F1}");
    }
}