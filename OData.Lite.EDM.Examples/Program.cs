using OData.Lite;
using OData.Lite.EDM;

internal class Program
{
    private static void Main()
    {
        if (CsdlReader.TryRead(File.OpenText("example.csdl.xml"), out var model))
        {
            System.Console.WriteLine(model); ;

            // using (var writer = new CsdlWriter(Console.Out))
            // {
            //     writer.Write(model);
            // }

            if (model.DataServices.Schemas.TryFind("self", out var schema) &&
                schema.Elements.TryFind<OData.Lite.ComplexType>("Shoe", out var complex) &&
                complex.Properties.TryFind("color", out var prop) &&
                model.TryResolve<EnumType>(prop.Type, out var color)
            )
            {
                Console.WriteLine("found: {0}", color);
            }
            else
            {
                Console.WriteLine("not found");
            }
        }

        // Console.WriteLine(model.GetSchema("self"));
        // Console.WriteLine(model.GetSchema("example.com"));


    }
}