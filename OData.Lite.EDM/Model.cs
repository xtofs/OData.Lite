namespace OData.Lite.EDM;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.VisualBasic;
using OData.Lite.EDM;


public record class Model(Namespace<Schema> Schemas) : IEnumerable<Schema>
{
    public Model() : this(new Namespace<Schema>(s => s.Namespace, s => s.Alias!))
    {
    }

    public void Add(Schema schema) => this.Schemas.Add(schema);

    public Schema? GetSchema(string v)
    {
        return Schemas.TryGetValue(v, out var schema) ? schema : null;
    }

    public bool TryGetSchema(string v, [MaybeNullWhen(false)] out Schema schema)
    {
        return Schemas.TryGetValue(v, out schema);
    }

    IEnumerator<Schema> IEnumerable<Schema>.GetEnumerator() => Schemas.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Schemas.GetEnumerator();
}

public enum SchemaElementKind { }

public abstract record SchemaElement(SchemaElementKind Kind, string Name)
{
}

public record class Schema(String Namespace, string? Alias) : IEnumerable
{

    public Namespace<SchemaElement> Elements { get; } = new Namespace<SchemaElement>(ns => ns.Name);

    IEnumerator IEnumerable.GetEnumerator() => Elements.GetEnumerator();
}


public record class Reference(Uri Uri, Include Include)
{
}

public record class Include(String Namespace, string? Alias)
{
}
