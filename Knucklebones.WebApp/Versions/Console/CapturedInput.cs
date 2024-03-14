using Microsoft.AspNetCore.Components.Web;

namespace Knucklebones.WebApp.Versions.Console;

public class CapturedInput : Knucklebones.Console.Screen.Input
{
    private static readonly ManualResetEvent PropertyChangedEvent = new(false);
    private static ConsoleKeyInfo _keyInfo;

    protected override ConsoleKeyInfo ProtectedReadKey()
    {
        PropertyChangedEvent.WaitOne();
        PropertyChangedEvent.Reset();
        return _keyInfo;
    }

    public void SendKey(KeyboardEventArgs args)
    {
        _keyInfo = new ConsoleKeyInfo(' ', MapKey(args.Key), args.ShiftKey, args.AltKey, args.CtrlKey);
        PropertyChangedEvent.Set();
    }

    private static ConsoleKey MapKey(string key)
    {
        return key switch
        {
            "Enter" => ConsoleKey.Enter,
            "ArrowLeft" => ConsoleKey.LeftArrow,
            "ArrowRight" => ConsoleKey.RightArrow,
            "a" => ConsoleKey.A,
            "d" => ConsoleKey.D,
            _ => ConsoleKey.NoName
        };
    }

    public void Clear()
    {
        PropertyChangedEvent.Reset();
    }
}