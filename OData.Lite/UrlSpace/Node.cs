using System.Collections.Immutable;

namespace OData.Lite;

public abstract record class Node(string Label, string Type, IReadOnlyList<Node> Nodes)
{
    public Node(string Label, string Type, params Node[] nodes) : this(Label, Type, nodes.AsReadOnly()) { }

    internal void Debug(System.CodeDom.Compiler.IndentedTextWriter w)
    {
        w.WriteLine("{0}", this.Label);
        w.Indent += 1;
        foreach (var node in this.Nodes)
        {
            node.Debug(w);
        }
        w.Indent -= 1;
    }

    internal abstract IEnumerable<ImmutableList<Segment>> Paths(ImmutableList<Segment> prefix, ImmutableDictionary<string, uint> keyNameCount);
}

public sealed record class PropertyNode(string Segment, string Type, IReadOnlyList<Node> Nodes) :
    Node(Segment, Type, Nodes)
{
    public PropertyNode(string segment, string type, params Node[] nodes) : this(segment, type, nodes.AsReadOnly()) { }

    internal override IEnumerable<ImmutableList<Segment>> Paths(ImmutableList<Segment> prefix, ImmutableDictionary<string, uint> keyNameCount)
    {
        prefix = prefix.Add(new PropertySegment(this.Label, this.Type));

        yield return prefix;

        foreach (var node in Nodes)
        {
            foreach (var path in node.Paths(prefix, keyNameCount))
            {
                yield return path;
            }
        }
    }
}

public sealed record class KeyNode(string KeyName, string KeyType, string Type, IReadOnlyList<Node> Nodes) :
     Node($"{{{KeyName}}}", Type, Nodes)
{
    public KeyNode(string keyName, string keyType, string type, params Node[] nodes) : this(keyName, keyType, type, nodes.AsReadOnly()) { }

    internal override IEnumerable<ImmutableList<Segment>> Paths(ImmutableList<Segment> prefix, ImmutableDictionary<string, uint> keyNameCount)
    {

        if (keyNameCount.TryGetValue(this.KeyName, out var c))
        {
            keyNameCount = keyNameCount.Remove(this.KeyName).Add(this.KeyName, c + 1);
            // TODO: ensure that this string intrerpolation is in sync with the KeyNode's constructor
            prefix = prefix.Add(new KeySegment($"{{{this.KeyName + c}}}", this.Type, this.KeyName, this.KeyType));
        }
        else
        {
            keyNameCount = keyNameCount.Add(this.KeyName, 1);
            prefix = prefix.Add(new KeySegment(this.Label, this.Type, this.KeyName, this.KeyType));
        }

        yield return prefix;

        foreach (var node in Nodes)
        {
            foreach (var path in node.Paths(prefix, keyNameCount))
            {
                yield return path;
            }
        }
    }
}
