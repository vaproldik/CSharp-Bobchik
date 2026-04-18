class Program
{
    static void Main()
    {
        int[][] jagged = new int[3][];
        jagged[0] = new int[] { 1, 2, 3 };
        jagged[1] = new int[] { 0, 0, 0 }; 
        jagged[2] = new int[] { 4, 5 };

        List<int[]> result = new List<int[]>();

        foreach (var row in jagged)
        {
            int sum = 0;
            foreach (int val in row) sum += val;

            if (sum != 0)
            {
                result.Add(row);
            }
        }

        Console.WriteLine("Массив после удаления строк с нулевой суммой:");
        foreach (var row in result)
        {
            Console.WriteLine(string.Join(" ", row));
        }
    }
}