namespace OData.Lite.EDM;

using System.Xml;
using OData.Lite.EDM.Model;

public sealed class CsdlWriter : IDisposable
{
    public CsdlWriter(TextWriter inner)
    {
        writer = XmlWriter.Create(inner, XmlWriterSettings);
    }

    private readonly XmlWriter writer;

    const string EDM = "http://docs.oasis-open.org/odata/ns/edm";
    const string EDMX = "http://docs.oasis-open.org/odata/ns/edmx";

    public void Write(Model.Model model)
    {
        writer.WriteStartElement("edmx", "Edmx", EDMX);
        {
            writer.WriteStartElement("DataServices", EDMX);
            foreach (var schema in model.Schemas)
            {
                Write(schema);
            }
            writer.WriteEndElement();
        }
        writer.WriteEndElement();
    }

    private void Write(Schema schema)
    {
        writer.WriteStartElement("Schema", EDM);
        writer.WriteAttributeString("Namespace", schema.Namespace);
        writer.WriteAttributeString("Alias", schema.Alias);
        writer.WriteEndElement();
    }

    public void Dispose()
    {
        ((IDisposable)writer).Dispose();
    }

    static XmlWriterSettings XmlWriterSettings = new XmlWriterSettings
    {
        Indent = true,
    };
}

// var re
