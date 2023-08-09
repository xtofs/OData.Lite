using System;
using System.Data.SqlTypes;

namespace OData.Lite;

// https://docs.oasis-open.org/odata/odata-csdl-xml/v4.01/odata-csdl-xml-v4.01.html#sec_Term
record class Term(string Name, string Type, bool Nullable, AppliesTo AppliesTo) : SchemaElement(Name), IFromXElement<Term>, ILineInfo
{
    internal required (int LineNumber, int LinePosition) Pos { get; init; }

    (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;

    public static bool TryFromXElement(XElement element, [MaybeNullWhen(false)] out Term value)
    {
        var pos = element.LineInfo();
        string name = element.Attribute("Name")?.Value ?? "";
        string type = element.Attribute("Type")?.Value ?? "";
        if (bool.TryParse(element.Attribute("Nullable")?.Value ?? "false", out var nullable) &&
            Enum.TryParse<AppliesTo>(element.Attribute("AppliesTo")?.Value, out var appliesTo))
        {
            value = new Term(name, type, nullable, appliesTo) { Pos = pos };
          return true;
        }
        value = default;
        return false;
    }
}

enum AppliesTo
{
    Action,
    ActionImport,
    Annotation,
    Apply, //Application of a client-side function in an annotation
    Cast, // Type Cast annotation expression
    Collection,//Entity Set or collection-valued Property or Navigation Property
    ComplexType,
    EntityContainer,
    EntitySet,
    EntityType,
    EnumType, // Enumeration Type
    Function,
    FunctionImport,
    If, // Conditional annotation expression
    Include,// Reference to an Included Schema
    IsOf,// Type Check annotation expression
    LabeledElement, // Labeled Element expression
    Member, // Enumeration Member
    NavigationProperty,
    Null, // Null annotation expression
    OnDelete,//On-Delete Action of a navigation property
    Parameter,// Action of Function Parameter
    Property,//Property of a structured type
    PropertyValue,//Property value of a Record annotation expression
    Record,//Record annotation expression
    Reference,//Reference to another CSDL document
    ReferentialConstraint,// Referential Constraint of a navigation property
    ReturnType,// Return Type of an Action or Function
    Schema,
    Singleton,
    Term,
    TypeDefinition,
    UrlRef, // UrlRef annotation expression
}