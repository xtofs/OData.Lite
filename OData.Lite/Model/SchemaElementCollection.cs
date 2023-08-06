namespace OData.Lite;

public class SchemaElementCollection : KeyedCollection<string, SchemaElement>
{
    public SchemaElementCollection() { }

    public SchemaElementCollection(IEnumerable<SchemaElement> elements)
    {
        foreach (var element in elements) { Add(element); }
    }
    protected override string GetKeyForItem(SchemaElement item) => item.Name;

    public override string ToString()
    {
        return "[" + string.Join(", ", this) + "]";
    }

    internal static SchemaElementCollection FromXElements(IEnumerable<XElement> elements)
    {
        return new SchemaElementCollection(elements.FromXElements<SchemaElement>());
    }

    public bool TryFind<T>(string name, [MaybeNullWhen(false)] out T element) where T : SchemaElement
    {
        element = default; //  necessary since complier can't figure out the .Is<T> pattern.
        return this.TryGetValue(name, out var el) && el.Is<T>(out element);
    }
}
