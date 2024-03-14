using Knucklebones.Console.Constants;

namespace Knucklebones.Console.Screen;

public class ScreenSettings : Screen
{
    protected override void OnNavigate()
    {
        Content = BigText.Settings;
    }

    protected override void OnInput(ConsoleKeyInfo input)
    {
        UiManager.Navigate(Screens.Game);
    }
}