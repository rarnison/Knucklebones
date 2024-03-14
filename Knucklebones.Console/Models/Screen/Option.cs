using System.Collections;
using System.Collections.Generic;

namespace Knucklebones.Console.Models.Screen;

public class Options : IEnumerable<Option>
{
    private readonly Option[] _options;
    private int _selectedIndex = 0;

    public Options(params Option[] options)
    {
        _options = options;
        SetSelectedValues();
    }

    public bool MoveSelection(int indexChange)
    {
        var newIndex = Math.Clamp(
            value: _selectedIndex + indexChange,
            min: 0,
            max: _options.Length - 1);

        if (newIndex == _selectedIndex) 
            return false;

        _selectedIndex = newIndex;
        SetSelectedValues();

        return true;
    }

    public void Selected()
    {
        _options[_selectedIndex].Selected();
    }

    private void SetSelectedValues()
    {
        for (var index = 0; index < _options.Length; index++)
        {
            _options[index].IsSelected = index == _selectedIndex;
        }
    }

    #region IEnumerable

    public IEnumerator<Option> GetEnumerator() => ((IEnumerable<Option>)_options).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _options.GetEnumerator();

    #endregion
}

public class Option
{
    private readonly string _header;
    private readonly Action _onSelect;

    public Option(string header, Action onSelect)
    {
        _header = header;
        _onSelect = onSelect;
    }

    public bool IsSelected { get; set; }

    public override string ToString()
    {
        return (IsSelected ? '>' : ' ') + _header;
    }

    public void Selected() => _onSelect();
}