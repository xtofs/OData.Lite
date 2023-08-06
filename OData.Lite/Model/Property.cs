namespace OData.Lite;

public record class Property(string Name, bool Nullable, TypeReference Type) : IFromXElement<Property>, ILineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out Property value)
    {
        if (element.Name != EDM + "Property")
        {
            value = null; return false;
        }
        var pos = element.LineInfo();
        string name = element.Attribute("Name")?.Value ?? "";
        var nullable = NullableAttr.FromXElement(element);
        TypeReference.TryFromXElement(element, out var typeReference);

        value = new Property(name, nullable, typeReference) { Pos = pos };
        return true;
    }

    internal (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;
}


public class PropertyCollection : KeyedCollection<string, Property>
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