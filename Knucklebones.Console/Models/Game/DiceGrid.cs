namespace Knucklebones.Console.Models.Game;

public class DiceGrid
{
    public DiceColumn[] Columns = { new(), new(), new() };

    public DiceGrid(bool isPlayer)
    {
        if (!isPlayer)
            return;

        RollDice();
        Turn = true;
        Columns.First().IsSelected = true;
    }

    public int? Roll { get; private set; }
    public bool Turn { get; set; }

    public bool IsFull => Columns.All(c => !c.IsSelectable);

    public int GetScore()
    {
        return Columns.Sum(c => c.GetScore());
    }

    public void Clear()
    {
        Turn = false;
        Roll = null;

        for (var ci = 0; ci < 3; ci++)
        {
            Columns[ci].IsSelected = false;

            for (var cvi = 0; cvi < 3; cvi++)
            {
                Columns[ci].Values[cvi] = null;
            }
        }
    }
    
    public string DisplayScore()
    {
        var score = GetScore();

        var spacing = score switch
        {
            < 10 => "  ",
            < 100 => " ",
            _ => ""
        };

        var splitScore = string.Join(' ', score.ToString().Cast<char>());

        return spacing + splitScore + spacing;
    }

    public void CheckSelection()
    { 
        var selectedColumn = Columns.FirstOrDefault(c => c.IsSelected);
        if (selectedColumn == null)
            return;

        if (selectedColumn.IsSelectable)
            return;

        selectedColumn.IsSelected = false;
        selectedColumn = Columns.FirstOrDefault(c => c.IsSelectable);
        if (selectedColumn == null)
            return;
        selectedColumn.IsSelected = true;
    }

    public void MoveSelection(int offset)
    {
        var selectedColumn = Columns.FirstOrDefault(c => c.IsSelected);

        if (selectedColumn == null)
        {
            selectedColumn = Columns.FirstOrDefault(c => c.IsSelectable);
            if (selectedColumn != null)
                selectedColumn.IsSelected = true;
            return;
        }

        var newIndex = Math.Clamp(Array.IndexOf(Columns, selectedColumn) + offset, 0, 2);

        if (Columns[newIndex].IsSelectable)
        {
            selectedColumn.IsSelected = false;
            Columns[newIndex].IsSelected = true;
            return;
        }

        newIndex = Math.Clamp(newIndex + offset, 0, 2);
        if (Columns[newIndex].IsSelectable)
        {
            selectedColumn.IsSelected = false;
            Columns[newIndex].IsSelected = true;
        }
    }

    public void RollDice()
    {
        Roll = Rand.DiceRoll;
    }

    public RollPlacement PlaceRoll()
    {
        Roll ??= Rand.DiceRoll;
        
        var selectedColumn = Columns.FirstOrDefault(c => c.IsSelected);
        if (selectedColumn == null)
            return new RollPlacement(Roll.Value, 0);

        selectedColumn.Add(Roll.Value);
        
        CheckSelection();
        
        var placement = new RollPlacement(Roll.Value, Array.IndexOf(Columns, selectedColumn));

        Roll = null;
        return placement;
    }

    public void SelectRandom()
    {
        if (Columns.All(c => !c.IsSelectable)) return;

        var selectableColumns = Columns
            .Select((c, i) => new { column = c, index = i })
            .Where(a => a.column.IsSelectable)
            .ToArray();

        var index = selectableColumns.ElementAt(Rand.GetIndex(selectableColumns)).index;
        for (var i = 0; i < Columns.Length; i++)
        {
            Columns[i].IsSelected = i == index;
        }
    }

    public void HandleOtherPlacement(RollPlacement placement, DiceGrid other)
    {
        var column = Columns[placement.ColumnIndex];
        column.Remove(placement.Value);

        other.Turn = false;
        Turn = true;
    }
}