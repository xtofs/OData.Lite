namespace OData.Lite.EDM.Model;


/// <summary>
/// a base class for all model elements that contain other model elments that can be index by a string
/// </summary>
/// <typeparam name="TItem"></typeparam>
/// <param name="primaryKey"></param>
/// <param name="secondaryKeys"></param> 
/// <typeparam name="TItem"></typeparam>
public abstract record class Container<TItem>(Func<TItem, string> primaryKey, params Func<TItem, string?>[] secondaryKeys) : IEnumerable
{
    readonly Func<TItem, string> primaryKey = primaryKey;
    readonly Func<TItem, string?>[] secondaryKeys = secondaryKeys;
    readonly Dictionary<string, TItem> index = new(StringComparer.OrdinalIgnoreCase);

    public IEnumerable<TItem> Values => index.Values;

    public void Add(TItem item)
    {
        index.Add(primaryKey(item), item);
        foreach (var get in secondaryKeys)
        {
            var key = get(item);
            if (key != null)
            {
                index.Add(key, item);
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

        var res = index.TryGetValue(name, out element);
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