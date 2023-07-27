﻿namespace OData.Lite.EDM;

using System.Collections;
using System.Diagnostics.CodeAnalysis;

// http://docs.oasis-open.org/odata/odata/v4.0/errata03/os/complete/part3-csdl/odata-v4.0-errata03-os-part3-csdl-complete.html#_Toc453752505



public sealed record class Model() : MultiKeyContainer<Schema>(s => s.Namespace, s => s.Alias)
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
        element = default;
        return TryFindElement(typeRef.Name, out var el) && el.Is(out element);
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

    protected override bool PrintMembers(StringBuilder builder)
    {
        builder.AppendFormat("Schemas = [ {0} ]", string.Join(", ", Schemas));
        return true;
    }
}


// public record class Reference(Uri Uri, Include Include)
// {
// }

// public record class Include(String Namespace, string? Alias)
// {
// }

