using System.Xml.Linq;

namespace OData.Lite;

public record class Schema(string Namespace, string Alias, SchemaElementCollection Elements, EntityContainer Container) : IFromXElement<Schema>, ILineInfo
{
    internal (int, int) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;


    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out Schema value)
    {
        if (element.Name != EDM + "Schema")
        {
            value = null; return false;
        }
        var pos = element.LineInfo();
        string @namespace = element.Attribute("Namespace")?.Value ?? "";
        string alias = element.Attribute("Alias")?.Value ?? "";

        var schemaElements = SchemaElementCollection.FromXElements(element.Elements());
        // TODO: check for error: missing EntityContainer
        EntityContainer.TryFromXElement(element.Element(EDM + "EntityContainer")!, out var container);
        value = new Schema(@namespace, alias, schemaElements, container!) { Pos = pos };
        return true;
    }

}
