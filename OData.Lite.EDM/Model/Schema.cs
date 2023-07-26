namespace OData.Lite.EDM.Model;

using System.Collections;
using System.Diagnostics.CodeAnalysis;


public record class Schema(String Namespace, string? Alias) : IEnumerable
{
    public ReadOnlyStringDictionary<ISchemaElement> Elements { get; } = new ReadOnlyStringDictionary<ISchemaElement>(ns => ns.Name);

    IEnumerator IEnumerable.GetEnumerator() => Elements.GetEnumerator();

    public void Add(ISchemaElement element)
    {
        Elements.Add(element);
    }

    public bool TryFindElement(string name, [MaybeNullWhen(false)] out ISchemaElement element)
    {
        return Elements.TryGetValue(name, out element);
    }

    public IEnumerable<ISchemaElement> FindElement(string name)
    {
        if (TryFindElement(name, out var schema))
        {
            yield return schema;
        }
    }

    public bool TryFindElement<T>(string name, [MaybeNullWhen(false)] out T element)
        where T : ISchemaElement
    {
        element = default;
        return Elements.TryGetValue(name, out var obj) && obj.Is(out element);
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