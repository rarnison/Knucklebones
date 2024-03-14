namespace Knucklebones.Console.Models.Game;

public class DiceColumn
{
    public int?[] Values { get; private set; } = new int?[3];

    public bool IsSelected { get; set; }

    public bool IsSelectable => Values.Any(v => v == null);

    public int GetScore()
    {
        var score = 0;

        foreach (var valueGroup in Values.GroupBy(v => v))
        {
            if (!valueGroup.Key.HasValue)
                continue;

            var amount = valueGroup.Count();
            score += valueGroup.Key.Value * amount * amount;
        }

        return score;
    }

    public string DisplayScore()
    {
        var score = GetScore();

        var spacing = score switch
        {
            < 10 => " ",
            _ => ""
        };

        return spacing + string.Join(' ', score.ToString().Cast<char>()) + spacing;
    }

    public void Add(int roll)
    {
        if (Values[0] == null) Values[0] = roll;
        else if (Values[1] == null) Values[1] = roll;
        else if (Values[2] == null) Values[2] = roll;
    }

    public void Remove(int placementValue)
    {
        Values = Values
            .Select((value, index) => new { index, value = value == placementValue ? null : value })
            .OrderBy(a => a.value == null)
            .ThenBy(a => a.index)
            .Select(a => a.value)
            .ToArray();
    }
}