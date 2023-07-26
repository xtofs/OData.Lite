using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace OData.Lite.EDM.Model;

public class ReadOnlyStringDictionary<TV> : IEnumerable<TV>
{
    private readonly Func<TV, string> primary;
    private readonly Func<TV, string?>[] secondaries;
    private readonly Dictionary<string, TV> index;

    public ReadOnlyStringDictionary(Func<TV, string> primary, params Func<TV, string?>[] secondaries)
    {
        this.primary = primary;
        this.secondaries = secondaries;
        this.index = new Dictionary<string, TV>(StringComparer.OrdinalIgnoreCase);
    }

    public void Add(TV value)
    {
        index.Add(primary(value), value);
        for (int i = 0; i < secondaries.Length; i++)
        {
            var key = secondaries[i](value);
            if (!string.IsNullOrWhiteSpace(key))
            {
                index.Add(key, value);
            }
        }
    }

    internal bool TryGetValue(string key, [MaybeNullWhen(false)] out TV value)
    {
        return index.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator() => index.Values.GetEnumerator();

    public IEnumerator<TV> GetEnumerator() => index.Values.GetEnumerator();

    public override string ToString()
    {
        return $"{{ {string.Join(", ", from p in index select ($"[{p.Key}]= {p.Value}"))}}}";
    }
}


public interface INamedElement
{
    string Name { get; }
}

public abstract record class Container<TItem>() : IEnumerable
    where TItem : INamedElement
{
    protected ReadOnlyStringDictionary<TItem> Elements { get; } = new ReadOnlyStringDictionary<TItem>(ns => ns.Name);

    public void Add(TItem element)
    {
        Elements.Add(element);
    }

    IEnumerator IEnumerable.GetEnumerator() => Elements.GetEnumerator();

    [Obsolete("use .TryFindElement<T>(string name, out T element)")]
    protected IEnumerable<TItem> FindElement(string name)
    {
        if (TryFindElement(name, out var schema))
        {
            yield return schema;
        }
    }

    protected bool TryFindElement(string name, [MaybeNullWhen(false)] out TItem element)
    {
        return Elements.TryGetValue(name, out element);
    }

    protected bool TryFindElement<T>(string name, [MaybeNullWhen(false)] out T element)
        where T : TItem
    {
        element = default;
        return Elements.TryGetValue(name, out var obj) && obj.Is(out element);
    }
}