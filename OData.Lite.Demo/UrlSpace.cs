using System.Resources;
using OData.Lite;

record class UrlSpace(IReadOnlyList<Node> Nodes)
{
    public static UrlSpace From(Model model, Schema schema)
    {
        return new UrlSpace(FromSchema(model, schema).ToList());
    }

    private static IEnumerable<Node> FromSchema(Model model, Schema schema)
    {
        foreach (var containerElement in schema.EntityContainer.Elements)
        {
            if (containerElement is EntitySet entitySet)
            {
                yield return FromEntitySet(model, entitySet);
            }
            else if (containerElement is Singleton singleton)
            {
                yield return FromSingleton(model, singleton);
            }
            else
            {
                throw new NotImplementedException($"unkown container element {containerElement}");
            }
        }
    }

    private static Node FromEntitySet(Model model, EntitySet entitySet)
    {
        if (model.TryResolve<EntityType>(entitySet.EntityType, out var entityType))
        {

            return new Node(entitySet.Name, FromEntityKey(model, entityType));
        }
        else
        {
            System.Console.WriteLine("can't resolve type {0} of entitySet {1}", entitySet.EntityType, entitySet);
            return new Node($"{entitySet.Name}: unkown type {entitySet.EntityType}"); ; ;
        }
    }

    private static Node FromSingleton(Model model, Singleton singleton)
    {
        if (model.TryResolve<EntityType>(singleton.EntityType, out var entityType))
        {
            return new Node(singleton.Name, entityType.NavigationProperties.Select(p => FromProperty(model, p)).ToList());
        }
        else
        {
            System.Console.WriteLine("can't resolve type {0} of singleton {1}", singleton.EntityType, singleton);
            return new Node($"{singleton.Name}: unkown type {singleton.EntityType}"); ; ;
        }
    }


    private static Node FromEntityKey(Model model, EntityType entityType)
    {
        var key = entityType.Key;
        var prop = entityType.Properties.Single(p => p.Name == key.PropertyRefs.ElementAt(0).Name);
        return new Node($"{{{entityType.Name}.{prop.Name}: {prop.TypeFQN}}}", entityType.NavigationProperties.Select(p => FromProperty(model, p)).ToList());
    }

    private static Node FromProperty(Model model, NavigationProperty property)
    {
        return new Node(property.Name, Array.Empty<Node>());
    }

    public void Display(TextWriter @out)
    {
        var w = new TreePrinter<Node>(@out, n => n.Segment, n => n.Nodes);
        w.PrintNodes(this.Nodes);
        // var w = new System.CodeDom.Compiler.IndentedTextWriter(@out);
        // foreach (var node in Nodes)
        // {
        //     node.Display(w, full);
        // }
    }
}

record class Node(string Segment, IReadOnlyList<Node> Nodes)
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