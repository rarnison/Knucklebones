using Knucklebones.Console.Screen;

namespace Knucklebones.Console;

public class Program
{
    public static void Main(string[] args)
    {
        Input.Initialise(args.FirstOrDefault() == "auto" ? new AutoInput() : new ConsoleInput());
        System.Console.CursorVisible = false;
        
        Screens.Title.Navigate();
    }
}