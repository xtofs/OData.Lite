
using Microsoft.AspNetCore.Http;

public interface IODataService
{
    IResult Get(string template, IReadOnlyDictionary<string, string> keys, string resultType);


}


// public interface IGetService<T>
// {
//     OData<T> Get(string urlPath, IReadOnlyDictionary<string, string> keys);

// }