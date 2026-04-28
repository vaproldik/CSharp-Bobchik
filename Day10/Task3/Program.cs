public interface IWeatherObserver
{
    void Update(float temperature);
}

public class WeatherStation
{
    private List<IWeatherObserver> _observers = new List<IWeatherObserver>();
    private float _temperature;

    public void Subscribe(IWeatherObserver observer)
    {
        _observers.Add(observer);
    }

    public void Unsubscribe(IWeatherObserver observer)
    {
        _observers.Remove(observer);
    }

    public void SetTemperature(float temperature)
    {
        _temperature = temperature;
        NotifyObservers();
    }

    private void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_temperature);
        }
    }
}

public class MobileApp : IWeatherObserver
{
    public void Update(float temperature)
    {
        Console.WriteLine($"Уведомление в приложении: Температура теперь {temperature}°C");
    }
}

public class Website : IWeatherObserver
{
    public void Update(float temperature)
    {
        Console.WriteLine($"Обновление на сайте: Текущая погода {temperature}°C");
    }
}

class Program
{
    static void Main(string[] args)
    {
        WeatherStation station = new WeatherStation();

        MobileApp myPhone = new MobileApp();
        Website mySite = new Website();

        station.Subscribe(myPhone);
        station.Subscribe(mySite);

        Console.WriteLine("Изменение погоды утром:");
        station.SetTemperature(15.5f);

        Console.WriteLine("\nИзменение погоды днем:");
        station.SetTemperature(22.0f);

        station.Unsubscribe(mySite);

        Console.WriteLine("\nИзменение погоды вечером (сайт отписан):");
        station.SetTemperature(18.0f);
    }
}