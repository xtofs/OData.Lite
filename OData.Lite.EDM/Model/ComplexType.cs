namespace OData.Lite.EDM.Model;

public sealed record class TypeRef(string Name) { }

public record class Property(string Name, TypeRef Type) : INamedItem
{ }


public sealed record ComplexType(string Name) : Container<Property>, ISchemaElement
{
    public SchemaElementKind Kind { get; } = SchemaElementKind.ComplexType;

    public string Name { get; } = Name;

    public IEnumerable<Property> Properties => this.Items;

    public bool TryFindProperty(string name, [MaybeNullWhen(false)] out Property property) =>
        TryFindItem<Property>(name, out property);
}
