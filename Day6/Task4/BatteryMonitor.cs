public class BatteryMonitor
{
    public event EventHandler<BatteryEventArgs> BatteryLow;

    public void CheckBattery(int level)
    {
        if (level < 20)
        {
            BatteryLow?.Invoke(this, new BatteryEventArgs { Percent = level });
        }
    }
}