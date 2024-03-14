using Knucklebones.Console.Constants;
using Knucklebones.Console.Models.Game;

namespace Knucklebones.Console.Screen;

public class ScreenGame : Screen
{
    private const string GameTemplate = @"
                   c.c1.selected      c.c2.selected      c.c3.selected          CPU
                +-----++-----++-----+     +-----+
                |  c.c1.3  ||  c.c2.3  ||  c.c3.3  |     |  c.roll  |
                +-----++-----++-----+     +-----+
                |  c.c1.2  ||  c.c2.2  ||  c.c3.2  |      c.score
                +-----++-----++-----+
                |  c.c1.1  ||  c.c2.1  ||  c.c3.1  | 
                +-----++-----++-----+
                  c.c1.score    c.c2.score    c.c3.score

    Player        p.c1.score    p.c2.score    p.c3.score
    +-----+     +-----++-----++-----+
    |  p.roll  |     |  p.c1.1  ||  p.c2.1  ||  p.c3.1  |
    +-----+     +-----++-----++-----+
     p.score      |  p.c1.2  ||  p.c2.2  ||  p.c3.2  | 
                +-----++-----++-----+
                |  p.c1.3  ||  p.c2.3  ||  p.c3.3  | 
                +-----++-----++-----+
                   p.c1.selected      p.c2.selected      p.c3.selected
";

    private readonly GameController _controller;
    
    public ScreenGame()
    {
        _controller = new GameController(UpdateDisplay, NavigateToEndScreen);
    }

    private void NavigateToEndScreen()
    {
        Screens.GameOver.Navigate();
    }

    protected override void OnNavigate()
    {
        UpdateDisplay();
    }

    protected override void OnInput(ConsoleKeyInfo input)
    {
        switch (input.Key)
        {
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                _controller.MoveSelection(-1);
                break;
            
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                _controller.MoveSelection(1);
                break;
            
            case ConsoleKey.Enter:
                _controller.Place();
                break;
        }
    }

    private void UpdateDisplay()
    {
        Content = ParseGameTemplate();
    }

    private string ParseGameTemplate()
    {
        var result = GameTemplate;
        
        result = ParseGrid(result, _controller.PlayerGrid, "p");
        result = ParseGrid(result, _controller.ComputerGrid, "c");

        return result;
    }

    private string ParseGrid(string result, DiceGrid grid, string gridKey)
    {
        result = ParseColumn(result, grid, gridKey, 1);
        result = ParseColumn(result, grid, gridKey, 2);
        result = ParseColumn(result, grid, gridKey, 3);

        return result
            .Replace(gridKey + ".score", grid.DisplayScore())
            .Replace(gridKey + ".roll", grid.Roll?.ToString() ?? " ");
    }

    private string ParseColumn(string result, DiceGrid grid, string gridKey, int columnKey)
    {
        result = ParseColumnValue(result, grid, gridKey, columnKey, 1);
        result = ParseColumnValue(result, grid, gridKey, columnKey, 2);
        result = ParseColumnValue(result, grid, gridKey, columnKey, 3);
        
        return result
            .Replace($"{gridKey}.c{columnKey}.score", grid.Columns[columnKey - 1].DisplayScore())
            .Replace($"{gridKey}.c{columnKey}.selected", SelectedCharacter(grid, gridKey, columnKey));
    }

    private string SelectedCharacter(DiceGrid grid, string gridKey, int columnKey)
    {
        const string empty = " ";
        var selectedCharacter = gridKey == "p" ? "^" : "v";

        if (grid.Turn && grid.Columns[columnKey - 1].IsSelected)
            return selectedCharacter;

        return empty;
    }

    private string ParseColumnValue(string result, DiceGrid grid, string gridKey, int columnKey, int valueKey)
    {
        var value = grid.Columns[columnKey - 1].Values[valueKey - 1];
        
        return result
            .Replace($"{gridKey}.c{columnKey}.{valueKey}", value == null ? " " : value.ToString());
    }
}