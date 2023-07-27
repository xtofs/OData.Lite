namespace OData.Lite.EDM.Model;

using System.Collections;
using System.Diagnostics.CodeAnalysis;


public record class Schema(String Namespace, string? Alias) : Container<ISchemaElement>(e => e.Name)
{
    public IEnumerable<ISchemaElement> Elements => base.Values;

    public bool TryFindElement(string name, [MaybeNullWhen(false)] out ISchemaElement element)
    {
        return base.TryFindItem(name, out element);
    }

    public bool TryFindElement<T>(string name, [MaybeNullWhen(false)] out T element)
        where T : ISchemaElement
    {
        return base.TryFindItem<T>(name, out element);
    }
}

// http://docs.oasis-open.org/odata/odata/v4.0/errata03/os/complete/part3-csdl/odata-v4.0-errata03-os-part3-csdl-complete.html#_Toc453752521
public enum SchemaElementKind
{
    Action,
    Annotations,
    Annotation,
    ComplexType,
    EntityContainer,
    EntityType,
    EnumType,
    Function,
    Term,
    TypeDefinition,
}

public interface ISchemaElement
{
    SchemaElementKind Kind { get; }
    string Name { get; }
}