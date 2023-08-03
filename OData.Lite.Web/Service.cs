
using Microsoft.AspNetCore.Http;

class Demo : IODataService
{

    public IResult Get(string template, IReadOnlyDictionary<string, string> parameters)
    {
        return Results.Json(new
        {
            template = template,
            parameters = parameters
        });
    }
}