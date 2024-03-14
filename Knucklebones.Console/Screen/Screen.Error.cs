namespace Knucklebones.Console.Screen;

public class ScreenError : StaticScreen
{
    protected override string Content => "Something went wrong...\r\npress any key to return to the title screen";
    protected override ConsoleKey? ContinueKey => null;
    protected override Screens ContinueTo => Screens.Title;
}