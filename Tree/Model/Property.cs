namespace OData.Lite;

record class Property(string Name, TypeReference Type) : IFromXElement<Property>, IXmlLineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out Property value)
    {
        if (element.Name != EDM + "Property")
        {
            value = null; return false;
        }
        var pos = element.LineInfo();
        string name = element.Attribute("Name")?.Value ?? "";
        TypeReference.TryFromXElement(element, out var typeReference);

        value = new Property(name, typeReference) { Pos = pos };
        return true;
    }

    internal required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) IXmlLineInfo.Position => Pos;
}


class PropertyCollection : KeyedCollection<string, Property>
{
    public PropertyCollection()
    { }

    public PropertyCollection(IEnumerable<Property> members)
    {
        foreach (var member in members) { Add(member); }
    }

    protected override string GetKeyForItem(Property item) => item.Name;

    public override string ToString()
    {
        return "[" + string.Join(", ", this) + "]";
    }

    internal static PropertyCollection FromXElements(IEnumerable<XElement> elements)
    {
        var self = new PropertyCollection();
        foreach (var element in elements)
        {
            if (Property.TryFromXElement(element, out var item))
            {
                self.Add(item);
            }
        }
        return self;
    }
}