using Microsoft.VisualBasic;

namespace OData.Lite;

public record struct TypeReference(string FQN) : IFromXElement<TypeReference>, ILineInfo
{

    public readonly bool IsCollection(out TypeReference collectionItemType)
    {
        if (this.FQN.StartsWith("Collection("))
        {
            var s = this.FQN.IndexOf('(');
            var e = this.FQN.LastIndexOf(')');
            if (s >= 0 && e >= 00)
            {
                var elementTypeName = this.FQN[(s + 1)..e];
                collectionItemType = new TypeReference(elementTypeName);
                return true;
            }
        }
        collectionItemType = default;
        return false;
    }

    public static bool TryFromXElement(XElement element, out TypeReference typeReference)
    {
        var attr = element.Attribute("Type");
        if (attr == null)
        {
            typeReference = new("UnknownType") { Pos = element.LineInfo() }; ;
        }
        else
        {
            typeReference = new(attr.Value) { Pos = attr.LineInfo() }; ;
        }
        return true;
    }

    internal (int LineNumber, int LinePosition) Pos { get; init; }

    readonly (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;


    public override readonly string ToString()
    {
        return FQN;
    }

    public readonly bool TrySplit([MaybeNullWhen(false)] out string nameSpace, [MaybeNullWhen(false)] out string localName)
    {
        var ix = this.FQN.LastIndexOf('.');
        if (ix < 0)
        {
            nameSpace = default;
            localName = this.FQN;
            return false;
        }
        nameSpace = this.FQN[..(ix)];
        localName = this.FQN[(ix + 1)..];
        return true;
    }
}



public record struct TermReference(string FQN) : IFromXElement<TermReference>, ILineInfo
{
    public static bool TryFromXElement(XElement element, out TermReference termReference)
    {
        var attr = element.Attribute("Term");
        if (attr == null)
        {
            termReference = new("UnknownTerm") { Pos = element.LineInfo() }; ;
        }
        else
        {
            termReference = new(attr.Value) { Pos = attr.LineInfo() }; ;
        }
        return true;
    }

    internal (int LineNumber, int LinePosition) Pos { get; init; }

    readonly (int LineNumber, int LinePosition) ILineInfo.LineInfo => Pos;

    public override readonly string ToString()
    {
        return FQN;
    }

    // public readonly bool TrySplit([MaybeNullWhen(false)] out string nameSpace, [MaybeNullWhen(false)] out string localName)
    // {
    //     var ix = this.FQN.LastIndexOf('.');
    //     if (ix < 0)
    //     {
    //         nameSpace = default;
    //         localName = this.FQN;
    //         return false;
    //     }
    //     nameSpace = this.FQN[..(ix)];
    //     localName = this.FQN[(ix + 1)..];
    //     return true;
    // }
}
