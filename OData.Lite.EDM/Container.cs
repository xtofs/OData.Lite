namespace OData.Lite.EDM;


/// <summary>
/// a base class for model elements that contain other model elments that can be index by a string
/// </summary>
/// <typeparam name="TItem"></typeparam>
/// <param name="GetKey"></param>
/// <typeparam name="TItem"></typeparam>
public abstract record class Container<TItem>(Func<TItem, string> GetKey) : IEnumerable, IEnumerable<TItem>
{
    readonly Func<TItem, string> primaryKey = GetKey;
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

    public bool TryFind(string name, [MaybeNullWhen(false)] out TItem element)
    {
        // Console.WriteLine("find {0} in container {1} with keys {2}",
        // name,
        //     this.GetType().Name,
        // $"{{{string.Join(", ", index.Keys)}}}");

        var res = index.TryGetValue(name, out element);
        // Console.WriteLine("{0}found: {1}", res ? "" : "not ", element);
        return res;
    }

    public bool TryFind<T>(string name, [MaybeNullWhen(false)] out T element)
        where T : TItem
    {
        element = default;
        return TryFind(name, out var elem) && elem.Is(out element);
    }
}
