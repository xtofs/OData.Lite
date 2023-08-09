using OData.Lite;
using System.Collections.Immutable;

internal class Program
{
    private static void Main()
    {

        OData.Lite.Logging.InitConsole();

        var cliArgs = Environment.GetCommandLineArgs();
        var filename = cliArgs.Length > 1 ? cliArgs[1] : "example90.csdl.xml";
        using var file = File.OpenText(filename);

        if (Model.TryLoad(file, out var model))
        {

            Console.WriteLine(model); ;
            Console.WriteLine("\n==========================================================\n");

            // CsdlWriter.Write("output.csdl.xml", model);

            //if (model.Schemas.TryFind("ODataDemo", out var schema))
            //{
            //    var urlSpace = UrlSpace.From(model, schema, 3);
            //    urlSpace.Display(Console.Out);

            //    foreach (var path in urlSpace.Paths())
            //    {
            //        Console.WriteLine(string.Join("/", from seg in path select seg.Name));
            //    }
            //}

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