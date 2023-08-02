
namespace OData.Lite

{

    public static class XElementExtensions
    {
        public static (int LineNumber, int LinePosition) LineInfo(this XElement element)
        {
            var lineInfo = (System.Xml.IXmlLineInfo)element;
            return (lineInfo.LineNumber, lineInfo.LinePosition);
        }

        public static (int LineNumber, int LinePosition) LineInfo(this XAttribute attribute)
        {
            var lineInfo = (System.Xml.IXmlLineInfo)attribute;
            return (lineInfo.LineNumber, lineInfo.LinePosition);
        }
    }
}