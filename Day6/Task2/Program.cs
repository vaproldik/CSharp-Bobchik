class Program
{
    static void Main(string[] args)
    {
        LogManager manager = new LogManager();

        manager.LogMessage("Система запущена успешно.", LoggerMethods.ConsoleLog);

        manager.LogMessage("Критическая ошибка в модуле сети.", LoggerMethods.FileLog);

        Console.ReadKey();
    }
}