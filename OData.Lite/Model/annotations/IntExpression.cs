namespace OData.Lite;

public sealed record class IntExpression(int Value) :
    AnnotationExpression(), IFromXElement<IntExpression>, IFromXAttribute<IntExpression>, ILineInfo
{
    public static bool TryFromXAttribute(XAttribute attribute, [MaybeNullWhen(false)] out IntExpression value)
    {
        if (attribute.Name == "Int" && int.TryParse(attribute.Value, out var val))
        {
            value = new IntExpression(val) { Pos = attribute.LineInfo() };
            return true;
        }
        value = default;
        return false;
    }

    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out IntExpression value)
    {
        if (element != null && int.TryParse(element.Value, out var val))
        {
            value = new IntExpression(val) { Pos = element.LineInfo() };
            return true;
        }
        value = default;
        return false;
    }
}
