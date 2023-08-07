namespace OData.Lite;

// https://docs.oasis-open.org/odata/odata-csdl-xml/v4.01/odata-csdl-xml-v4.01.html#sec_Collection
public sealed record class CollectionExpression(IReadOnlyCollection<AnnotationExpression> Items) :
    AnnotationExpression(), IFromXElement<CollectionExpression>, ILineInfo
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out CollectionExpression value)
    {
        var collection = element.Element("Collection");
        if(collection != null) { 
            var items = collection.Elements().SelectWhere<XElement,AnnotationExpression>(TryFromXElement).ToList();

            value = new CollectionExpression(items) { Pos = element.LineInfo() };
            return true;
        }
        value = default;
        return false;
    }

    protected override bool PrintMembers(StringBuilder builder)
    {
        builder.AppendFormat("Items = [");
        builder.AppendJoin(", ", Items);
        builder.Append("], ");
        return base.PrintMembers(builder);

    }
}
