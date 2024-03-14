namespace Knucklebones.Console.Screen;

public static class UiManager
{
    public static Screen? ActiveScreen { get; private set; }

    public static void Navigate(this Screens screenType)
    {
        System.Console.Clear();
        
        ActiveScreen = screenType switch
        {
            Screens.Title => new ScreenTitle(),
            Screens.Settings => new ScreenSettings(),
            Screens.Game => new ScreenGame(),
            Screens.GameOver => new ScreenGameOver(),
            Screens.Error => new ScreenError(),
            _ => new ScreenError()
        };
        
        ActiveScreen.Navigate();
    }

    public static void Close() => ActiveScreen = null;
}