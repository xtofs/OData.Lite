namespace OData.Lite;

using System.Xml;
using System.Xml.Serialization;


[XmlRoot(ElementName = "Edmx", Namespace = NS.EDMX)]
public record class Model
{
    [XmlElement(ElementName = "DataServices")]
    public required DataServices DataServices { get; set; }

    [XmlAttribute(AttributeName = "Version")]
    public required string Version { get; set; }

    [XmlAttribute(AttributeName = "SchemaLocation", Namespace = NS.XSI)]
    public string SchemaLocation { get; set; } = $"{NS.EDMX} {NS.EDMXLocation} {NS.EDM} {NS.EDMLocation}";

    public bool TryResolve<T>(TypeRef type, [MaybeNullWhen(false)] out T element)
    where T : SchemaElement
    {
        string fqn = type.FQN;
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

public record class Schema
{
    [XmlAttribute(AttributeName = "Alias", Namespace = NS.EDM)]
    public required string? Alias { get; set; }

    [XmlAttribute(AttributeName = "Namespace", Namespace = NS.EDM)]
    public required string Namespace { get; set; }

    [XmlElement(ElementName = "EnumType", Type = typeof(EnumType), Namespace = NS.EDM)]
    [XmlElement(ElementName = "ComplexType", Type = typeof(ComplexType), Namespace = NS.EDM)]
    [XmlElement(ElementName = "EntityType", Type = typeof(EntityType), Namespace = NS.EDM)]
    public required SchemaElementCollection Elements { get; set; }

    [XmlElement(ElementName = "EntityContainer")]
    public required EntityContainer EntityContainer { get; set; }
}

public record class DataServices
{
    [XmlElement(ElementName = "Schema", Namespace = NS.EDM)]
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
    [XmlElement(ElementName = "Member", Namespace = NS.EDM)]
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
    public string TypeFQN { get; set; }

    [XmlIgnore]
    public required TypeRef Type
    {
        get => new TypeRef(TypeFQN);
        set { TypeFQN = value.FQN; }
    }
}

public record class ComplexType : SchemaElement
{
    [XmlElement(ElementName = "Property")]
    public required PropertyCollection Properties { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public override required string Name { get; set; }
}

public record class EntityType : SchemaElement
{
    [XmlAttribute(AttributeName = "Name")]
    public override required string Name { get; set; }

    [XmlElement(ElementName = "Key")]
    public required Key Key { get; set; }

    [XmlElement(ElementName = "Property")]
    public required PropertyCollection Properties { get; set; }
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

public class TypeRef
{
    public TypeRef() { FQN = string.Empty; }

    public TypeRef(string fqn) { FQN = fqn; }

    public string FQN { get; private set; }
}


public class EntityContainer
{
    [XmlAttribute(AttributeName = "Name")]
    public required string Name { get; set; }

    [XmlElement(ElementName = "EntitySet", Type = typeof(EntitySet), Namespace = NS.EDM)]
    [XmlElement(ElementName = "Singleton", Type = typeof(Singleton), Namespace = NS.EDM)]
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
