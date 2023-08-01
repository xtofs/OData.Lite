
namespace OData.Lite;

interface IFromXElement<TSelf>
{
    static abstract bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out TSelf value);
}
