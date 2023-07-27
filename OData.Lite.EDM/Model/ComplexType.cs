namespace OData.Lite.EDM.Model;

public sealed record class TypeRef(string Name) { }

public record class Property(string Name, TypeRef Type)
{ }


public sealed record ComplexType(string Name) : Container<Property>(p => p.Name), ISchemaElement
{
    public SchemaElementKind Kind { get; } = SchemaElementKind.ComplexType;

    public IEnumerable<Property> Properties => base.Values;

    public bool TryFindProperty(string name, [MaybeNullWhen(false)] out Property property)
    {
        return base.TryFindItem(name, out property);
    }

}
