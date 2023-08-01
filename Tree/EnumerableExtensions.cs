using Microsoft.VisualBasic;

namespace System.Collections.Generic;


public delegate bool Try<S, T>(S source, [MaybeNullWhen(false)] out T value);

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

    public static IEnumerable<T> SelectWhere<S, T>(this IEnumerable<S> items, Try<S, T> selector)
    {

        foreach (var item in items)
        {
            if (selector(item, out var value))
            {
                yield return value;
            }
        }
    }
}
