using Microsoft.AspNetCore.OpenApi;
using OData.Lite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddSingleton<IODataService, DemoService>();

var app = builder.Build();
// if (app.Environment.IsDevelopment())
// {
//         app.UseSwagger();
//         app.UseSwaggerUI();
// }
if (!Model.TryLoad("example89.csdl.xml", out var model))
{
    return;
};
var urlSpace = UrlSpace.From(model, model.Schemas.First());
var o = new ODataService(urlSpace, app);

o.MapGet("/products", () => Results.Ok());
o.MapGet("/products/{id}", () => Results.Ok());

app.Run();

class ODataService
{
    public ODataService(UrlSpace urlSpace, WebApplication webApplication)
    {
        this.urlSpace = urlSpace;
        this.expected = urlSpace.Flatten().Select(p => ("/" + string.Join("/", p.Item1), false)).ToDictionary(p => p.Item1, p => p.Item2);
        this.webApplication = webApplication;

        this.webApplication.MapGet("/tree", () => Results.Text(urlSpace.ToString()));
        this.webApplication.MapGet("/", () => Results.Text(string.Join("\n", expected)));
    }

    private readonly UrlSpace urlSpace;
    private readonly Dictionary<string, bool> expected;
    private readonly WebApplication webApplication;

    public ODataService MapGet(string template, Delegate handler)
    {
        // if (this.expected.TryGetValue(template, out var val) ? val : false)
        {
            this.expected[template] = true;
            // throw new InvalidOperationException($"the route {template} is not supported byt this service");
        }
        this.webApplication.MapGet(template, handler);

        return this;
    }
}