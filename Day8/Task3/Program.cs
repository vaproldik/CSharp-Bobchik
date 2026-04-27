class Program
{
    static void Main()
    {
        SimpleCache<string> myStorage = new SimpleCache<string>();

        CacheManager<string> manager = new CacheManager<string>(myStorage);

        myStorage.Add("Данные сессии #1");
        myStorage.Add("Настройки пользователя");
        myStorage.Add("Временный токен");

        manager.PrintCache();
        Console.WriteLine($"Количество элементов: {manager.Count()}");

        Console.WriteLine("\nОчищаем кэш...");
        myStorage.Clear();
        Console.WriteLine($"Количество после очистки: {manager.Count()}");
    }
}