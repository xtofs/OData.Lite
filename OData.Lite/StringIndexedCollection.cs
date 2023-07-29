namespace OData.Lite;
using Microsoft.Extensions.Logging;

/// <summary>
/// a collection of items indexed by an item's string property
/// </summary>
/// <typeparam name="TItem"></typeparam>
/// <param name="GetKey"></param>
public abstract record class StringIndexedCollection<TItem>(Func<TItem, string> GetKey) : IEnumerable<TItem>
{
    readonly Func<TItem, string> primaryKey = GetKey;
    // TODO: check if KeyedCollection is a better choice since it supposedly preserves order
    readonly Dictionary<string, TItem> index = new(StringComparer.OrdinalIgnoreCase);

    public void Add(TItem item)
    {
        index.Add(primaryKey(item), item);
    }

    // public void Add(object obj)
    // {
    //     if (obj is TItem item) { primary.Add(primaryKey(item), item); }
    // }

    IEnumerator IEnumerable.GetEnumerator() => index.Values.GetEnumerator();

    public IEnumerator<TItem> GetEnumerator() => index.Values.GetEnumerator();

    public bool TryFind(string name, [MaybeNullWhen(false)] out TItem item)
    {
        Log.Logger.LogInformation("find '{name}' in container '{container}' with keys [{keys}]",
            name,
            this.GetType().Name,
            $"{{{string.Join(", ", index.Keys)}}}");

        var res = index.TryGetValue(name, out item);

        Log.Logger.LogInformation("{phrase} {item}", res ? "found" : "couldn't find", item);

        return res;
    }

    public bool TryFind<T>(string name, [MaybeNullWhen(false)] out T element)
        where T : TItem
    {
        element = default;
        return TryFind(name, out var elem) && elem.Is(out element);
    }

    public override string ToString()
    {
        return $"{string.Join(", ", index.Values)}";
    }

    protected virtual bool PrintMembers(StringBuilder stringBuilder)
    {
        stringBuilder.Append('[');
        stringBuilder.AppendJoin(", ", index.Values);
        stringBuilder.Append(']');
        return true;
    }
}


/// <summary>
/// a collection of items indexed by multiple item string properties
/// </summary>
/// <typeparam name="TItem"></typeparam>
/// <param name="primaryKey"></param>
/// <param name="secondaryKeys"></param> 
/// <typeparam name="TItem"></typeparam>
public abstract record class MultiStringIndexedCollection<TItem>(Func<TItem, string> primaryKey, params Func<TItem, string?>[] secondaryKeys) : IEnumerable<TItem>
{
    readonly Func<TItem, string> primaryKey = primaryKey;
    readonly Dictionary<string, TItem> index = new(StringComparer.OrdinalIgnoreCase);

    readonly Func<TItem, string?>[] secondaryKeys = secondaryKeys;
    readonly Dictionary<string, TItem> secondaryIndex = new(StringComparer.OrdinalIgnoreCase);

    protected IEnumerable<TItem> Items => index.Values;

    public void Add(TItem item)
    {
        index.Add(primaryKey(item), item);
        foreach (var get in secondaryKeys)
        {
            var key = get(item);
            if (key != null)
            {
                secondaryIndex.Add(key, item);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => index.Values.GetEnumerator();

    public IEnumerator<TItem> GetEnumerator() => index.Values.GetEnumerator();

    public bool TryFind(string name, [MaybeNullWhen(false)] out TItem item)
    {
        // // TODO: logging
        // Console.WriteLine("find '{0}' in container '{1}' with keys [{2}]",
        //     name,
        //     this.GetType().Name,
        //     $"{{{string.Join(", ", index.Keys.Concat(secondaryIndex.Keys))}}}");
        Log.Logger.LogInformation("find '{name}' in container '{container}' with keys [{keys}]",
                   name,
                   this.GetType().Name,
                   $"{{{string.Join(", ", index.Keys.Concat(secondaryIndex.Keys))}}}");

        var res = index.TryGetValue(name, out item) || secondaryIndex.TryGetValue(name, out item);

        Log.Logger.LogInformation("{phrase} {item}", res ? "found" : "couldn't find", item);

        return res;
    }

    public bool TryFindItem<T>(string name, [MaybeNullWhen(false)] out T element)
        where T : TItem
    {
        element = default;
        return TryFind(name, out var elem) && elem.Is(out element);
    }

    protected virtual bool PrintMembers(StringBuilder stringBuilder)
    {
        stringBuilder.Append('[');
        stringBuilder.AppendJoin(", ", index.Values);
        stringBuilder.Append(']');
        return true;
    }
}
