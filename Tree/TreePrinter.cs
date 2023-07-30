

record class TreePrinter<TNode>(TextWriter Writer, Func<TNode, string> getLabel, Func<TNode, IEnumerable<TNode>> getChildren)
{

    public void PrintNodes(params TNode[] nodes)
    {
        foreach (var (_, node, last) in nodes.WithFirstLast())
        {
            PrintNode(node, "", last);
        }
    }

    private void PrintNode(TNode node, string indent, bool isLast)
    {

        Writer.WriteLine("{0}{1} {2}", indent, isLast ? " └─" : " ├─", getLabel(node));
        indent += isLast ? "   " : " │ ";
        foreach (var (_, child, last) in getChildren(node).WithFirstLast())
        {
            PrintNode(child, indent, last);
        }
    }
}
