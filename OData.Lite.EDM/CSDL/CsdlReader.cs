namespace OData.Lite.EDM;

using System.Xml;
using System.Xml.Serialization;
using OData.Lite.CSDL.XML;

public sealed class CsdlReader
{
    public CsdlReader()
    {
    }

    public bool TryRead(TextReader reader, [MaybeNullWhen(false)] out Model model)
    {
        var serializer = new XmlSerializer(typeof(Edmx));

        var obj = serializer.Deserialize(reader);
        if (obj != null && obj is Edmx edmx)
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
