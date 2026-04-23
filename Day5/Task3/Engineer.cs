public class Engineer : Employee, IWorker
{
    public void Work()
    {
        Console.WriteLine($"Инженер {Name} разрабатывает техническую документацию.");
    }
}