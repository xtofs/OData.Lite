using System.Collections.Immutable;

namespace OData.Lite;

public abstract record class Node(string Segment, string Type, IReadOnlyList<Node> Nodes)
{
    public Node(string Segment, string Type, params Node[] nodes) : this(Segment, Type, nodes.AsReadOnly()) { }

    internal void Debug(System.CodeDom.Compiler.IndentedTextWriter w)
    {
        w.WriteLine("{0}", this.Segment);
        w.Indent += 1;
        foreach (var node in this.Nodes)
        {
            node.Debug(w);
        }
        w.Indent -= 1;
    }

    internal IEnumerable<(ImmutableList<string>, string)> Flatten(ImmutableList<string> immutableList)
    {
        var prefix = immutableList.Add(this.Segment);
        yield return (prefix, this.Type);
        foreach (var node in Nodes)
        {
            foreach (var pathAndType in node.Flatten(prefix))
            {
                yield return pathAndType;
            }
        }
    }
}

public sealed record class PropertyNode(string Segment, string Type, IReadOnlyList<Node> Nodes) :
    Node(Segment, Type, Nodes)
{
    public PropertyNode(string segment, string type, params Node[] nodes) : this(segment, type, nodes.AsReadOnly()) { }
}

public sealed record class KeyNode(string KeyName, string KeyType, string Type, IReadOnlyList<Node> Nodes) :
     Node($"{{{KeyName}: {KeyType}}}", Type, Nodes)
{
    public KeyNode(string keyName, string keyType, string type, params Node[] nodes) : this(keyName, keyType, type, nodes.AsReadOnly()) { }
}
