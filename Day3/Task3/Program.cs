class Program
{
    static void Main()
    {
        University bntu = new University();
        bntu.People = new Person[]
        {
                new Student("Иван Иванов", 20, 4.5),
                new Student("Мария Петрова", 19, 4.9),
                new Student("Алексей Сидоров", 21, 3.8),
                new Teacher("Дмитрий Николаевич", 45, "Программирование"),
                new Teacher("Ольга Сергеевна", 38, "Высшая математика"),
                new Teacher("Виктор Петрович", 60, "Физика")
        };

        Student topStudent = bntu.GetBestStudent();
        if (topStudent != null)
        {
            Console.WriteLine($"Лучший студент: {topStudent.FullName}");
            Console.WriteLine($"Средний балл: {topStudent.AverageGrade}");
        }

        int ageLimit = 40;
        Console.WriteLine($"\nПреподаватели старше {ageLimit} лет:");
        Teacher[] seniorTeachers = bntu.GetTeachersByAge(ageLimit);

        if (seniorTeachers.Length > 0)
        {
            foreach (var t in seniorTeachers)
            {
                Console.WriteLine($"- {t.FullName} ({t.Age} лет), предмет: {t.Subject}");
            }
        }
        else
        {
            Console.WriteLine("Преподаватели не найдены.");
        }
    }
}