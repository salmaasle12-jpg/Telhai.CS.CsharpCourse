using Telhai.CS.CsharpCourse.Drawing;

internal class Program
{
    static void Main(string[] args)
    {
        Drawing d1 = new Rectangle();
        Drawing d2 = new Square();

        Console.WriteLine(d1);
        Console.WriteLine(d2);

        List<Drawing> drawingList = new List<Drawing>
            {
                new Rectangle(),
                new Square(),
                new Square() { Length = 10 },
                new Rectangle() { Height = 4, Width = 7 }
            };

        foreach (Drawing d in drawingList)
        {
            Console.WriteLine(d);
        }

        Dictionary<string, Drawing> drawingDic = new Dictionary<string, Drawing>
            {
                { "Rect1", d1 },
                { "Square1", d2 }
            };

        foreach (var d in drawingDic)
        {
            Console.WriteLine($"{d.Key}: {d.Value}");
        }

        string userName = Environment.UserName;
        string machineName = Environment.MachineName;
        string osVersion = Environment.OSVersion.ToString();

        Console.WriteLine($"Current user: {userName}");
        Console.WriteLine($"Current machine: {machineName}");
        Console.WriteLine($"Operating system: {osVersion}");
    }
}




