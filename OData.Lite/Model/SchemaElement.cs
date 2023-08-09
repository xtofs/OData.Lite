namespace OData.Lite;

public abstract record class SchemaElement(string Name) : IFromXElement<SchemaElement>
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out SchemaElement value)
    {
        switch (element.Name.LocalName)
        {
            case "EnumType":
                var res = EnumType.TryFromXElement(element, out var enumType);
                value = enumType;
                return res;
            case "EntityType":
                res = EntityType.TryFromXElement(element, out var entityType);
                value = entityType;
                return res;
            case "ComplexType":
                res = ComplexType.TryFromXElement(element, out var complexType);
                value = complexType;
                return res;
            case "Term":
                res = Term.TryFromXElement(element, out var term);
                value = term;
                return res;

            default:
                value = null;
                return false;
        }
    }
}


public static class SchemaElementExtensions
{
    public static bool Is<T>(this SchemaElement schemaElement, [MaybeNullWhen(false)] out T element)
        where T : SchemaElement
    {
        if (schemaElement is T el)
        {
            element = el; return true;
        }
        else
        {
            element = default;
            return false;
        }
    }
}