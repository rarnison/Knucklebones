using Knucklebones.Console.Constants;
using Knucklebones.Console.Models.Game;

namespace Knucklebones.Console.Screen;

public class ScreenGameOver : StaticScreen
{
    // protected override void OnInput(ConsoleKeyInfo input)
    // {
    // }
    //
    // protected override void OnNavigate()
    // {
    //     var result = GameController.Result;
    //
    //     if (result == null)
    //     {
    //         GoToTitle();
    //         return;
    //     }
    //     
    //     if ()
    //         DisplayWinner(result);
    //     else
    //         DisplayLoser(result);
    //
    //     var bigText = result.PlayerScore > result.CpuScore;
    // }
    //
    // private void DisplayWinner(GameResult result)
    // {
    //     Content = BigText.Winner + Environment.NewLine + Scores(result);
    // }
    //
    // private void DisplayLoser(GameResult result)
    // {
    //     Content = BigText.GameOver + Environment.NewLine + Scores(result);
    // }
    //
    // private string Scores(GameResult result)
    // {
    //     return $"         Player:{result.PlayerScore}     CPU:{result.CpuScore}";
    // }
    //
    // private void GoToTitle()
    // {
    //     Screens.Title.Navigate();
    // }
    protected override string Content => ParseGameResult(GameController.Result);
    protected override ConsoleKey? ContinueKey => null;
    protected override Screens ContinueTo => Screens.Title;

    private string ParseGameResult(GameResult? result)
    {
        if (result == null)
        {
            ContinueTo.Navigate();
            return string.Empty;
        }

        return (result.PlayerScore > result.CpuScore ? BigText.Winner : BigText.GameOver) + Environment.NewLine +
               $"        player:{result.PlayerScore}     cpu:{result.CpuScore}" + Environment.NewLine +
               BigText.PressAnyKeyToContinue;
    }
}