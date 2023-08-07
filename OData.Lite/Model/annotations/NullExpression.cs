namespace OData.Lite;

public sealed record class NullExpression() :
    AnnotationExpression(), IFromXElement<NullExpression>, ILineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out NullExpression value)
    {
        var e = element.Element("Null");
        if (e != null)
        {
            value = new NullExpression() { Pos = e.LineInfo() };
            return true;
        }
        value = default;
        return false;
    }
}
