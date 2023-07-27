using OData.Lite;
using Microsoft.Extensions.Logging;

internal class Program
{

    private static void Main()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        builder.AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.SingleLine = true;
            options.TimestampFormat = "HH:mm:ss ";
        }));

        Log.Logger = loggerFactory.CreateLogger<Program>();


        using var file = File.OpenText("example.csdl.xml");
        if (CsdlReader.TryRead(file, out var model))
        {
            Console.WriteLine(model); ;
            Console.WriteLine();
            // CsdlWriter.Write(Console.Out, model);

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
