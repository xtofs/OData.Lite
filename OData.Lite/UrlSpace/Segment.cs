

namespace OData.Lite;

public abstract record class Segment(string Name, string Type)
{
}


public sealed record class PropertySegment(string Name, string Type) : Segment(Name, Type)
{ }

public sealed record class KeySegment(string Name, string Type, string KeyType, string KeyName) : Segment(Name, Type)
{ }

