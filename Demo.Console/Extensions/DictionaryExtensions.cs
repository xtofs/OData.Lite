namespace System.Collections.Generic;

public static class DictionaryExtensions
{
    public static Dictionary<T, int> Frequency<T>(this IEnumerable<T> items) where T : notnull
    {
        var counter = new Dictionary<T, int>();
        foreach (var item in items)
        {
            counter.AddOrUpdate(item, _ => 1, (_, v) => v + 1);
        }
        return counter;
    }

    public static void AddOrUpdate<TK, TV>(this Dictionary<TK, TV> dictionary, TK key, Func<TK, TV> add, Func<TK, TV, TV> update)
        where TK : notnull
    {
        if (dictionary.TryGetValue(key, out var val))
        {
            dictionary[key] = update(key, val);
        }
        else
        {
            dictionary[key] = add(key);
        }
    }
}