﻿namespace OData.Lite;

public abstract record class AnnotationExpression() : ILineInfo, IFromXElement<AnnotationExpression>
{

    public required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;

    // TODO: this requires to know all the suptypes of AnnotationExpression. Maybe
    // reflection at assemply load tiem can help.
    static readonly HashSet<string> LiteralIdentifiers = new HashSet<string> { "Int", "Bool", "Null", "Collection" };

    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out AnnotationExpression value)
    {
        var eNames = element.Elements().Select(e => e.Name.LocalName);
        var aNames = element.Attributes().Select(a => a.Name.LocalName);
        var names = eNames.Concat(aNames).Where(n => LiteralIdentifiers.Contains(n)).ToHashSet();
        // TODO: deal with the situation where both Sub-Element and Attribute are provided.
        if (names.Count != 1) { value = default; return false; }

        switch (names.Single())
        {
            case "Null": { var res = NullExpression.TryFromXElement(element, out var val); value = val; return res; }
            case "Int": { var res = IntExpression.TryFromXElement(element, out var val); value = val; return res; }
            case "Bool": { var res = BoolExpression.TryFromXElement(element, out var val); value = val; return res; }
            case "Collection": { var res = CollectionExpression.TryFromXElement(element, out var val); value = val; return res; }
            default: value = default; return false;
        }
    }
}
