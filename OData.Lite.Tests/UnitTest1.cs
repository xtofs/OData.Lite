namespace OData.Lite.Tests;
using System.Xml.Linq;

public class UnitTest1
{
    [Theory]

    [InlineData("""<Annotation Term="Foo" Int="7"/>""")]
    [InlineData("""<Annotation Term="Foo" Bool="false"/>""")]

    [InlineData("""<Annotation Term="Foo"><Int>7</Int></Annotation>""")]
    [InlineData("""<Annotation Term="Foo"><Bool>true</Bool></Annotation>""")]
    [InlineData("""<Annotation Term="Foo"><Null/></Annotation>""")]

    [InlineData("""<Annotation Term="Foo" Int="7"><Bool>true</Bool></Annotation>""")]
    [InlineData("""<Annotation><Collection><Int>1</Int><Int>2</Int></Collection></Annotation>""")]

    public void CanParse(string csdl)
    {
        var e = XElement.Parse(csdl, LoadOptions.SetLineInfo | LoadOptions.SetBaseUri);

        if (Annotation.TryFromXElement(e, out var a))
        {
            Console.WriteLine("{0}:\n\t{1}", e.ToString(SaveOptions.DisableFormatting), a);
        }
        else
        {
            Console.WriteLine("{0}:\n\tfailed to parse.", e.ToString(SaveOptions.DisableFormatting));
        }
        Console.WriteLine();
    }
}