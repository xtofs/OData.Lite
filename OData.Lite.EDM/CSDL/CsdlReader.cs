namespace OData.Lite;

using System.IO.Enumeration;
using System.Xml;
using System.Xml.Serialization;

public static class CsdlReader
{

    public static bool TryRead(TextReader reader, [MaybeNullWhen(false)] out Model model)
    {
        var serializer = new XmlSerializer(typeof(Model));
        serializer.UnknownNode += Serializer_UnknownNode;
        // serializer.UnknownAttribute += Serializer_UnknownAttribute;

        var obj = serializer.Deserialize(reader);
        if (obj is not null && obj is Model edmx)
        {
            // System.Console.WriteLine(edmx);
            // serializer.Serialize(Console.Out, edmx);

            model = edmx;
            return true;
        }
        model = default;
        return false;
    }

    private static readonly HashSet<(string, XmlNodeType)> seen = new();

    private static void Serializer_UnknownNode(object? sender, XmlNodeEventArgs e)
    {
        if (seen.Contains((e.Name, e.NodeType))) { return; } else { seen.Add((e.Name, e.NodeType)); }
        if (e.NodeType == XmlNodeType.Attribute && e.NamespaceURI == XmlNamespaces.XSI)
        {
            return;
        }
        // Log.Logger.LogWarning("unknown {Type} {Name} ({No},{Co})", e.NodeType, e.Name, e.LineNumber, e.LinePosition);
    }
}
