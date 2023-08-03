using System.Collections.Immutable;

namespace OData.Lite;

public record class Node(string Segment, string Type, IReadOnlyList<Node> Nodes)
{
    public Node(string Segment, string Type, params Node[] nodes) : this(Segment, Type, nodes.AsReadOnly()) { }

    internal void Display(System.CodeDom.Compiler.IndentedTextWriter w, bool full)
    {
        w.WriteLine("{0}", this.Segment /*, full ? this.Type.FullTypeName() : this.Type.Name()*/ );
        w.Indent += 1;
        foreach (var node in this.Nodes)
        {
            node.Display(w, full);
        }
        w.Indent -= 1;
    }

    internal IEnumerable<ImmutableList<string>> Flatten(ImmutableList<string> immutableList)
    {
        var lst = immutableList.Add(this.Segment);
        yield return lst;
        foreach (var node in Nodes)
        {
            foreach (var path in node.Flatten(lst))
            {
                yield return path;
            }
        }
    }
}