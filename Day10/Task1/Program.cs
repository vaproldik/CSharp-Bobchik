public class DatabaseConnection
{
    private static DatabaseConnection _instance;

    private DatabaseConnection()
    {
        Console.WriteLine("Создан новый объект DatabaseConnection.");
    }

    public static DatabaseConnection GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DatabaseConnection();
        }
        return _instance;
    }

    public void Connect()
    {
        Console.WriteLine("Соединение с базой данных установлено.");
    }

    public void Disconnect()
    {
        Console.WriteLine("Соединение с базой данных разорвано.");
    }

    public void ExecuteQuery(string query)
    {
        Console.WriteLine($"Выполнение SQL-запроса: {query}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        DatabaseConnection connection1 = DatabaseConnection.GetInstance();
        connection1.Connect();
        connection1.ExecuteQuery("SELECT * FROM Users");

        DatabaseConnection connection2 = DatabaseConnection.GetInstance();
        connection2.ExecuteQuery("UPDATE Users SET Name = 'Pavel' WHERE Id = 1");

        if (connection1 == connection2)
        {
            Console.WriteLine("Оба объекта ссылаются на один и тот же экземпляр.");
        }

        connection1.Disconnect();
    }
}