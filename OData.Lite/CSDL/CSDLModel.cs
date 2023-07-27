namespace OData.Lite.CSDL.XML;

using System.Xml.Serialization;
using OData.Lite.EDM;

// [XmlInclude(typeof(EnumType)), XmlInclude(typeof(ComplexType))]
public abstract class SchemaElement
{
    [XmlAttribute(AttributeName = "Name")]
    public abstract string Name { get; set; }
}


public class Member
{
    [XmlAttribute(AttributeName = "Name")]
    public required string Name { get; set; }

    public override string ToString()
    {
        return $"{{Member Name={Name}}}";
    }
}

// [XmlRoot(ElementName = "EnumType")]
public class EnumType : SchemaElement
{

    [XmlElement(ElementName = "Member", Namespace = NS.EDM)]
    public required List<Member> Members { get; set; }


    [XmlAttribute(AttributeName = "Name")]
    public override required string Name { get; set; }

    public override string ToString()
    {
        return $"{{Schema Name={Name}  Members=[{string.Join(", ", Members)}]}}";
    }
}

public class Property
{
    [XmlAttribute(AttributeName = "Name")]
    public required string Name { get; set; }

    [XmlAttribute(AttributeName = "Type")]
    public required string Type { get; set; }

    public override string ToString()
    {
        return $"{{Property Name={Name} Type={Type}}}";
    }
}

public class ComplexType : SchemaElement
{

    [XmlElement(ElementName = "Property")]
    public required List<Property> Properties { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public override required string Name { get; set; }

    public override string ToString()
    {
        return $"{{ComplexType Name={Name} Properties=[{string.Join(", ", Properties)}]}}";
    }
}

public class Schema
{
    [XmlAttribute(AttributeName = "Alias", Namespace = NS.EDM)]
    public required string? Alias { get; set; }

    [XmlAttribute(AttributeName = "Namespace", Namespace = NS.EDM)]
    public required string Namespace { get; set; }

    [XmlElement(ElementName = "EnumType", Type = typeof(EnumType), Namespace = NS.EDM)]
    [XmlElement(ElementName = "ComplexType", Type = typeof(ComplexType), Namespace = NS.EDM)]
    public required List<SchemaElement> Elements { get; set; }

    public override string ToString()
    {
        return $"{{Schema Alias={Alias} Namespace={Namespace} Elements=[{string.Join(", ", Elements)}]}}";
    }
}

// [XmlElement(ElementName = "DataServices", Namespace = NS.EDMX)]
public class DataServices
{

    [XmlElement(ElementName = "Schema", Namespace = NS.EDM)]
    public required List<Schema> Schemas { get; set; }

    public override string ToString()
    {
        return $"{{DataServices Schema=[{string.Join(", ", Schemas)}]}}";
    }
}

[XmlRoot(ElementName = "Edmx", Namespace = NS.EDMX)]
public class Edmx
{
    [XmlElement(ElementName = "DataServices")]
    public required DataServices DataServices { get; set; }

    [XmlAttribute(AttributeName = "Version")]
    public required string Version { get; set; }

    [XmlAttribute(AttributeName = "SchemaLocation", Namespace = NS.XSI)]
    public string SchemaLocation { get; set; } = $"{NS.EDMX} {NS.EDMXLocation} {NS.EDM} {NS.EDMLocation}";

    public override string ToString()
    {
        return $"{{Edmx Version={Version} DataServices={DataServices}}}";
    }
}

