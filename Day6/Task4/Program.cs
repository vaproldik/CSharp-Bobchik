class Program
{
    static void Main(string[] args)
    {
        BatteryMonitor monitor = new BatteryMonitor();
        PowerManager manager = new PowerManager();

        PowerSaver saver = new PowerSaver();
        UserNotifier notifier = new UserNotifier();

        manager.Link(monitor, saver, notifier);

        Console.WriteLine("Проверка 1: Заряд 50%");
        monitor.CheckBattery(50);

        Console.WriteLine("\nПроверка 2: Заряд 15%");
        monitor.CheckBattery(15);
    }
}