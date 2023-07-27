namespace OData.Lite.EDM;

public sealed record class TypeRef(string Name) { }

public sealed record class Property(string Name, TypeRef Type)
{
#pragma warning disable IDE0051
    private bool PrintMembers(StringBuilder builder)
    {
        builder.AppendFormat("Name = {0}, ", Name);
        builder.AppendFormat("Type = {0}", Type.Name);
        return true;
    }
#pragma warning restore IDE0051
}

public sealed record ComplexType(string Name) : Container<Property>(p => p.Name), ISchemaElement
{
    public SchemaElementKind Kind { get; } = SchemaElementKind.ComplexType;

    public IEnumerable<Property> Properties => base.Values;

    public bool TryFindProperty(string name, [MaybeNullWhen(false)] out Property property)
    {
        return base.TryFindItem(name, out property);
    }

    protected override bool PrintMembers(StringBuilder builder)
    {
        builder.AppendFormat("Name = {0}, ", Name);
        builder.AppendFormat("Properties = [ ");
        builder.AppendJoin(", ", Properties);
        builder.AppendFormat("]");
        return true;
    }
}
