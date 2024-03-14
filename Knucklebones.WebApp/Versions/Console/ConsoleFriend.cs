using System.Text;

namespace Knucklebones.WebApp.Versions.Console;

public static class ConsoleFriend
{
    public static void RedirectConsoleOut(Action<string> update)
    {
        System.Console.SetOut(new ConsoleOut(update));
    }

    private class ConsoleOut : TextWriter
    {
        private readonly Action<string> _update;
        
        public override Encoding Encoding => Encoding.Default;

        public override void Write(string? value)
        {
            if (!string.IsNullOrEmpty(value?.Trim()))
                _update(value);
        }
        
        public ConsoleOut(Action<string> update)
        {
            _update = update;
        }
    }
}