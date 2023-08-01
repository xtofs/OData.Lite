
namespace OData.Lite;

interface IXmlLineInfo
{
    (int LineNumber, int LinePosition) Position { get; }
}
