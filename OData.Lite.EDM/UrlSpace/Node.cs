namespace OData.Lite;

public record class Node(string Segment, IReadOnlyList<Node> Nodes)
{
    public Node(string Segment, params Node[] nodes) : this(Segment, nodes.AsReadOnly()) { }

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
}