namespace OData.Lite;

public sealed record class BoolExpression(bool Value) :
    AnnotationExpression(), IFromXElement<BoolExpression>, ILineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out BoolExpression value)
    {
        var a = element.Attribute("Bool");
        if (a != null && bool.TryParse(a.Value, out var val))
        {
            value = new BoolExpression(val) { Pos = a.LineInfo() };
            return true;
        }
        var e = element.Element("Bool");
        if (e != null && bool.TryParse(e.Value, out val))
        {
            value = new BoolExpression(val) { Pos = e.LineInfo() };
            return true;
        }
        value = default;
        return false;
    }

}