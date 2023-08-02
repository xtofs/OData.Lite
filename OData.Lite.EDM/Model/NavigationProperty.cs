namespace OData.Lite;

public record class NavigationProperty(string Name, TypeReference Type, bool Nullable) : IFromXElement<NavigationProperty>, IXmlLineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out NavigationProperty value)
    {
        if (element.Name != EDM + "NavigationProperty")
        {
            value = null; return false;
        }

        var pos = element.LineInfo();
        var name = element.Attribute("Name")?.Value ?? "";
        var nullable = NullableAttr.FromXElement(element);
        var partner = element.Attribute("Partner")?.Value ?? "";
        TypeReference.TryFromXElement(element, out var typeReference);

        value = new NavigationProperty(name, typeReference, nullable) { Pos = pos };
        return true;
    }

    internal (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) IXmlLineInfo.LineInfo => Pos;
}

public class NavigationPropertyCollection : KeyedCollection<string, NavigationProperty>
{
    public NavigationPropertyCollection()
    { }

    public NavigationPropertyCollection(IEnumerable<NavigationProperty> members)
    {
        foreach (var member in members) { Add(member); }
    }

    protected override string GetKeyForItem(NavigationProperty item) => item.Name;

    public override string ToString()
    {
        return "[" + string.Join(", ", this) + "]";
    }

    internal static NavigationPropertyCollection FromXElements(IEnumerable<XElement> elements)
    {
        var self = new NavigationPropertyCollection();
        foreach (var element in elements)
        {
            if (NavigationProperty.TryFromXElement(element, out var item))
            {
                self.Add(item);
            }
        }
        return self;
    }
}

static class NullableAttr
{
    public static bool FromXElement(XElement element)
    {
#pragma warning disable IDE0075
        return bool.TryParse(element.Attribute("Nullable")?.Value, out var v) ? v : true;
#pragma warning restore IDE0075
    }
}