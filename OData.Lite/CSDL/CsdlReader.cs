namespace OData.Lite.CSDL.XML;

using System.Xml.Serialization;
using OData.Lite;

public sealed class CsdlReader
{
    public CsdlReader()
    {
    }

    public static bool TryRead(TextReader reader, [MaybeNullWhen(false)] out Model model)
    {
        var serializer = new XmlSerializer(typeof(Model));

        var obj = serializer.Deserialize(reader);
        if (obj != null && obj is Model edmx)
        {
            System.Console.WriteLine(edmx);

            serializer.Serialize(Console.Out, edmx);
        }
        // reader.Read();
        // if (reader.NodeType == XmlNodeType.XmlDeclaration)
        // {
        //     reader.Read();
        // }
        // if (reader.NodeType == XmlNodeType.Whitespace)
        // {
        //     reader.Read();
        // }
        // if (reader.NodeType == XmlNodeType.Element && reader.NamespaceURI == NS.EDMX && reader.LocalName == "Edmx")
        // {
        //     // var version = reader.ReadAttributeValue("Version");
        //     model = new Model() { };
        //     return true;
        // }
        model = default;
        return false;
    }
}
