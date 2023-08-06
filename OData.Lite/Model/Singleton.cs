namespace OData.Lite;

// https://docs.oasis-open.org/odata/odata-csdl-xml/v4.01/odata-csdl-xml-v4.01.html#sec_Singleton
public record class Singleton(string Name, TypeReference Type) : ContainerElement(Name), IFromXElement<Singleton>, ILineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out Singleton value)
    {
        if (element.Name != EDM + "Singleton")
        {
            value = null; return false;
        }

        var pos = element.LineInfo();
        var name = element.Attribute("Name")?.Value ?? "";
        var type = element.Attribute("Type")?.Value ?? "";
        // IncludeInServiceDocument

        value = new Singleton(name, new TypeReference(type)) { Pos = pos };
        return true;
    }

    internal (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;
}