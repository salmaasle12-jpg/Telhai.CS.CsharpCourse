using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // ===== NATIVE VALUE TYPES =====
        int age = 30;
        double price = 19.99;
        float rate = 0.75f;
        bool isActive = true;
        char grade = 'A';
        byte small = 255;
        short shortNum = 12345;
        long bigNum = 9999999999;
        decimal money = 1234.56m;

        Console.WriteLine($"Age: {age}, Price: {price}, Rate: {rate}, Active: {isActive}");
        Console.WriteLine($"Char: {grade}, Byte: {small}, Short: {shortNum}, Long: {bigNum}, Decimal: {money}");

        // ===== OBJECT AND VAR =====
        object anything = "Hello Object";
        var inferred = 42; // var -> compiler infers int
        Console.WriteLine($"Object: {anything}, Var: {inferred}");

        // ===== NULLABLE TYPES =====
        int? optionalNumber = null;
        optionalNumber = 100;
        Console.WriteLine($"Nullable: {optionalNumber.HasValue} -> {optionalNumber.Value}");

        // ===== STRING =====
        string text = "C# is awesome!";
        string formatted = $"{text} - {DateTime.Now.Year}";
        Console.WriteLine(formatted);

        // ===== GUID =====
        Guid id = Guid.NewGuid();
        Console.WriteLine($"GUID: {id}");

        // ===== DATE & TIME =====
        DateTime now = DateTime.Now;
        DateTime tomorrow = now.AddDays(1);
        TimeSpan duration = tomorrow - now;
        Console.WriteLine($"Now: {now}, Tomorrow: {tomorrow}, Diff: {duration.TotalHours} hours");

        // ===== TUPLE =====
        var person = (Name: "Alice", Age: 25, City: "Tel-Aviv");
        Console.WriteLine($"Tuple: {person.Name}, {person.Age}, {person.City}");

        // ===== COLLECTIONS =====
        int[] numbers = { 1, 2, 3, 4 };
        List<string> names = new() { "Bob", "Charlie", "Dana" };
        HashSet<int> unique = new() { 1, 2, 2, 3 };
        Queue<string> queue = new();
        queue.Enqueue("First");
        queue.Enqueue("Second");
        Stack<int> stack = new();
        stack.Push(10);
        stack.Push(20);

        Console.WriteLine($"Array[1]: {numbers[1]}, List Count: {names.Count}, Unique Count: {unique.Count}");
        Console.WriteLine($"Queue Peek: {queue.Peek()}, Stack Pop: {stack.Pop()}");

        // ===== DICTIONARY =====
        Dictionary<string, int> scores = new()
        {
            ["Alice"] = 95,
            ["Bob"] = 82,
            ["Charlie"] = 77
        };
        scores.Add("salma", 100);
        Console.WriteLine($"Bob's Score: {scores["Bob"]}");

        // ===== ENUM =====
        DayOfWeek today = DateTime.Now.DayOfWeek;
        Console.WriteLine($"Today is: {today}");

        // ===== KNOWN DELEGATE (Action / Func) =====
        Action<string> say = msg => Console.WriteLine($"Action says: {msg}");
        Func<int, int, int> add = (a, b) => a + b;
        Func<int, int, int> mult = Test;
        int resM = mult(100, 100); // ✅ FIXED HERE

        say("Hello Delegates");

        Console.WriteLine($"Sum: {add(5, 7)}");
        Console.WriteLine($"Multiply: {resM}");

        // ===== DYNAMIC TYPE =====
        dynamic dyn = "Dynamic Value";
        Console.WriteLine($"Dynamic: {dyn}, Length: {dyn.Length}");

        // ===== ENUMERABLE FOREACH =====
        foreach (var n in numbers)
            Console.Write($"{n} ");
        Console.WriteLine();

        // ===== EXTRA TYPES =====
        // Anonymous type
        var anon = new { Model = "Tesla", Year = 2025 };
        Console.WriteLine($"Anonymous: {anon.Model}, {anon.Year}");

        // KeyValuePair
        KeyValuePair<string, int> pair = new("Points", 100);
        Console.WriteLine($"{pair.Key}: {pair.Value}");
    }

    static int Test(int x, int y)
    {
        return x * y;
    }
}
