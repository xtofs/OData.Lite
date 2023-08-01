using System.Xml.Linq;

namespace OData.Lite;

record class Schema(string Namespace, string Alias, SchemaElementCollection Elements) : IFromXElement<Schema>, IXmlLineInfo
{
    internal required (int, int) Pos { get; init; }

    (int LineNumber, int LinePosition) IXmlLineInfo.Position => Pos;


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

        value = new Schema(@namespace, alias, schemaElements) { Pos = pos };
        return true;
    }

    // private static readonly HashSet<XName> SchemaElementNames = new() { EDM + "EnumType", EDM + "ComplexType", EDM + "EntityType" };
}


class SchemaCollection : KeyedCollection<string, Schema>
{
    public SchemaCollection()
    { }

    public SchemaCollection(IEnumerable<Schema> schemas)
    {
        foreach (var schema in schemas) { Add(schema); }
    }

    protected override string GetKeyForItem(Schema item) => item.Namespace;

    public override string ToString()
    {
        return "[" + string.Join(", ", this) + "]";
    }

    internal static SchemaCollection FromXElements(IEnumerable<XElement> elements)
    {
        var self = new SchemaCollection();
        foreach (var element in elements)
        {
            if (Schema.TryFromXElement(element, out var item))
            {
                self.Add(item);
            }
        }
        return self;
    }
}

abstract record class SchemaElement(string Name) // : IFromXElement<SchemaElement>
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out SchemaElement value)
    {
        switch (element.Name.LocalName)
        {
            case "EnumType":
                var res = EnumType.TryFromXElement(element, out var enumType);
                value = enumType;
                return res;
            case "EntityType":
                res = EntityType.TryFromXElement(element, out var entityType);
                value = entityType;
                return res; ;
            case "ComplexType":
                res = ComplexType.TryFromXElement(element, out var complexType);
                value = complexType;
                return res; ;
            default:
                value = null;
                return false;
        }
    }
}

class SchemaElementCollection : KeyedCollection<string, SchemaElement>
{
    public SchemaElementCollection() { }

    public SchemaElementCollection(IEnumerable<SchemaElement> elements)
    {
        foreach (var element in elements) { Add(element); }
    }
    protected override string GetKeyForItem(SchemaElement item) => item.Name;

    public override string ToString()
    {
        return "[" + string.Join(", ", this) + "]";
    }

    internal static SchemaElementCollection FromXElements(IEnumerable<XElement> elements)
    {
        // var schemaElements = element.Elements()
        //     .Where(element => SchemaElementNames.Contains(element.Name))
        //     .SelectWhere<XElement, SchemaElement>(SchemaElement.TryFromXElement);

        var self = new SchemaElementCollection();
        foreach (var element in elements)
        {
            if (SchemaElement.TryFromXElement(element, out var item))
            {
                self.Add(item);
            }
        }
        return self;
    }
}
