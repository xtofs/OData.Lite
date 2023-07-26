namespace OData.Lite.EDM.Model;

using System.Collections;
using System.Diagnostics.CodeAnalysis;

// http://docs.oasis-open.org/odata/odata/v4.0/errata03/os/complete/part3-csdl/odata-v4.0-errata03-os-part3-csdl-complete.html#_Toc453752505

public record class Model : Container<Schema>
{

    public IEnumerable<Schema> Schemas => base.Items;

    public bool TryFindSchema(string aliasOrNamespace, [MaybeNullWhen(false)] out Schema schema) =>
        base.TryFindItem(aliasOrNamespace, out schema);

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
        if (ix >= 0)
        {
            var parts = (fqn[..(ix)], fqn[(ix + 1)..]);
            element = default;
            return this.TryFindSchema(parts.Item1, out var schema)
                && schema.TryFindElement(parts.Item2, out element);
        }
        else
        {
            element = default;
            return false;
        }
    }

    private bool TryFindElement<T>(string fqn, [MaybeNullWhen(false)] out T element)
        where T : ISchemaElement
    {
        var ix = fqn.LastIndexOf('.');
        if (ix >= 0)
        {
            var parts = (fqn[..(ix)], fqn[(ix + 1)..]);
            element = default;
            return this.TryFindSchema(parts.Item1, out var schema)
                && schema.TryFindElement<T>(parts.Item2, out element);
        }
        else
        {
            element = default;
            return false;
        }
    }
}


// public record class Reference(Uri Uri, Include Include)
// {
// }

// public record class Include(String Namespace, string? Alias)
// {
// }


