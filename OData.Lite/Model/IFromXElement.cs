
namespace OData.Lite;

public interface IFromXElement<TSelf> where TSelf : IFromXElement<TSelf>
{
    static abstract bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out TSelf value);
}

public interface IFromXAttribute<TSelf> where TSelf : IFromXAttribute<TSelf>
{
    static abstract bool TryFromXAttribute(XAttribute attribute, [MaybeNullWhen(false)] out TSelf value);
}


public static class IFromXElementExtensions
{
    public static IEnumerable<T> FromXElements<T>(this IEnumerable<XElement> elements) where T : IFromXElement<T>
    {
        foreach (var element in elements)
        {
            if (T.TryFromXElement(element, out var item))
            {
                yield return item;
            }
        }
    }
}