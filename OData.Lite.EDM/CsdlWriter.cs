namespace OData.Lite.EDM;

using System.Xml;
using OData.Lite.EDM.Model;

public sealed class CsdlWriter : IDisposable
{
    public CsdlWriter(TextWriter inner)
    {
        var settings = new XmlWriterSettings
        {
            Indent = true,
        };
        writer = XmlWriter.Create(inner, settings);
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
        foreach (var element in schema.Elements)
        {
            Write(element);
        }
        writer.WriteEndElement();
    }

    private void Write(ISchemaElement element)
    {
        writer.WriteStartElement("Element", EDM);
        writer.WriteAttributeString("Name", element.Name);
        writer.WriteAttributeString("Kind", element.Kind.ToString());
        writer.WriteEndElement();
    }

    public void Dispose()
    {
        ((IDisposable)writer).Dispose();
    }
}
