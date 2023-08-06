

namespace OData.Lite;

public record class UrlSpace(IReadOnlyList<Node> Nodes)
{
    public static UrlSpace From(Model model, Schema schema, int maxKeys = 2)
    {
        return new UrlSpace(FromSchema(model, schema, maxKeys).ToList());
    }

    private static IEnumerable<Node> FromSchema(Model model, Schema schema, int maxKeys)
    {
        foreach (var containerElement in schema.Container.Elements)
        {
            if (containerElement is EntitySet entitySet)
            {
                yield return FromEntitySet(model, entitySet, maxKeys);
            }
            else if (containerElement is Singleton singleton)
            {
                yield return FromSingleton(model, singleton, maxKeys);
            }
            else
            {
                throw new NotImplementedException($"unkown container element {containerElement}");
            }
        }
    }

    private static string Collection(TypeReference type) => $"[{type.FQN}]";

    private static Node FromEntitySet(Model model, EntitySet entitySet, int maxKeys)
    {
        if (model.TryResolve<EntityType>(entitySet.EntityType, out var entityType))
        {

            return new PropertyNode(entitySet.Name, Collection(entitySet.EntityType), FromEntityKey(model, entityType, maxKeys));
        }
        else
        {
            System.Console.WriteLine("unable to resolve type {0} of {1}", entitySet.EntityType, entitySet);
            return new PropertyNode(entitySet.Name, $"unkown type {entitySet.EntityType}");
        }
    }

    private static Node FromSingleton(Model model, Singleton singleton, int maxKeys)
    {
        if (model.TryResolve<EntityType>(singleton.Type, out var entityType))
        {
            var nodes = FromStructuralType(model, entityType.NavigationProperties, entityType.Properties, maxKeys);
            return new PropertyNode(singleton.Name, singleton.Type.FQN, nodes);
        }
        else
        {
            System.Console.WriteLine("can't resolve type {0} of singleton {1}", singleton.Type, singleton);
            return new PropertyNode(singleton.Name, $"unkown type {singleton.Type}");
        }
    }

    private static Node FromEntityKey(Model model, EntityType entityType, int maxKeys)
    {
        var keys = entityType.Keys;
        var key = keys[0]; // TODO  error handling for composite keys
        var prop = entityType.Properties.Single(p => p.Name == key.Name);
        return new KeyNode(prop.Name, prop.Type.FQN, entityType.Name,
            entityType.NavigationProperties.Select(p => FromProperty(model, p, maxKeys - 1)).ToList());
    }

    private static IEnumerable<Node> FromComplexProperties(Model model, PropertyCollection properties, int maxKeys)
    {
        foreach (var property in properties)
        {
            if (model.TryResolve<ComplexType>(property.Type, out var complexType))
            {
                yield return new PropertyNode(property.Name, property.Type.FQN, FromComplexType(model, complexType, maxKeys));
            }
        }
    }

    private static Node FromProperty(Model model, NavigationProperty property, int maxKeys)
    {
        if (property.Type.IsCollection(out var collectionItemTypeRef))
        {
            if (model.TryResolve<EntityType>(collectionItemTypeRef, out var collectionItemType))
            {
                return new PropertyNode(property.Name, Collection(collectionItemTypeRef), FromEntityKey(model, collectionItemType, maxKeys));
            }
            return new PropertyNode(property.Name, $": Unkown<{property.Type.FQN}>", Array.Empty<Node>());
        }
        if (model.TryResolve<EntityType>(property.Type, out var entityType))
        {
            return new PropertyNode(property.Name,
            property.Type.FQN,
            FromStructuralType(model, entityType.NavigationProperties, entityType.Properties, maxKeys));
        }
        return new PropertyNode(
            property.Name,
            $"unknown {property.Type.FQN}",
            Array.Empty<Node>());
    }

    private static IReadOnlyList<Node> FromComplexType(Model model, ComplexType complexType, int maxKeys)
    {
        return FromStructuralType(model, complexType.NavigationProperties, complexType.Properties, maxKeys);
    }

    private static IReadOnlyList<Node> FromStructuralType(Model model, NavigationPropertyCollection navigationProperties, PropertyCollection properties, int maxKeys)
    {
        if (maxKeys <= 0)
        {
            return Array.Empty<Node>();
        }
        var nav = navigationProperties.Select(p => FromProperty(model, p, maxKeys));
        // TODO: replace Where with check of complext type
        var com = FromComplexProperties(model, properties, maxKeys);
        return (nav.Concat(com)).ToList();
    }

    public void Display(TextWriter @out)
    {
        var w = new TreePrinter<Node>(@out, n => $"{n.Label}: {n.Type}", n => n.Nodes);
        w.PrintNodes(this.Nodes);
    }

    public override string ToString()
    {
        using var w = new StringWriter();
        Display(w);
        return w.ToString();
    }

    public IEnumerable<ImmutableList<Segment>> Paths()
    {
        return Nodes.SelectMany(n => n.Paths(ImmutableList<Segment>.Empty, ImmutableDictionary<string,uint>.Empty));
    }
}
