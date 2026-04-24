public class PowerManager
{
    public void Link(BatteryMonitor monitor, PowerSaver saver, UserNotifier notifier)
    {
        monitor.BatteryLow += saver.Activate;
        monitor.BatteryLow += notifier.ShowWarning;
    }
}