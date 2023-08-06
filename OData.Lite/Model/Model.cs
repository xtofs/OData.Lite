namespace OData.Lite;


public record class Model(string Version, SchemaCollection Schemas) : IFromXElement<Model>, IXmlLineInfo
{
    (int LineNumber, int LinePosition) IXmlLineInfo.LineInfo => Pos;

    internal (int, int) Pos { get; init; }

    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out Model value)
    {
        var pos = element.LineInfo();
        string version = element.Attribute("Version")?.Value ?? "";

        var ds = element.Element(EDMX + "DataServices");
        if (ds == null)
        {
            value = null;
            return false;
        }
        var schemas = ds.Elements().FromXElements<Schema>();

        value = new Model(version, new SchemaCollection(schemas)) { Pos = pos };
        return true;
    }

    public static bool TryLoad(string filename, [MaybeNullWhen(false)] out Model model)
    {
        var doc = XDocument.Load(filename, LoadOptions.SetLineInfo);
        return Model.TryFromXElement(doc.Root!, out model);
    }

    public static bool TryLoad(TextReader file, [MaybeNullWhen(false)] out Model model)
    {
        var doc = XDocument.Load(file, LoadOptions.SetLineInfo);
        return Model.TryFromXElement(doc.Root!, out model);
    }

    internal bool TryResolve<T>(TypeReference typeRef, [MaybeNullWhen(false)] out T value) where T : SchemaElement
    {
        if (typeRef.TrySplit(out var nameSpace, out var localName))
        {
            value = default; // 
            return this.Schemas.TryFind(nameSpace, out var schema)
                && schema.Elements.TryFind<T>(localName, out value);
        }
        else
        {
            value = default;
            return false;
        }
    }
}
