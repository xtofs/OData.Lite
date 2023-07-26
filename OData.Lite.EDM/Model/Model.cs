namespace OData.Lite.EDM.Model;

using System.Collections;
using System.Diagnostics.CodeAnalysis;

// http://docs.oasis-open.org/odata/odata/v4.0/errata03/os/complete/part3-csdl/odata-v4.0-errata03-os-part3-csdl-complete.html#_Toc453752505

public record class Model(ReadOnlyStringDictionary<Schema> Schemas) : IEnumerable<Schema>
{
    public Model() : this(new ReadOnlyStringDictionary<Schema>(s => s.Namespace, s => s.Alias!))
    {
    }

    public void Add(Schema schema) => this.Schemas.Add(schema);

    public bool TryFindSchema(string aliasOrNamespace, [MaybeNullWhen(false)] out Schema schema)
    {
        return Schemas.TryGetValue(aliasOrNamespace, out schema);
    }

    public IEnumerable<Schema> FindSchema(string name)
    {
        if (Schemas.TryGetValue(name, out var schema))
        {
            yield return schema;
        }
    }

    public bool TryResolve(TypeRef typeRef, [MaybeNullWhen(false)] out ISchemaElement element)
    {
        return TryFindElement(typeRef.Name, out element);
    }

    public bool TryResolve<T>(TypeRef typeRef, [MaybeNullWhen(false)] out T element)
            where T : ISchemaElement
    {
        return TryFindElement<T>(typeRef.Name, out element);
    }

    private bool TryFindElement(string fqn, [MaybeNullWhen(false)] out ISchemaElement element)
    {
        var ix = fqn.LastIndexOf('.');
        var parts = (fqn[..(ix)], fqn[(ix + 1)..]);
        var res = from schema in this.FindSchema(parts.Item1)
                  from elem in schema.FindElement(parts.Item2)
                  select elem;
        return (element = res.FirstOrDefault()) != null;
    }

    private bool TryFindElement<T>(string fqn, [MaybeNullWhen(false)] out T element)
        where T : ISchemaElement
    {
        element = default;
        return TryFindElement(fqn, out var obj) && obj.Is(out element);
    }

    IEnumerator<Schema> IEnumerable<Schema>.GetEnumerator() => Schemas.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Schemas.GetEnumerator();
}


// public record class Reference(Uri Uri, Include Include)
// {
// }

// public record class Include(String Namespace, string? Alias)
// {
// }


