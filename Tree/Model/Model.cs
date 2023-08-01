namespace OData.Lite;


record class Model(string Version, SchemaCollection Schemas) : IFromXElement<Model>, IXmlLineInfo
{
    (int LineNumber, int LinePosition) IXmlLineInfo.Position => Pos;

    internal required (int, int) Pos { get; init; }

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
        // var schemas = ds.Elements(EDM + "Schema").SelectWhere<XElement, Schema>(Schema.TryFromXElement);
        var schemas = SchemaCollection.FromXElements(ds.Elements());

        value = new Model(version, new SchemaCollection(schemas)) { Pos = pos };
        return true;
    }

    public static bool Load(string filename, [MaybeNullWhen(false)] out Model model)
    {
        var doc = XDocument.Load(filename, LoadOptions.SetLineInfo);
        return Model.TryFromXElement(doc.Root!, out model);
    }
}
