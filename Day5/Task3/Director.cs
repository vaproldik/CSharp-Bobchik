public class Director : Employee, IManager
{
    public void Manage()
    {
        Console.WriteLine($"Директор {Name} проводит совещание и управляет отделом.");
    }
}