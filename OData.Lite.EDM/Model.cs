namespace OData.Lite;

using System.ComponentModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

// https://learn.microsoft.com/en-us/dotnet/standard/serialization/attributes-that-control-xml-serialization

[XmlRoot(ElementName = "Edmx", Namespace = XmlNamespaces.EDMX)]
public record class Model
{
    [XmlAttribute(AttributeName = "Version")]
    public required string Version { get; set; }

    [XmlAttribute(AttributeName = "SchemaLocation", Namespace = XmlNamespaces.XSI)]
    public string SchemaLocation { get; } = $"{XmlNamespaces.EDMX} {XmlNamespaces.EDMXLocation} {XmlNamespaces.EDM} {XmlNamespaces.EDMLocation}";

    [XmlElement(ElementName = "Reference", Namespace = XmlNamespaces.EDMX)]
    public required ReferenceCollection Reference { get; set; }

    [XmlElement(ElementName = "DataServices")]
    public required DataServices DataServices { get; set; }



    public bool TryResolve<T>(string fqn, [MaybeNullWhen(false)] out T element)
    where T : SchemaElement
    {
        var ix = fqn.LastIndexOf('.');
        if (ix >= 0)
        {
            var parts = (fqn[..(ix)], fqn[(ix + 1)..]);

            element = default;
            return this.DataServices.Schemas.TryFind(parts.Item1, out var schema)
                && schema.Elements.TryFind<T>(parts.Item2, out element);
        }
        else
        {
            element = default;
            return false;
        }
    }
}


public record ReferenceCollection() : MultiStringIndexedCollection<Reference>(e => e.Uri)
{
}

public record SchemaCollection() : MultiStringIndexedCollection<Schema>(e => e.Namespace, e => e.Alias)
{
}

public record SchemaElementCollection() : StringIndexedCollection<SchemaElement>(e => e.Name)
{
    public bool TryFindElement(string name, [MaybeNullWhen(false)] out SchemaElement element) =>
           base.TryFind<SchemaElement>(name, out element);

    public bool TryFindElement<TElement>(string name, [MaybeNullWhen(false)] out TElement element)
        where TElement : SchemaElement =>
           base.TryFind<TElement>(name, out element);
}

public record class Reference
{
    [XmlAttribute(AttributeName = "Uri")]
    public required string Uri { get; set; }

    [XmlElement(ElementName = "Include", Namespace = XmlNamespaces.EDMX)]
    public required Include Include { get; set; }
}


public record class Include
{
    [XmlAttribute(AttributeName = "Namespace")]
    public required string? Uri { get; set; }

    [XmlAttribute(AttributeName = "Alias")]
    public required string? Alias { get; set; }
}

public record class Schema
{
    [XmlAttribute(AttributeName = "Alias", Namespace = XmlNamespaces.EDM)]
    public required string? Alias { get; set; }

    [XmlAttribute(AttributeName = "Namespace", Namespace = XmlNamespaces.EDM)]
    public required string Namespace { get; set; }

    [XmlElement(ElementName = "EnumType", Type = typeof(EnumType), Namespace = XmlNamespaces.EDM)]
    [XmlElement(ElementName = "ComplexType", Type = typeof(ComplexType), Namespace = XmlNamespaces.EDM)]
    [XmlElement(ElementName = "EntityType", Type = typeof(EntityType), Namespace = XmlNamespaces.EDM)]
    public required SchemaElementCollection Elements { get; set; }

    [XmlElement(ElementName = "EntityContainer")]
    public required EntityContainer EntityContainer { get; set; }
}

public record class DataServices
{
    [XmlElement(ElementName = "Schema", Namespace = XmlNamespaces.EDM)]
    public required SchemaCollection Schemas { get; set; }
}

[XmlInclude(typeof(EnumType)), XmlInclude(typeof(ComplexType))]
public abstract record class SchemaElement
{
    [XmlAttribute(AttributeName = "Name")]
    public abstract string Name { get; set; }
}


public record class EnumType : SchemaElement
{
    [XmlElement(ElementName = "Member", Namespace = XmlNamespaces.EDM)]
    public required EnumMemberCollection Members { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public override required string Name { get; set; }
}

public record EnumMemberCollection() : StringIndexedCollection<EnumMember>(e => e.Name)
{
}

public record class EnumMember
{
    [XmlAttribute(AttributeName = "Name")]
    public required string Name { get; set; }
}


public record PropertyCollection() : StringIndexedCollection<Property>(e => e.Name)
{
}

public record class Property
{
    [XmlAttribute(AttributeName = "Name")]
    public required string Name { get; set; }

    [XmlAttribute(AttributeName = "Type")]
    public required string TypeFQN { get; set; }

    // [XmlIgnore]
    // public required TypeRef Type
    // {
    //     get => new(TypeFQN);
    //     set { TypeFQN = value.FQN; }
    // }


    [XmlAttribute(AttributeName = "Nullable", DataType = "boolean")]
    public required bool Nullable { get; set; }

    [XmlAttribute(AttributeName = "Scale")]
    public required string Scale { get; set; }

    [XmlAttribute(AttributeName = "MaxLength")]
    public required string MaxLength { get; set; }
}

public record class ComplexType : SchemaElement
{

