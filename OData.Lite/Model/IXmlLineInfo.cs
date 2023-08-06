
namespace OData.Lite;

interface IXmlLineInfo
{
    (int LineNumber, int LinePosition) LineInfo { get; }
}
