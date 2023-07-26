namespace OData.Lite.EDM;

using System.Diagnostics.CodeAnalysis;

static class Reflection

{
    public static bool Is<S, T>(this S obj, [MaybeNullWhen(false)] out T item)
    {
        if (obj is T tempItem)
        {
            item = tempItem;
            return true;
        }
        else
        {
            item = default;
            return false;
        }
    }
}


