namespace OData.Lite.EDM;


/// <summary>
/// a base class for model elements that contain other model elments that can be index by a string
/// </summary>
/// <typeparam name="TItem"></typeparam>
/// <param name="primaryKey"></param>
/// <typeparam name="TItem"></typeparam>
public abstract record class Container<TItem>(Func<TItem, string> primaryKey) : IEnumerable
{
    readonly Func<TItem, string> primaryKey = primaryKey;
    readonly Dictionary<string, TItem> primary = new(StringComparer.OrdinalIgnoreCase);

    public IEnumerable<TItem> Values => primary.Values;

    public void Add(TItem item)
    {
        primary.Add(primaryKey(item), item);
    }

    // to enable for collection initializers
    IEnumerator IEnumerable.GetEnumerator() => primary.Values.GetEnumerator();

    protected bool TryFindItem(string name, [MaybeNullWhen(false)] out TItem element)
    {
        // Console.WriteLine("find {0} in container {1} with keys {2}",
        // name,
        //     this.GetType().Name,
        // $"{{{string.Join(", ", index.Keys)}}}");

        var res = primary.TryGetValue(name, out element);
        // Console.WriteLine("{0}found: {1}", res ? "" : "not ", element);
        return res;
    }

    protected bool TryFindItem<T>(string name, [MaybeNullWhen(false)] out T element)
        where T : TItem
    {
        element = default;
        return TryFindItem(name, out var elem) && elem.Is(out element);
    }
}
