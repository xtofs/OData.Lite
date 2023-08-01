namespace OData.Lite;

record class NavigationProperty(string Name, TypeReference Type) : IFromXElement<NavigationProperty>, IXmlLineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out NavigationProperty value)
    {
        if (element.Name != EDM + "NavigationProperty")
        {
            value = null; return false;
        }

        var pos = element.LineInfo();
        var name = element.Attribute("Name")?.Value ?? "";
        TypeReference.TryFromXElement(element, out var typeReference);

        value = new NavigationProperty(name, typeReference) { Pos = pos };
        return true;
    }

    internal required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) IXmlLineInfo.Position => Pos;
}

class NavigationPropertyCollection : KeyedCollection<string, NavigationProperty>
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