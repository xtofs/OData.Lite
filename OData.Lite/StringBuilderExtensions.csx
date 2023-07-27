using System.Collections;
using System.Text;

namespace OData.Lite;

interface IPrintable<T>
{
    bool WriteTo(StringBuilder builder);
}

static class StringBuilderExtensions
{
    public static string Print<T>(this StringBuilder builder, T value) where T : IPrintable<T>
    {
        value.WriteTo(builder);
        return builder.ToString();
    }

    public static bool WriteTo<T>(this IEnumerable<T> items, StringBuilder builder, string prefix, string separator, string postfix)
        where T : IPrintable<T>
    {
        var some = false;
        builder.Append(prefix);
        foreach (var item in items)
        {
            if (some) { builder.Append(separator); }
            some |= item.WriteTo(builder);
        }
        builder.Append(postfix);
        return true;
    }
}



// static class StringBuilderExtensions
// {
//     public static void AppendJoin<T>(this StringBuilder builder, string prefix, IEnumerable<T> items, string separator, string postfix, Action<StringBuilder, T> printItem)
//     {
//         var first = true;
//         builder.Append(prefix);
//         foreach (var item in items)
//         {
//             if (first) { builder.Append(separator); }
//             first = false;
//             printItem(builder, item);
//         }
//         builder.Append(postfix);
//     }

//     public enum SchemaElementKind { }

//     public abstract record SchemaElement(SchemaElementKind Kind, string Name) { }

//     public record class Schema(String Namespace, string? Alias) : IEnumerable
//     {

//         public Namespace<SchemaElement> Elements { get; } = new Namespace<SchemaElement>(ns => ns.Name);

//         IEnumerator IEnumerable.GetEnumerator() => Elements.GetEnumerator();
//     }


//     public record class Reference(Uri Uri, Include Include)
//     {
//     }

//     public record class Include(String Namespace, string? Alias)
//     {
//     }
