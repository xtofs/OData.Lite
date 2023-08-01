using System.Text.RegularExpressions;

namespace OData.Lite;

public record class UrlSpace(IReadOnlyList<Node> Nodes)
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
        if (property.TypeFQN.StartsWith("Collection"))
        {
            var match = Regex.Match("Collection\\(([a-z.]+)\\)", property.TypeFQN);
            if (match.Success)
            {
                var t = match.Groups["1"].Value;
                if (model.TryResolve<EntityType>(t, out var elementType))
                {
                    return new Node(property.Name, FromEntityKey(model, elementType));
                }
            }
        }
        return new Node(property.Name, Array.Empty<Node>());
    }

    public void Display(TextWriter @out)
    {
        var w = new TreePrinter<Node>(@out, n => n.Segment, n => n.Nodes);
        w.PrintNodes(this.Nodes);
    }
}
