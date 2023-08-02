namespace System.Collections.Generic;

public static class EnumerableExtensions
{
    public static IEnumerable<(bool, T, bool)> WithFirstLast<T>(this IEnumerable<T> items)
    {
        var enumerator = items.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            yield break;
        }
        T previous = enumerator.Current;
        bool first = true;
        while (enumerator.MoveNext())
        {
            yield return (first, previous, false);
            first = false;
            previous = enumerator.Current;
        }
        yield return (first, previous, true);
    }

    public static IEnumerable<(T, bool)> WithLast<T>(this IEnumerable<T> items)
    {
        var enumerator = items.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            yield break;
        }
        T previous = enumerator.Current;
        while (enumerator.MoveNext())
        {
            yield return (previous, false);
            previous = enumerator.Current;
        }
        yield return (previous, true);
    }
}
