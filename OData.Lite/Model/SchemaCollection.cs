namespace OData.Lite;

public class SchemaCollection : KeyedCollection<string, Schema>
{
    public SchemaCollection()
    { }

    public SchemaCollection(IEnumerable<Schema> schemas)
    {
        foreach (var schema in schemas) { Add(schema); }
    }

    protected override string GetKeyForItem(Schema item) => item.Namespace;

    public override string ToString()
    {
        return "[" + string.Join(", ", this) + "]";
    }

    internal static SchemaCollection FromXElements(IEnumerable<XElement> elements)
    {
        var self = new SchemaCollection();
        foreach (var element in elements)
        {
            if (Schema.TryFromXElement(element, out var item))
            {
                self.Add(item);
            }
        }
        return self;
    }

    public bool TryFind(string nameSpace, [MaybeNullWhen(false)] out Schema schema)
    {
        return this.TryGetValue(nameSpace, out schema);
    }
}
