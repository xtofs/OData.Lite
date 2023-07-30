using OData.Lite;

internal class Program
{

    private static void Main()
    {
        // Log.AddConsole();

        // using var file = File.OpenText("example.csdl.xml");
        var filename = Environment.GetCommandLineArgs()[1];
        using var file = File.OpenText(filename);

        if (CsdlReader.TryRead(file, out var model))
        {
            // Console.WriteLine(model); ;
            // Console.WriteLine("\n==========================================================\n");

            CsdlWriter.Write("output.csdl.xml", model);


            if (model.DataServices.Schemas.TryFind("ODataDemo", out var schema))
            {
                var urlSpace = UrlSpace.From(model, schema);
                urlSpace.Display(Console.Out);
            }
            Console.WriteLine("\n==========================================================\n");

            // if (model.DataServices.Schemas.TryFind("self", out var schema) &&
            //     schema.Elements.TryFind<OData.Lite.ComplexType>("Shoe", out var complex) &&
            //     complex.Properties.TryFind("color", out var prop) &&
            //     model.TryResolve<EnumType>(prop.Type, out var color)
            // )
            // {
            //     Console.WriteLine("found self/Show/color's type: {0}", color);
            // }
            // else
            // {
            //     Console.WriteLine("couldn't find self/Show/color's type");
            // }
        }
    }
}