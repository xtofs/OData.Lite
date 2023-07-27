namespace OData.Lite.EDM;

/// <summary>
/// a base class for model elements that contain other model elements that can be index by one or more keys
/// </summary>
/// <typeparam name="TItem"></typeparam>
/// <param name="primaryKey"></param>
/// <param name="secondaryKeys"></param> 
/// <typeparam name="TItem"></typeparam>
public abstract record class MultiKeyContainer<TItem>(Func<TItem, string> primaryKey, params Func<TItem, string?>[] secondaryKeys) : IEnumerable
{
    readonly Func<TItem, string> primaryKey = primaryKey;
    readonly Func<TItem, string?>[] secondaryKeys = secondaryKeys;
    readonly Dictionary<string, TItem> index = new(StringComparer.OrdinalIgnoreCase);
    readonly Dictionary<string, TItem> secondary = new(StringComparer.OrdinalIgnoreCase);

    protected IEnumerable<TItem> Items => index.Values;

    public void Add(TItem item)
    {
        index.Add(primaryKey(item), item);
        foreach (var get in secondaryKeys)
        {
            var key = get(item);
            if (key != null)
            {
                secondary.Add(key, item);
            }
        }
    }

    // to enable for collection initializers
    IEnumerator IEnumerable.GetEnumerator() => index.Values.GetEnumerator();

    protected bool TryFindItem(string name, [MaybeNullWhen(false)] out TItem element)
    {
        // Console.WriteLine("find {0} in container {1} with keys {2}",
        // name,
        //     this.GetType().Name,
        // $"{{{string.Join(", ", index.Keys)}}}");

        var res = index.TryGetValue(name, out element) || secondary.TryGetValue(name, out element);
        // Console.WriteLine("{0}found: {1}", res ? "" : "not ", element);
        return res;
    }

    protected bool TryFindItem<T>(string name, [MaybeNullWhen(false)] out T element)
        where T : TItem
    {
        element = default;
        return TryFindItem(name, out var elem) && elem.Is(out element);
    }


    // public override string ToString()
    // {
    //     return base.ToString();
    // }
}