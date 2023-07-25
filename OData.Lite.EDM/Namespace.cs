using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace OData.Lite.EDM;

public class Namespace<TV> : IEnumerable<TV>
{
    private readonly Func<TV, string>[] keys;
    private readonly Dictionary<string, TV>[] indices;

    public Namespace(params Func<TV, string>[] keys)
    {
        this.keys = keys;
        this.indices = new Dictionary<string, TV>[keys.Length];
        for (int i = 0; i < keys.Length; i++)
        {
            indices[i] = new();
        }
    }

    public void Add(TV value)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            var key = keys[i](value);
            if (!string.IsNullOrWhiteSpace(key))
            {
                indices[i].Add(key, value);
            }
        }

    }


    internal bool TryGetValue(string key, [MaybeNullWhen(false)] out TV value)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (indices[i].TryGetValue(key, out var val))
            {
                value = val; return true;
            }
        }
        value = default;
        return false;
    }

    IEnumerator IEnumerable.GetEnumerator() => indices[0].Values.GetEnumerator();

    public IEnumerator<TV> GetEnumerator() => indices[0].Values.GetEnumerator();
}