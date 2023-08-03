
using Microsoft.AspNetCore.Http;

public interface IODataService
{
    public IResult Get(string template, IReadOnlyDictionary<string, string> keys);
}