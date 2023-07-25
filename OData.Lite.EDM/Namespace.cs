using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace OData.Lite.EDM;

public class Namespace<TV> : IEnumerable<TV>
{
    private readonly Func<TV, string>[] keys;
    private readonly Dictionary<string, TV> index;

    public Namespace(params Func<TV, string>[] keys)
    {
        this.keys = keys;
        this.index = new Dictionary<string, TV>();
    }

    public void Add(TV value)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            var key = keys[i](value);
            if (!string.IsNullOrWhiteSpace(key))
            {
                index.Add(key, value);
            }
        }
    }


    internal bool TryGetValue(string key, [MaybeNullWhen(false)] out TV value)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (index.TryGetValue(key, out var val))
            {
                value = val; return true;
            }
        }
        value = default;
        return false;
    }

    IEnumerator IEnumerable.GetEnumerator() => index.Values.GetEnumerator();

    public IEnumerator<TV> GetEnumerator() => index.Values.GetEnumerator();

    public override string ToString()
    {
        return $"{{ {string.Join(", ", from p in index select ($"[{p.Key}]= {p.Value}"))}}}";
    }
}