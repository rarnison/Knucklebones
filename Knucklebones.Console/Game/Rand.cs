using System.Collections.Generic;

namespace Knucklebones.Console;

public static class Rand
{
    private static readonly Random _random = new();

    public static bool CoinFlip => _random.Next(0, 100) < 50;

    public static int DiceRoll => _random.Next(1, 7);
    
    public static int Index => _random.Next(0, 3);

    public static int GetIndex<T>(IEnumerable<T> enumerable) => _random.Next(0, enumerable.Count());
}