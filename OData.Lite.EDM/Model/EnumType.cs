namespace OData.Lite.EDM.Model;


public record class EnumMember(string Name) : INamedElement
{ }

public sealed record EnumType(string Name) : Container<EnumMember>, ISchemaElement
{
    public SchemaElementKind Kind { get; } = SchemaElementKind.EnumType;

    public string Name { get; } = Name;

    public IEnumerable<EnumMember> Members => this.Elements;
}


