namespace OData.Lite;

public record class EnumType(string Name, EnumMemberCollection Members) : SchemaElement(Name), IFromXElement<EnumType>, ILineInfo
{
    public required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;

    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out EnumType value)
    {
        var pos = element.LineInfo();
        string name = element.Attribute("Name")?.Value ?? "";

        var members = element.Elements().FromXElements<EnumMember>();

        value = new EnumType(name, new EnumMemberCollection(members)) { Pos = pos };
        return true;
    }
}

public class EnumMemberCollection : KeyedCollection<string, EnumMember>
{
    public EnumMemberCollection()
    { }

    public EnumMemberCollection(IEnumerable<EnumMember> members)
    {
        foreach (var member in members) { Add(member); }
    }

    protected override string GetKeyForItem(EnumMember item) => item.Name;

    public override string ToString()
    {
        return "[" + string.Join(", ", this) + "]";
    }
}

public record class EnumMember(string Name) : IFromXElement<EnumMember>, ILineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out EnumMember value)
    {
        var pos = element.LineInfo();
        string name = element.Attribute("Name")?.Value ?? "";
        value = new EnumMember(name) { Pos = pos };
        return true;
    }

    public required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;
}
