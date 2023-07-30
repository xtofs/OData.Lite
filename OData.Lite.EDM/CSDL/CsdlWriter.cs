namespace OData.Lite;

using System.Xml;
using System.Xml.Serialization;

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
        using var xmlWriter = XmlWriter.Create(writer, settings);
        serializer.Serialize(xmlWriter, model);
    }
}