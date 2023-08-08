namespace OData.Lite;

public sealed record class NullExpression() :
    AnnotationExpression(), IFromXElement<NullExpression>, ILineInfo
{

    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out NullExpression value)
    {
        if (element != null && element.Name.LocalName == "Null")
        {
            value = new NullExpression() { Pos = element.LineInfo() };
            return true;
        }
        value = default;
        return false;
    }
}