    [XmlAttribute(AttributeName = "Name")]
    public override required string Name { get; set; }

    [XmlElement(ElementName = "Property")]
    public required PropertyCollection Properties { get; set; }

    [XmlElement(ElementName = "NavigationProperty")]
    public required NavigationPropertyCollection NavigationProperties { get; set; }
}

public record NavigationPropertyCollection() : StringIndexedCollection<NavigationProperty>(e => e.Name)
{
}

public record class NavigationProperty
{
    [XmlAttribute(AttributeName = "Name")]
    public required string Name { get; set; }

    [XmlAttribute(AttributeName = "Type")]
    public required string TypeFQN { get; set; }

    // [XmlIgnore]
    // public required TypeRef Type
    // {
    //     get => new (TypeFQN);
    //     set { TypeFQN = value.FQN; }
    // }

    [XmlAttribute(AttributeName = "Nullable", DataType = "boolean")]
    // https://docs.oasis-open.org/odata/odata-csdl-xml/v4.01/odata-csdl-xml-v4.01.html#sec_NullableNavigationProperty
    [DefaultValue(true)]
    public required bool Nullable { get; set; }

    [XmlAttribute(AttributeName = "Partner")]
    public required string Partner { get; set; }

    // [XmlElement(ElementName = "OnDelete")]
    // public required OnDelete OnDelete { get; set; }

    // [XmlElement(ElementName = "ReferentialConstraint")]
    // public required ReferentialConstraint ReferentialConstraint { get; set; }
}

// //  <OnDelete Action="Cascade" />
// public record class OnDelete
// {
//     [XmlAttribute(AttributeName = "Action")]
//     public required DeleteAction Action { get; set; }
// }

// public enum DeleteAction { Cascade, None, SetNull, SetDefault }

// public record class ReferentialConstraint
// {
//     [XmlAttribute(AttributeName = "Property")]
//     public required string Property { get; set; }

//     [XmlAttribute(AttributeName = "ReferencedProperty")]
//     public required string ReferencedProperty { get; set; }

// }

public record class EntityType : SchemaElement
{
    [XmlAttribute(AttributeName = "Name")]
    public override required string Name { get; set; }


    // // https://docs.oasis-open.org/odata/odata-csdl-xml/v4.01/odata-csdl-xml-v4.01.html#sec_AbstractEntityType
    // [XmlAttribute(AttributeName = "Abstract")]
    // [DefaultValue(false)]
    // public required bool Abstract { get; set; }

    [XmlAttribute(AttributeName = "HasStream")]
    [DefaultValue(false)]
    public required bool HasStream { get; set; }

    [XmlElement(ElementName = "Key")]
    public required Key Key { get; set; }

    [XmlElement(ElementName = "Property")]
    public required PropertyCollection Properties { get; set; }

    [XmlElement(ElementName = "NavigationProperty")]
    public required NavigationPropertyCollection NavigationProperties { get; set; }

    public XmlSchema? GetSchema()
    {
        return null;
    }
}

public class Key
{
    [XmlElement(ElementName = "PropertyRef")]
    public required List<PropertyRef> PropertyRefs { get; set; }
}


public class PropertyRef
{
    [XmlAttribute(AttributeName = "Name")]
    public required string Name { get; set; }
}

public class EntityContainer
{
    [XmlAttribute(AttributeName = "Name")]
    public required string Name { get; set; }

    [XmlElement(ElementName = "EntitySet", Type = typeof(EntitySet), Namespace = XmlNamespaces.EDM)]
    [XmlElement(ElementName = "Singleton", Type = typeof(Singleton), Namespace = XmlNamespaces.EDM)]
    public required ContainerElementCollection Elements { get; set; }
}


[XmlInclude(typeof(EnumType)), XmlInclude(typeof(ComplexType))]
public abstract record class ContainerElement
{
    [XmlAttribute(AttributeName = "Name")]
    public abstract string Name { get; set; }
}

public record ContainerElementCollection() : StringIndexedCollection<ContainerElement>(e => e.Name)
{
    public bool TryFindElement(string name, [MaybeNullWhen(false)] out ContainerElement element) =>
           base.TryFind<ContainerElement>(name, out element);

    public bool TryFindElement<TElement>(string name, [MaybeNullWhen(false)] out TElement element)
        where TElement : ContainerElement =>
           base.TryFind<TElement>(name, out element);
}
public record class EntitySet : ContainerElement
{
    [XmlAttribute(AttributeName = "Name")]
    public override required string Name { get; set; }

    [XmlAttribute(AttributeName = "EntityType")]
    public required string EntityType { get; set; }
}

public record class Singleton : ContainerElement
{
    [XmlAttribute(AttributeName = "Name")]
    public override required string Name { get; set; }

    [XmlAttribute(AttributeName = "Type", DataType = "string")]
    public required string EntityType { get; set; }

    [XmlAttribute(AttributeName = "Nullable", DataType = "boolean")]
    public required bool Nullable { get; set; }
}


public static class XmlReaderExtensions
{
    public static (int, int) Location(this XmlReader reader)
    {
        IXmlLineInfo xmlInfo = (IXmlLineInfo)reader;
        return (xmlInfo.LineNumber, xmlInfo.LinePosition);
    }
}