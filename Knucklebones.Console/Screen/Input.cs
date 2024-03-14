namespace Knucklebones.Console.Screen;

public abstract class Input
{
    private static Input _instance = null!;
    
    public static void Initialise(Input input)
    {
        _instance = input;
    }

    public static ConsoleKeyInfo ReadKey()
    {
        return _instance.ProtectedReadKey();
    }

    protected abstract ConsoleKeyInfo ProtectedReadKey();
}

public class ConsoleInput : Input
{
    protected override ConsoleKeyInfo ProtectedReadKey()
    {
        return System.Console.ReadKey(true);
    }
}

public class AutoInput : Input
{
    protected override ConsoleKeyInfo ProtectedReadKey()
    {
        Thread.Sleep(2500);
        return new ConsoleKeyInfo('\n', ConsoleKey.Enter, false, false, false);
    }
}