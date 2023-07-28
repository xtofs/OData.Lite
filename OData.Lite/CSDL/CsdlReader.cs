namespace OData.Lite;

using System.Xml.Serialization;

public static class CsdlReader
{

    public static bool TryRead(TextReader reader, [MaybeNullWhen(false)] out Model model)
    {
        var serializer = new XmlSerializer(typeof(Model));

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
}
