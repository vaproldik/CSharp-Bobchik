class Program
{
    static void Main()
    {
        NumberProcessor processor = new NumberProcessor();
        try
        {
            processor.Process("это_не_число");
        }
        catch (InvalidNumberException ex)
        {
            Console.WriteLine("--- ЛОГ ОШИБКИ ---");
            Console.WriteLine($"Сообщение: {ex.Message}");
            Console.WriteLine($"Причина (Inner): {ex.InnerException.Message}");
            Console.WriteLine($"Стек вызовов:\n{ex.StackTrace}");
        }
    }
}