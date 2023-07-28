namespace OData.Lite;

using System.Xml;
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

public static class CsdlWriter
{
    public static void Write(string path, Model model)
    {
        using var writer = File.CreateText(path);
        Write(writer, model);
    }

    public static void Write(TextWriter writer, Model model)
    {
        var settings = new XmlWriterSettings { Indent = true };
        var serializer = new XmlSerializer(typeof(Model));
        using (var xmlWriter = XmlWriter.Create(writer, settings))
        {
            serializer.Serialize(xmlWriter, model);
        }
    }
}
