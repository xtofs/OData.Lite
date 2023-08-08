namespace OData.Lite;


public abstract record class AnnotationExpression() : ILineInfo, IFromXElement<AnnotationExpression>
{
    public required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;


    static readonly HashSet<string> ConcreteExpressions = new HashSet<string> { "Bool", "Int", "Null" };


    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out AnnotationExpression value)
    {
        element = element.Elements().Single();
        switch (element.Name.LocalName)
        {
            case "Null": { var res = NullExpression.TryFromXElement(element, out var val); value = val; return res; }
            case "Bool": { var res = BoolExpression.TryFromXElement(element, out var val); value = val; return res; }
            case "Int": { var res = IntExpression.TryFromXElement(element, out var val); value = val; return res; }
            default: value = default; return false;
        }
        value = default; return false;
    }
}
