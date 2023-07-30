
var trees = new[] {
    new Node("A", new Node("C"), new Node("B", new Node("B"), new Node("C"))),
     new Node("D")
};
var w = new TreePrinter<Node>(Console.Out, n => n.Label, n => n.Children);
w.PrintNodes(trees);

record class Node(string Label, params Node[] Children) { }
