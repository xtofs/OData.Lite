
using System.Reflection.Metadata;
using System.Xml.Linq;
using OData.Lite.EDM;
using OData.Lite.EDM.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        var model = new Model{
            new Schema("example.com", "self"){
                new EnumType("Color") {
                    new EnumMember("Red"),
                    new EnumMember("Green"),
                },
                new ComplexType("Shoe") {
                    new Property("color", new TypeRef("self.Color"))
                }
            },
        };

        using (var writer = new CsdlWriter(Console.Out))
        {
            writer.Write(model);
        }

        if (model.TryFindSchema("self", out var schema) &&
                     schema.TryFindElement<ComplexType>("Shoe", out var complex) &&
                     complex.TryFindProperty("color", out var prop) &&
                     model.TryFindElement<ComplexType>(prop.Type, out var color))
        {
            Console.WriteLine("found: {0}", color);
        }
        else
        {
            Console.WriteLine("not found");
        }

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

// var re
