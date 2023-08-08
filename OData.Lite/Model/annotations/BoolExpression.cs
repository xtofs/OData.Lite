    namespace OData.Lite;

public sealed record class BoolExpression(bool Value) :
    AnnotationExpression(), IFromXElement<BoolExpression>, IFromXAttribute<BoolExpression>, ILineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out BoolExpression value)
    {
        if (element != null && bool.TryParse(element.Value, out var val))
        {
            value = new BoolExpression(val) { Pos = element.LineInfo() };
            return true;
        }
        value = default;
        return false;
    }

    public static bool TryFromXAttribute(XAttribute attribute, [MaybeNullWhen(false)] out BoolExpression value)
    {
        if (attribute.Name == "Bool" && bool.TryParse(attribute.Value, out var val))
        {
            value = new BoolExpression(val) { Pos = attribute.LineInfo() };
            return true;
        }
        value = default;
        return false;
    }
}