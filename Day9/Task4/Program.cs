public class FileWatcher
{
    private FileSystemWatcher watcher;
    private string logPath = "log.txt";

    public void Start(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        watcher = new FileSystemWatcher(folderPath);

        watcher.Renamed += OnRenamed;

        watcher.EnableRaisingEvents = true;

        Console.WriteLine($"Слежение за папкой '{folderPath}' запущено.");
        Console.WriteLine("Переименуйте любой файл в этой папке, чтобы увидеть результат.");
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        string message = $"Файл {e.OldName} переименован в {e.Name}";

        Console.WriteLine(message);

        string logEntry = $"{DateTime.Now}: {message}{Environment.NewLine}";
        File.AppendAllText(logPath, logEntry);
    }
}

class Program
{
    static void Main()
    {
        FileWatcher fw = new FileWatcher();

        fw.Start("./MyTestFolder");

        Console.WriteLine("Для выхода из программы нажмите 'q'...");
        while (Console.Read() != 'q') ;
    }
}