using OData.Lite;
using System.Collections.Immutable;

internal class Program
{

    private static void Main()
    {
        // // Log.AddConsole();

        var cliArgs = Environment.GetCommandLineArgs();
        var filename = cliArgs.Length > 1 ? cliArgs[1] : "example89.csdl.xml";
        using var file = File.OpenText(filename);

        if (Model.TryLoad(file, out var model))
        {
            // Console.WriteLine(model); ;
            // Console.WriteLine("\n==========================================================\n");

            // CsdlWriter.Write("output.csdl.xml", model);

            if (model.Schemas.TryFind("ODataDemo", out var schema))
            {
                var urlSpace = UrlSpace.From(model, schema, 3);
                urlSpace.Display(Console.Out);


                using var writer = File.CreateText("../OData.Lite.Web/mapping.cs");

                writer.WriteLine("public static class AppExtensions");
                writer.WriteLine("{");
                writer.WriteLine("    public static void MapService(this WebApplication app)");
                writer.WriteLine("    {");

                foreach (var (rawPath, type) in urlSpace.Flatten())
                {
                    var path = MakeKeyNamesUnique(rawPath).ToList();
                    var keys = path.WhereSelect<string, string>(StringExtensions.TryGetKeyName);

                    Console.WriteLine("// /{0}: {1}", string.Join("/", path), type);

                    // var urlTemplate = string.Join("/", path);
                    // var @params = string.Join(", ", from key in keys select $"string {key}");
                    // var dict = from key in keys select $"[\"{key}\"]={key}";
                    // writer.WriteLine(
                    //     $"        app.MapGet(\"{urlTemplate}\", \n\t\t\t(IODataService service{(@params.Any() ? ", " : "")}{@params}) => \n\t\t\t\tservice.Get(\"{urlTemplate}\", new Dictionary<string, string> {{ {string.Join(",", dict)} }}, \"{type}\"));"
                    // );
                }

                writer.WriteLine("    }");
                writer.WriteLine("}");
            }

            // Console.WriteLine("\n==========================================================\n");
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

    static IEnumerable<string> MakeKeyNamesUnique(ImmutableList<string> path)
    {
        var keys = new Counter<string>();
        foreach (var segment in path)
        {
            if (segment.TryGetKeyName(out var key))
            {
                if (keys[key] > 0)
                {
                    yield return $"{{{key + keys[key]}}}";
                }
                else
                {
                    yield return $"{{{key}}}";
                }
                keys[key] += 1;
            }
            else
            {
                yield return segment;
            }
        }
    }
}

public class Counter<T> where T : notnull
{
    private readonly Dictionary<T, ulong> counts = new();

    public ulong this[T key]
    {
        get => counts.TryGetValue(key, out var count) ? count : 0;
        set => counts[key] = value;
    }
}