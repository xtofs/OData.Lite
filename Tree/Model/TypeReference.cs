namespace OData.Lite;

record struct TypeReference(string Ref) : IFromXElement<TypeReference>, IXmlLineInfo
{

    public static bool TryFromXElement(XElement element, out TypeReference typeReference)
    {
        var attr = element.Attribute("Type");
        if (attr == null)
        {
            typeReference = new("UnknownType") { Pos = element.LineInfo() }; ;
        }
        else
        {
            typeReference = new(attr.Value) { Pos = attr.LineInfo() }; ;
        }
        return true;
    }

    internal required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) IXmlLineInfo.Position => Pos;


    public override readonly string ToString()
    {
        return Ref;
    }
}
