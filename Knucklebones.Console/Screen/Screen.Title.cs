using Knucklebones.Console.Constants;
using Knucklebones.Console.Models.Screen;

namespace Knucklebones.Console.Screen;

public class ScreenTitle : Screen
{
    private static readonly string OptionSpacing = new(' ', 40);
    
    private readonly Options _options = new(
        new Option("Start Game", StartGame),
        // new Option("Settings", Settings),
        new Option("Quit", Quit)
    );

    protected override void OnNavigate()
    {
        UpdateContent();
    }

    protected override void OnInput(ConsoleKeyInfo input)
    {
        switch (input.Key)
        {
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                UpdateOptions(-1);
                break;
            
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                UpdateOptions(1);
                break;
            
            case ConsoleKey.Enter:
                _options.Selected();
                break;
        }
    }

    private void UpdateOptions(int indexChange)
    {
        _options.MoveSelection(indexChange);
        UpdateContent();
    }

    private void UpdateContent()
    {
        Content = BigText.KnuckleBones + "     " +
                  string.Join(OptionSpacing, _options);
    }

    private static void StartGame() => Screens.Game.Navigate();
    // private static void Settings() => Screens.Settings.Navigate();
    private static void Quit() => UiManager.Close();
}