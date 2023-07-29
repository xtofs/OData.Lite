using OData.Lite;
using Microsoft.Extensions.Logging;

internal class Program
{

    private static void Main()
    {
        Log.AddConsole();

        // using var file = File.OpenText("example.csdl.xml");
        using var file = File.OpenText("example89.csdl.xml");

        if (CsdlReader.TryRead(file, out var model))
        {
            Console.WriteLine(model); ;

            Console.WriteLine("\n==========================================================\n");

            CsdlWriter.Write(Console.Out, model);

            Console.WriteLine("\n==========================================================\n");

            if (model.DataServices.Schemas.TryFind("self", out var schema) &&
                schema.Elements.TryFind<OData.Lite.ComplexType>("Shoe", out var complex) &&
                complex.Properties.TryFind("color", out var prop) &&
                model.TryResolve<EnumType>(prop.Type, out var color)
            )
            {
                Console.WriteLine("found self/Show/color's type: {0}", color);
            }
            else
            {
                Console.WriteLine("couldn't find self/Show/color's type");
            }
        }

        // Console.WriteLine(model.GetSchema("self"));
        // Console.WriteLine(model.GetSchema("example.com"));



    }
}
