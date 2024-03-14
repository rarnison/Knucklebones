namespace Knucklebones.Console.Screen;

public abstract class StaticScreen : Screen
{
    protected new abstract string Content { get; }
    protected abstract ConsoleKey? ContinueKey { get; }
    protected abstract Screens ContinueTo { get; }

    protected override void OnNavigate()
    {
        base.Content = Content;
    }

    protected override void OnInput(ConsoleKeyInfo input)
    {
        if (ContinueKey == null || ContinueKey == input.Key)
        {
            UiManager.Navigate(ContinueTo);
        }
    }
}