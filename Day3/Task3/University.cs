public class University
{
    public Person[] People { get; set; }

    public Student GetBestStudent()
    {
        Student best = null;
        foreach (var p in People)
        {
            if (p is Student currentStudent)
            {
                if (best == null || currentStudent.AverageGrade > best.AverageGrade)
                {
                    best = currentStudent;
                }
            }
        }
        return best;
    }

    public Teacher[] GetTeachersByAge(int targetAge)
    {
        int count = 0;
        foreach (var p in People)
        {
            if (p is Teacher t && t.Age > targetAge) count++;
        }

        Teacher[] result = new Teacher[count];
        int index = 0;
        foreach (var p in People)
        {
            if (p is Teacher t && t.Age > targetAge)
            {
                result[index++] = t;
            }
        }
        return result;
    }
}