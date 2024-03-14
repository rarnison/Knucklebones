namespace Knucklebones.Console.Screen;

public abstract class Screen
{
    private string _content = string.Empty;
    
    public void Navigate()
    {
        OnNavigate();
        UpdateConsole();
        ListenForInput();
    }

    protected abstract void OnNavigate();

    protected virtual string Content
    {
        get => _content;
        set => OnContentChanged(value);
    }

    protected abstract void OnInput(ConsoleKeyInfo input);

    private void ListenForInput()
    {
        while (_active)
        {
            var input = Input.ReadKey();
            OnInput(input);
        }
    }

    private void OnContentChanged(string value)
    {
        if (_content == value) return;

        _content = value;
        UpdateConsole();
    }

    private void UpdateConsole()
    {
        System.Console.SetCursorPosition(0, 0);
        System.Console.WriteLine(Content);
    }

    private bool _active => UiManager.ActiveScreen == this;
}
    
public enum Screens
{
    Title,
    Settings,
    Game,
    GameOver, 
    Error
}
