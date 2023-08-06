using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using OData.Lite;


public class ODataApplication
{
    public WebApplication WebApplication { get; }
    public UrlSpace UrlSpace { get; }
    public Dictionary<string, bool> Expected { get; }
    public Model Model { get; }
    public Schema Schema { get; }

    public static ODataApplication Build(WebApplicationBuilder builder, Model model, Schema schema)
    {
        var urlSpace = UrlSpace.From(model, schema);
        //var paths = urlSpace.Paths();
        var paths = urlSpace.Paths().Select(p => "/" + string.Join("/", from s in p select s.Name));
        var expected = paths.ToDictionary(p => p, _ => false);

        var app = builder.Build();

        // if (app.Environment.IsDevelopment())
        {
            app.MapGet("/", () => Results.Extensions.Table(
                from p in expected select new[] {
                    string.Join("/", p.Key), 
                    p.Value ? "registered" : "missing" 
                }
            ));
            app.MapGet("/urlspace", () => Results.Text(urlSpace.ToString()));

        }
        return new ODataApplication(app, model, schema, urlSpace, expected);
    }

    private ODataApplication(WebApplication webApplication, Model model, Schema schema, UrlSpace urlSpace, Dictionary<string, bool> expected)
    {
        WebApplication = webApplication;
        Model = model;
        Schema = schema;
        UrlSpace = urlSpace;
        Expected = expected;
    }

    public ODataApplication MapGet(string template, Delegate action)
    {
        WebApplication.MapGet(template, action);
        Expected[template] = true;

        return this;
    }

    public void Run()
    {
        var missing = Expected.Count(e => !e.Value);
        if (missing > 0)
        {
            WebApplication.Logger.LogError($"{missing} missing registrations, see http://localhost for details");
        }
        foreach (var p in Expected.Where(p => !p.Value).Select(p => p.Key))
        {
            WebApplication.MapGet(p, () => Results.Text($"{p} not implemented"));
        }

        WebApplication.Run();
    }

}
