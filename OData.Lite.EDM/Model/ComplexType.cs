using System.Data;

namespace OData.Lite;

record class ComplexType(string Name, PropertyCollection Properties, NavigationPropertyCollection NavigationProperties)
 : SchemaElement(Name), IFromXElement<ComplexType>, IXmlLineInfo
{

    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out ComplexType value)
    {
        var pos = element.LineInfo();
        string name = element.Attribute("Name")?.Value ?? "";

        var properties = PropertyCollection.FromXElements(element.Elements());
        var navigationProperties = NavigationPropertyCollection.FromXElements(element.Elements());

        value = new ComplexType(name, properties, navigationProperties) { Pos = pos };
        return true;
    }

    internal required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) IXmlLineInfo.LineInfo => Pos;
}
