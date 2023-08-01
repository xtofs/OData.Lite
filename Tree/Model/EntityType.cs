namespace OData.Lite;

record class EntityType(string Name, PropertyCollection Properties, NavigationPropertyCollection NavigationProperties) : SchemaElement(Name), IFromXElement<EntityType>, IXmlLineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out EntityType value)
    {
        var pos = element.LineInfo();
        var name = element.Attribute("Name")?.Value ?? "";
        var properties = PropertyCollection.FromXElements(element.Elements());
        var navigationProperties = NavigationPropertyCollection.FromXElements(element.Elements());

        value = new EntityType(name, properties, navigationProperties) { Pos = pos };
        return true;
    }

    internal required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) IXmlLineInfo.Position => Pos;
}
