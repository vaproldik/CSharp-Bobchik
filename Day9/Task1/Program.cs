public class FileManager
{
    public void CreateFile(string path, string text)
    {
        File.WriteAllText(path, text);
        Console.WriteLine($"Файл '{path}' создан и записан.");
    }

    public void CopyFile(string source, string dest)
    {
        File.Copy(source, dest, true);
        Console.WriteLine($"Файл скопирован в '{dest}'.");
    }

    public void MoveFile(string source, string dest)
    {
        File.Move(source, dest);
        Console.WriteLine($"Файл перемещен в '{dest}'.");
    }

    public void RenameFile(string source, string newName)
    {
        File.Move(source, newName);
        Console.WriteLine($"Файл переименован в '{newName}'.");
    }

    public void DeleteFile(string path)
    {

        if (File.Exists(path))
        {
            File.Delete(path);
            Console.WriteLine($"Файл '{path}' успешно удален.");
        }
        else
        {
            Console.WriteLine($"Ошибка: Невозможно удалить '{path}', файл не существует.");
        }
    }
}

public class FileInfoProvider
{
    public void GetDetails(string path)
    {
        if (File.Exists(path))
        {
            FileInfo info = new FileInfo(path);
            Console.WriteLine($"--- Инфо о файле {info.Name} ---");
            Console.WriteLine($"Размер: {info.Length} байт");
            Console.WriteLine($"Создан: {info.CreationTime}");
            Console.WriteLine($"Изменен: {info.LastWriteTime}");
        }
    }

    public long GetSize(string path)
    {
        return new FileInfo(path).Length;
    }
}

class Program
{
    static void Main()
    {
        FileManager fm = new FileManager();
        FileInfoProvider fp = new FileInfoProvider();

        string myFile = "bobchik.pi";
        string copyFile = "bobchik_copy.pi";
        string renamedFile = "bobchik.pi"; 
        string subFolder = "NewFolder";

        try
        {
            fm.CreateFile(myFile, "Привет, это файл Павла Ивановича Бобчика.");
            string content = File.ReadAllText(myFile);
            Console.WriteLine($"Прочитано: {content}");

            fp.GetDetails(myFile);

            fm.CopyFile(myFile, copyFile);
            if (File.Exists(copyFile)) Console.WriteLine("Копия существует.");

            if (fp.GetSize(myFile) == fp.GetSize(copyFile))
                Console.WriteLine("Размеры основного файла и копии совпадают.");

            if (!Directory.Exists(subFolder)) Directory.CreateDirectory(subFolder);
            string movedPath = Path.Combine(subFolder, copyFile);
            if (File.Exists(movedPath)) File.Delete(movedPath); 
            fm.MoveFile(copyFile, movedPath);

            if (File.Exists(renamedFile) && renamedFile != myFile) File.Delete(renamedFile);

            if (myFile != renamedFile) fm.RenameFile(myFile, renamedFile);

            Console.WriteLine("\nСписок файлов в текущей папке:");
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            foreach (string f in files) Console.WriteLine("- " + Path.GetFileName(f));

            FileAttributes attrs = File.GetAttributes(renamedFile);
            Console.WriteLine($"\nПрава: Можно читать? {!attrs.HasFlag(FileAttributes.Offline)}");

            File.SetAttributes(renamedFile, FileAttributes.ReadOnly);
            Console.WriteLine("Установлен атрибут 'Только для чтения'. Пробую записать...");
            try
            {
                File.AppendAllText(renamedFile, "Добавка");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Результат: Запись запрещена (ошибка перехвачена).");
            }
            File.SetAttributes(renamedFile, FileAttributes.Normal);

            fm.DeleteFile("missing_file.txt");

            Console.WriteLine("\nУдаление файлов по шаблону *.pi...");
            string[] piFiles = Directory.GetFiles("./", "*.pi");
            foreach (string f in piFiles) fm.DeleteFile(f);

        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла общая ошибка: " + ex.Message);
        }
    }
}