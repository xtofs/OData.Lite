namespace OData.Lite;

using System.Xml;

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

    public readonly XmlWriter writer;


    public void Write(Model model)
    {
        writer.WriteStartDocument();
        {
            writer.WriteStartElement("edmx", "Edmx", NS.EDMX);
            writer.WriteAttributeString("xmlns", "edmx", null, NS.EDMX);
            writer.WriteAttributeString("xmlns", "xsi", null, NS.XSI);
            writer.WriteAttributeString("xsi", "schemaLocation", null, $"{NS.EDMX} {NS.EDMXLocation} {NS.EDM} {NS.EDMLocation}");
            writer.WriteAttributeString("Version", "4.01");

            {
                writer.WriteStartElement("DataServices", NS.EDMX);
                foreach (var schema in model.DataServices.Schemas)
                {
                    Write(schema);
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        writer.WriteRaw(Environment.NewLine);
        writer.WriteEndDocument();
        writer.Flush();
    }

    private void Write(Schema schema)
    {
        writer.WriteStartElement("Schema", NS.EDM);
        writer.WriteAttributeString("Namespace", schema.Namespace);
        writer.WriteAttributeString("Alias", schema.Alias);
        foreach (var element in schema.Elements)
        {
            switch (element)
            {
                case ComplexType complexType:
                    Write(complexType);
                    break;
                case EnumType enumType:
                    Write(enumType);
                    break;
            }
        }
        writer.WriteFullEndElement();
    }

    private void Write(ComplexType complexType)
    {
        writer.WriteStartElement("ComplexType", NS.EDM);
        writer.WriteAttributeString("Name", complexType.Name);
        foreach (var property in complexType.Properties)
        {
            Write(property);
        }
        writer.WriteEndElement();
    }


    private void Write(EnumType enumType)
    {
        writer.WriteStartElement("EnumType", NS.EDM);
        writer.WriteAttributeString("Name", enumType.Name);
        foreach (var member in enumType.Members)
        {
            Write(member);
        }
        writer.WriteEndElement();
    }

    private void Write(
        Member member)
    {
        writer.WriteStartElement("Member", NS.EDM);
        writer.WriteAttributeString("Name", member.Name);
        writer.WriteEndElement();
    }

    private void Write(Property property)
    {
        writer.WriteStartElement("Property", NS.EDM);
        writer.WriteAttributeString("Name", property.Name);
        // TODO replace with a struct
        writer.WriteAttributeString("Type", property.Type);
        writer.WriteEndElement();
    }

    public void Dispose()
    {
        ((IDisposable)writer).Dispose();
    }
}
namespace OData.Lite.EDM;

using System.Xml;

// public sealed class CsdlWriter : IDisposable
// {
//     public CsdlWriter(TextWriter inner)
//     {
//         var settings = new XmlWriterSettings
//         {
//             Indent = true,
//         };
//         writer = XmlWriter.Create(inner, settings);
//     }

//     public readonly XmlWriter writer;


//     public void Write(Model model)
//     {
//         writer.WriteStartDocument();
//         {
//             writer.WriteStartElement("edmx", "Edmx", NS.EDMX);
//             writer.WriteAttributeString("xmlns", "edmx", null, NS.EDMX);
//             writer.WriteAttributeString("xmlns", "xsi", null, NS.XSI);
//             writer.WriteAttributeString("xsi", "schemaLocation", null, $"{NS.EDMX} {NS.EDMXLocation} {NS.EDM} {NS.EDMLocation}");
//             writer.WriteAttributeString("Version", "4.01");

//             {
//                 writer.WriteStartElement("DataServices", NS.EDMX);
//                 foreach (var schema in model.Schemas)
//                 {
//                     Write(schema);
//                 }
//                 writer.WriteEndElement();
//             }
//             writer.WriteEndElement();
//         }
//         writer.WriteRaw(Environment.NewLine);
//         writer.WriteEndDocument();
//         writer.Flush();
//     }

//     private void Write(Schema schema)
//     {
//         writer.WriteStartElement("Schema", NS.EDM);
//         writer.WriteAttributeString("Namespace", schema.Namespace);
//         writer.WriteAttributeString("Alias", schema.Alias);
//         foreach (var element in schema.Elements)
//         {
//             switch (element)
//             {
//                 case ComplexType complexType:
//                     Write(complexType);
//                     break;
//                 case EnumType enumType:
//                     Write(enumType);
//                     break;
//             }
//         }
//         writer.WriteFullEndElement();
//     }

//     private void Write(ComplexType complexType)
//     {
//         writer.WriteStartElement("ComplexType", NS.EDM);
//         writer.WriteAttributeString("Name", complexType.Name);
//         foreach (var property in complexType.Properties)
//         {
//             Write(property);
//         }
//         writer.WriteEndElement();
//     }


//     private void Write(EnumType enumType)
//     {
//         writer.WriteStartElement("EnumType", NS.EDM);
//         writer.WriteAttributeString("Name", enumType.Name);
//         foreach (var member in enumType.Members)
//         {
//             Write(member);
//         }
//         writer.WriteEndElement();
//     }

//     private void Write(EnumMember member)
//     {
//         writer.WriteStartElement("Member", NS.EDM);
//         writer.WriteAttributeString("Name", member.Name);
//         writer.WriteEndElement();
//     }

//     private void Write(Property property)
//     {
//         writer.WriteStartElement("Property", NS.EDM);
//         writer.WriteAttributeString("Name", property.Name);
//         writer.WriteAttributeString("Type", property.Type.Name);
//         writer.WriteEndElement();
//     }

//     public void Dispose()
//     {
//         ((IDisposable)writer).Dispose();
//     }
// }
