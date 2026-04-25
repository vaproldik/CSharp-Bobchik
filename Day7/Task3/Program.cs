class Program
{
    static void Main()
    {
        Calculator myCalc = new Calculator();
        try
        {
            Console.WriteLine("Пробуем поделить 10 на 0...");
            int result = myCalc.Divide(10, 0);
            Console.WriteLine($"Результат: {result}");
        }
        catch (DivisionByZeroException ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Программа завершила работу.");
        }
    }
}