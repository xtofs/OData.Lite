namespace OData.Lite;

// https://docs.oasis-open.org/odata/odata-csdl-xml/v4.01/odata-csdl-xml-v4.01.html#sec_EntityContainer
public record class EntityContainer(string Name, ContainerElementCollection Elements) : IFromXElement<EntityContainer>, ILineInfo
{
    internal (int, int) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;


    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out EntityContainer value)
    {
        if (element == null || element.Name != EDM + "EntityContainer")
        {
            value = null; return false;
        }
        var pos = element.LineInfo();
        string name = element.Attribute("Name")?.Value ?? "";

        var containerElements = ContainerElementCollection.FromXElements(element.Elements());

        value = new EntityContainer(name, containerElements) { Pos = pos };
        return true;
    }
}

public abstract record class ContainerElement(string Name) : IFromXElement<ContainerElement>
{
    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out ContainerElement value)
    {
        switch (element.Name.LocalName)
        {
            case "EntitySet":
                var res = EntitySet.TryFromXElement(element, out var entitySet);
                value = entitySet;
                return res;
            case "Singleton":
                res = Singleton.TryFromXElement(element, out var singleton);
                value = singleton;
                return res; ;
            default:
                value = null;
                return false;
        }
    }
}


public class ContainerElementCollection : KeyedCollection<string, ContainerElement>
{
    public ContainerElementCollection() { }

    public ContainerElementCollection(IEnumerable<ContainerElement> elements)
    {
        foreach (var element in elements) { Add(element); }
    }
    protected override string GetKeyForItem(ContainerElement item) => item.Name;

    public override string ToString()
    {
        return "[" + string.Join(", ", this) + "]";
    }

    internal static ContainerElementCollection FromXElements(IEnumerable<XElement> elements)
    {
        var self = new ContainerElementCollection();
        foreach (var element in elements)
        {
            if (ContainerElement.TryFromXElement(element, out var item))
            {
                self.Add(item);
            }
        }
        return self;
    }
}
