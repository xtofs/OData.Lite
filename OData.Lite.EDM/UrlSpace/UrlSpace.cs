using System.Text.RegularExpressions;

namespace OData.Lite;

public record class UrlSpace(IReadOnlyList<Node> Nodes)
{
    public static UrlSpace From(Model model, Schema schema)
    {
        return new UrlSpace(FromSchema(model, 5, schema).ToList());
    }

    private static IEnumerable<Node> FromSchema(Model model, int depth, Schema schema)
    {
        foreach (var containerElement in schema.Container.Elements)
        {
            if (containerElement is EntitySet entitySet)
            {
                yield return FromEntitySet(model, depth, entitySet);
            }
            else if (containerElement is Singleton singleton)
            {
                yield return FromSingleton(model, depth, singleton);
            }
            else
            {
                throw new NotImplementedException($"unkown container element {containerElement}");
            }
        }
    }

    private static Node FromEntitySet(Model model, int depth, EntitySet entitySet)
    {
        if (model.TryResolve<EntityType>(entitySet.EntityType, out var entityType))
        {

            return new Node(entitySet.Name, FromEntityKey(model, depth - 1, entityType));
        }
        else
        {
            System.Console.WriteLine("unable to resolve type {0} of {1}", entitySet.EntityType, entitySet);
            return new Node($"{entitySet.Name}: unkown type {entitySet.EntityType}"); ; ;
        }
    }

    private static Node FromSingleton(Model model, int depth, Singleton singleton)
    {
        if (model.TryResolve<EntityType>(singleton.Type, out var entityType))
        {
            // return new Node(singleton.Name, entityType.NavigationProperties.Select(p => FromProperty(model, p)).ToList());
            return new Node(singleton.Name, FromStructuralType(model, depth - 1, entityType.NavigationProperties));
        }
        else
        {
            System.Console.WriteLine("can't resolve type {0} of singleton {1}", singleton.Type, singleton);
            return new Node($"{singleton.Name}: unkown type {singleton.Type}"); ; ;
        }
    }

    private static Node FromEntityKey(Model model, int depth, EntityType entityType)
    {
        var keys = entityType.Keys;
        var key = keys[0]; // TODO error if multiple keys
        var prop = entityType.Properties.Single(p => p.Name == key.Name);
        return new Node($"{{{entityType.Name}.{prop.Name}: {prop.Type.FQN}}}",
            entityType.NavigationProperties.Select(p => FromProperty(model, depth - 1, p)).ToList());
    }

    private static Node FromProperty(Model model, int depth, NavigationProperty property)
    {
        if (property.Type.IsCollection(out var collectionItemTypeRef))
        {
            if (model.TryResolve<EntityType>(collectionItemTypeRef, out var collectionItemType))
            {
                return new Node(property.Name, FromEntityKey(model, depth - 1, collectionItemType));
            }
            return new Node(property.Name + $": Unkown<{property.Type}>", Array.Empty<Node>());
        }
        if (model.TryResolve<EntityType>(property.Type, out var entityType))
        {
            return new Node(property.Name, FromStructuralType(model, depth - 1, entityType.NavigationProperties));
        }
        return new Node(property.Name + $": {property.Type} ?", Array.Empty<Node>());
    }

    private static IReadOnlyList<Node> FromStructuralType(Model model, int depth, NavigationPropertyCollection NavigationProperties)
    {
        if (depth <= 0)
        {
            return Array.Empty<Node>();
        }
        return NavigationProperties.Select(p => FromProperty(model, depth - 1, p)).ToList();
    }

    public void Display(TextWriter @out)
    {
        var w = new TreePrinter<Node>(@out, n => n.Segment, n => n.Nodes);
        w.PrintNodes(this.Nodes);
    }
}
