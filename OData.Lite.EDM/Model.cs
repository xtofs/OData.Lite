namespace OData.Lite.EDM;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.VisualBasic;
using OData.Lite.EDM;


public class Model : IEnumerable<Schema>, IPrintable<Model>
{
    private readonly Namespace<Schema> Schemas;

    public Model()
    {
        this.Schemas = new Namespace<Schema>(s => s.Namespace, s => s.Alias ?? s.Namespace);
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

    public override string ToString()
    {
        return new StringBuilder().Print(this);
    }

    public bool WriteTo(StringBuilder stringBuilder)
    {
        stringBuilder.Append("Model = { Schemas = ");
        Schemas.AsEnumerable<Schema>().WriteTo(stringBuilder, "[", ", ", "]");
        stringBuilder.Append("}}");
        return true;
    }
}

public enum SchemaElementKind { }

public abstract record SchemaElement(SchemaElementKind Kind, string Name) { }

public record class Schema(String Namespace, string? Alias) : IEnumerable, IPrintable<Schema>
{

    public Namespace<SchemaElement> Elements { get; } = new Namespace<SchemaElement>(ns => ns.Name);

    public bool WriteTo(StringBuilder builder)
    {
        builder.Append("Schema {{");
        builder.AppendFormat("Namespace = '{0}'", Namespace);
        if (!string.IsNullOrWhiteSpace(Alias))
        {
            builder.AppendFormat(", Alias = '{0}'", Alias);
        }
        builder.Append("}");
        return true;
    }

    IEnumerator IEnumerable.GetEnumerator() => Elements.GetEnumerator();
}


public record class Reference(Uri Uri, Include Include)
{
}

public record class Include(String Namespace, string? Alias)
{
}
