using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace OData.Lite.EDM;

public class Namespace<TV> : IEnumerable<TV>
{
    private readonly Func<TV, string> primary;
    private readonly Func<TV, string?>[] secondaries;
    private readonly Dictionary<string, TV> index;

    public Namespace(Func<TV, string> primary, params Func<TV, string?>[] secondaries)
    {
        this.primary = primary;
        this.secondaries = secondaries;
        this.index = new Dictionary<string, TV>();
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