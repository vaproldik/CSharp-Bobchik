public class PowerSaver
{
    public void Activate(object sender, BatteryEventArgs e)
    {
        Console.WriteLine("Система: Энергосбережение ВКЛ. Заряд: " + e.Percent + "%");
    }
}

public class UserNotifier
{
    public void ShowWarning(object sender, BatteryEventArgs e)
    {
        Console.WriteLine("Уведомление: Подключите зарядку! Осталось " + e.Percent + "%");
    }
}