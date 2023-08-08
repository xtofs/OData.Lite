using System.Xml.Linq;

namespace OData.Lite;

// https://docs.oasis-open.org/odata/odata-csdl-xml/v4.01/odata-csdl-xml-v4.01.html#sec_Annotation
record class Annotation(string Term, string? Qualifier, Expression Expression) : IFromXElement<Annotation>, ILineInfo
{
    internal required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;

    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out Annotation value)
    {
        var pos = element.LineInfo();
        string name = element.Attribute("Name")?.Value ?? "";

        var members = element.Elements().FromXElements<EnumMember>();

        value = new EnumType(name, new EnumMemberCollection(members)) { Pos = pos };
        return true;
    }
}

// https://docs.oasis-open.org/odata/odata-csdl-xml/v4.01/odata-csdl-xml-v4.01.html#sec_ConstantExpression
abstract record class Expression() : IFromXElement<Expression>, ILineInfo
{
    internal required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;

    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out Expression value)
    {
        var pos = element.LineInfo();
        string name = element.Attribute("Property")?.Value ?? "";
        Expression.TryFromXElement(element, out var value);
        return new PropertyValue(name, value);
    }
}

// https://docs.oasis-open.org/odata/odata-csdl-xml/v4.01/odata-csdl-xml-v4.01.html#sec_Integer
record class IntExpression(int Value) : Expression(), IFromXElement<Expression> { }

// https://docs.oasis-open.org/odata/odata-csdl-xml/v4.01/odata-csdl-xml-v4.01.html#sec_Record
record class RecordExpression(string Type) : Expression(), IFromXElement<Expression>
{
    private readonly Dictionary<string, Expression> propertyValues = new();

    public IReadOnlyDictionary<string, Expression> PropertyValues => propertyValues;
}


record class PropertyValue(string Property, Expression Value) : IFromXElement<PropertyValue>
{

    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out PropertyValue value)
    {
        var pos = element.LineInfo();
        string name = element.Attribute("Property")?.Value ?? "";
        Expression.TryFromXElement(element, out var val);

        value = new PropertyValue(name, val);
        return true;
    }

}