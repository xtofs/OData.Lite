namespace OData.Lite;

public record class Annotation(TermReference Term, AnnotationExpression Expression) : ILineInfo, IFromXElement<Annotation>
{
    public required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;

    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out Annotation value)
    {
        TermReference.TryFromXElement(element, out var termReference);

        if (TryFromLiteralXAttribute(element, out var expr))
        {
            value = new Annotation(termReference, expr) { Pos = element.LineInfo() };
            return true;
        }
        if (AnnotationExpression.TryFromXElement(element, out expr))
        {
            value = new Annotation(termReference, expr) { Pos = element.LineInfo() };
            return true;
        }
        value = default;
        return false;
    }

    // TODO: this requires to know all the suptypes of AnnotationExpression that allow the XML attribute notation.
    // Maybe reflection at assemply load time can help.
    static readonly HashSet<string> LiteralAsAttributeIdentifiers = new HashSet<string> { "Bool", "Int" };

    static bool TryFromLiteralXAttribute(XElement element, [MaybeNullWhen(false)] out AnnotationExpression value)
    {
        // ensure only one of the XML attribute notations is used
        // see https://docs.oasis-open.org/odata/odata-csdl-xml/v4.01/odata-csdl-xml-v4.01.html#sec_ConstantExpression and following
        var candidates = element.Attributes().Where(a => LiteralAsAttributeIdentifiers.Contains(a.Name.LocalName)).ToList();
        if (candidates.Count != 1) { value = default; return false; }
        var attribute = candidates.Single();
        switch (attribute.Name.LocalName)
        {
            case "Bool": { var res = BoolExpression.TryFromXAttribute(attribute, out var val); value = val; return res; }
            case "Int": { var res = IntExpression.TryFromXAttribute(attribute, out var val); value = val; return res; }
            default: value = default; return false;
        }
        // TODO: deal with the situation where both Sub-Element and Attribute are provided.
        var eNames = element.Elements().Select(e => e.Name.LocalName);
    }
}
