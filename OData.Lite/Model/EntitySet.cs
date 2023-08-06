namespace OData.Lite;

// https://docs.oasis-open.org/odata/odata-csdl-xml/v4.01/odata-csdl-xml-v4.01.html#sec_EntitySet
public record class EntitySet(string Name, TypeReference EntityType) : ContainerElement(Name), IFromXElement<EntitySet>, ILineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out EntitySet value)
    {
        if (element.Name != EDM + "EntitySet")
        {
            value = null; return false;
        }

        var pos = element.LineInfo();
        var name = element.Attribute("Name")?.Value ?? "";
        var type = element.Attribute("EntityType")?.Value ?? "";
        // IncludeInServiceDocument

        value = new EntitySet(name, new TypeReference(type)) { Pos = pos };
        return true;
    }

    internal (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;
}
