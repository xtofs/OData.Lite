
namespace OData.Lite;

interface ILineInfo
{
    (int LineNumber, int LinePosition) LineInfo { get; }
}
