using Knucklebones.Console.Models.Game;
using Knucklebones.Console.Screen;

namespace Knucklebones.Console;

public class GameController
{
    private readonly Action _gameUpdated;
    private readonly Action _gameComplete;

    public static GameResult? Result { get; private set; }

    public GameController(Action gameUpdated, Action gameComplete)
    {
        _gameUpdated = gameUpdated;
        _gameComplete = gameComplete;
    }

    public DiceGrid PlayerGrid { get; } = new(isPlayer: true);
    public DiceGrid ComputerGrid { get; } = new(isPlayer: false);

    public void MoveSelection(int offset)
    {
        PlayerGrid.MoveSelection(offset);
        _gameUpdated();
    }

    public void Place()
    {
        PlaceUsersDice();

        PlaceComputerDice();

        if (!ComputerGrid.IsFull && !PlayerGrid.IsFull)
        {
            PlayerGrid.Turn = true;
            return;
        }

        PlayerGrid.Turn = false;
        Thread.Sleep(1500);
        Result = new GameResult(PlayerGrid.GetScore(), ComputerGrid.GetScore());
        _gameComplete();
    }

    private void PlaceUsersDice()
    {
        var placement = PlayerGrid.PlaceRoll();
        ComputerGrid.HandleOtherPlacement(placement, other: PlayerGrid);
        _gameUpdated();
    }

    private void PlaceComputerDice()
    {
        Thread.Sleep(1000);
        ComputerGrid.RollDice();
        ComputerGrid.SelectRandom();
        _gameUpdated();
        
        Thread.Sleep(1000);
        var placement = ComputerGrid.PlaceRoll();
        PlayerGrid.HandleOtherPlacement(placement, other: ComputerGrid);
        PlayerGrid.RollDice();
        _gameUpdated();
    }

    public void Reset()
    {
        PlayerGrid.Clear();
        ComputerGrid.Clear();

        PlayerGrid.Turn = true;
        PlayerGrid.Columns.First().IsSelected = true;
        PlayerGrid.RollDice();
    }
}