namespace OData.Lite;

using System;
using System.Xml.Serialization;
using OData.Lite.EDM;


[XmlRoot(ElementName = "Edmx", Namespace = NS.EDMX)]
public class Model
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

    public bool TryResolve<T>(string fqn, [MaybeNullWhen(false)] out SchemaElement element)
    {
        var ix = fqn.LastIndexOf('.');
        if (ix >= 0)
        {
            var parts = (fqn[..(ix)], fqn[(ix + 1)..]);

            element = default;
            return this.DataServices.Schemas.TryFind(parts.Item1, out var schema)
                && schema.Elements.TryFind(parts.Item2, out element);
        }
        else
        {
            element = default;
            return false;
        }
    }

}

public record SchemaCollection() : Container<Schema>(e => e.Namespace)
{
}

public class DataServices
{
    [XmlElement(ElementName = "Schema", Namespace = NS.EDM)]
    public required SchemaCollection Schemas { get; set; }

    public override string ToString()
    {
        return $"{{DataServices Schema=[{string.Join(", ", Schemas)}]}}";
    }
}



// [XmlInclude(typeof(EnumType)), XmlInclude(typeof(ComplexType))]
public abstract class SchemaElement
{
    [XmlAttribute(AttributeName = "Name")]
    public abstract string Name { get; set; }
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

public record MemberCollection() : Container<Member>(e => e.Name)
{
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


public record PropertyCollection() : Container<Property>(e => e.Name)
{
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
    public required PropertyCollection Properties { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public override required string Name { get; set; }

    public override string ToString()
    {
        return $"{{ComplexType Name={Name} Properties=[{string.Join(", ", Properties)}]}}";
    }
}


public record SchemaElementCollection() : Container<SchemaElement>(e => e.Name)
{
    public bool TryFindElement(string name, [MaybeNullWhen(false)] out SchemaElement element) =>
           base.TryFind<SchemaElement>(name, out element);

    public bool TryFindElement<TElement>(string name, [MaybeNullWhen(false)] out TElement element)
        where TElement : SchemaElement =>
           base.TryFind<TElement>(name, out element);
}

public class Schema
{
    [XmlAttribute(AttributeName = "Alias", Namespace = NS.EDM)]
    public required string? Alias { get; set; }

    [XmlAttribute(AttributeName = "Namespace", Namespace = NS.EDM)]
    public required string Namespace { get; set; }

    [XmlElement(ElementName = "EnumType", Type = typeof(EnumType), Namespace = NS.EDM)]
    [XmlElement(ElementName = "ComplexType", Type = typeof(ComplexType), Namespace = NS.EDM)]
    public required SchemaElementCollection Elements { get; set; }

    public override string ToString()
    {
        return $"{{Schema Alias={Alias} Namespace={Namespace} Elements=[{string.Join(", ", Elements)}]}}";
    }
}

