
using System.Reflection.Metadata;
using System.Xml.Linq;
using OData.Lite;
using OData.Lite.CSDL.XML;

internal class Program
{
    private static void Main()
    {
        using var text = File.OpenText("example.csdl.xml");
        if (CsdlReader.TryRead(text, out var model))
        {
            System.Console.WriteLine(model); ;
        }

        // var model = new Model{
        //     new Schema("example.com", "self"){
        //         new EnumType("Color") {
        //             new EnumMember("Red"),
        //             new EnumMember("Green"),
        //         },
        //         new ComplexType("Shoe") {
        //             new Property("size", new TypeRef("Edm.Float")),
        //             new Property("color", new TypeRef("self.Color"))
        //         }
        //     },
        // };

        // using (var writer = new CsdlWriter(Console.Out))
        // {
        //     writer.Write(model);
        // }

        // if (model.TryFindSchema("self", out var schema) &&
        //     schema.TryFindElement<ComplexType>("Shoe", out var complex) &&
        //     complex.TryFindProperty("color", out var prop) &&
        //     model.TryResolve<EnumType>(prop.Type, out var color)
        // )
        // {
        //     Console.WriteLine("found: {0}", color);
        // }
        // else
        // {
        //     Console.WriteLine("not found");
        // }

        // Console.WriteLine(model);

        // Console.WriteLine(model.GetSchema("self"));
        // Console.WriteLine(model.GetSchema("example.com"));

        // var res = from self in model.FindSchema("self")
        //           from shoe in self.FindElement<ComplexType>("Shoe")
        //           from prop in shoe.FindProperty("color")
        //           from color in model.FindType(prop.Type)
        //           select color;
    }
}