namespace OData.Lite;

// https://docs.oasis-open.org/odata/odata-csdl-xml/v4.01/odata-csdl-xml-v4.01.html#sec_EntityType
record class EntityType(string Name, PropertyRefCollection Keys, PropertyCollection Properties, NavigationPropertyCollection NavigationProperties) : SchemaElement(Name), IFromXElement<EntityType>, ILineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out EntityType value)
    {
        if (element.Name != EDM + "EntityType")
        {
            value = default;
            return false;
        }
        var pos = element.LineInfo();
        var name = element.Attribute("Name")?.Value ?? "";
        // N.B.: .Elements() get iterated twice
        var properties = PropertyCollection.FromXElements(element.Elements());
        var navigationProperties = NavigationPropertyCollection.FromXElements(element.Elements());
        var keyElement = element.Element(EDM + "Key");
        var keys = (keyElement != null)
            ? PropertyRefCollection.FromXElements(keyElement.Elements())
            : new PropertyRefCollection();
        value = new EntityType(name, keys, properties, navigationProperties) { Pos = pos };
        return true;
    }

    internal required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;
}



public class PropertyRefCollection : KeyedCollection<string, PropertyRef>
{
    public PropertyRefCollection()
    { }

    protected override string GetKeyForItem(PropertyRef item)
    {
        return item.Name;
    }

    internal static PropertyRefCollection FromXElements(IEnumerable<XElement> elements)
    {
        var self = new PropertyRefCollection();
        foreach (var element in elements)
        {
            if (PropertyRef.TryFromXElement(element, out var item))
            {
                self.Add(item);
            }
        }
        return self;
    }
}

public record class PropertyRef(string Name, string? Alias) : IFromXElement<PropertyRef>, ILineInfo
{
    public static bool TryFromXElement(XElement element, out PropertyRef propertyRef)
    {
        var name = element.Attribute("Name")?.Value ?? "";
        var alias = element.Attribute("Alias")?.Value;
        propertyRef = new PropertyRef(name, alias) { Pos = element.LineInfo() };
        return true;
    }

    internal (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;

}
