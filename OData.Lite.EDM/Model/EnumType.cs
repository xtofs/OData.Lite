namespace OData.Lite.EDM.Model;


public record class EnumMember(string Name)
{ }

public sealed record EnumType(string Name) : Container<EnumMember>(m => m.Name), ISchemaElement
{
    public SchemaElementKind Kind { get; } = SchemaElementKind.EnumType;

    public string Name { get; } = Name;

    public IEnumerable<EnumMember> Members => this.Values;

    public bool TryFindMember(string name, [MaybeNullWhen(false)] out EnumMember member)
    {
        return base.TryFindItem(name, out member);
    }



    protected override bool PrintMembers(StringBuilder builder)
    {
        // base.PrintMembers(builder);
        builder.AppendFormat("Name = {0}, ", Name);
        builder.AppendFormat("Members = [ {0} ]", string.Join(", ", Members));
        return true;
    }
}


