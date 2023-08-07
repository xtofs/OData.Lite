namespace OData.Lite;

public sealed record class IntExpression(int Value) :
    AnnotationExpression(), IFromXElement<IntExpression>, ILineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out IntExpression value)
    {
        var a = element.Attribute("Int");
        if (a != null && int.TryParse(a.Value, out var val))
        {
            value = new IntExpression(val) { Pos = a.LineInfo() };
            return true;
        }
        var e = element.Element("Int");
        if (e != null && int.TryParse(e.Value, out val))
        {
            value = new IntExpression(val) { Pos = e.LineInfo() };
            return true;
        }
        value = default;
        return false;
    }

}
