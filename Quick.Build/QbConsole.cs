namespace Quick.Build;

public static class QbConsole
{
    public static void DisplaySameLineInConsole(string line)
    {
        if (Console.IsOutputRedirected)
        {
            Console.WriteLine(line);
            return;
        }
        Console.CursorLeft = 0;
        Console.Write(string.Empty.PadRight(Console.WindowWidth - 1));
        Console.CursorLeft = 0;
        Console.Write(line);
    }
}
