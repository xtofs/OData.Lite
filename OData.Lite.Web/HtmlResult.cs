using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Http;

static class ResultsExtensions
{
    public static IResult Html(this IResultExtensions resultExtensions, string html)
    {
        ArgumentNullException.ThrowIfNull(resultExtensions);

        return new HtmlResult(html);
    }

    public static IResult Table(this IResultExtensions resultExtensions, IEnumerable<IReadOnlyList<string>> data)
    {
        ArgumentNullException.ThrowIfNull(resultExtensions);

        var rows = from row in data select $"<tr>{string.Join(" ", from d in row select $"<td>{d}</td>")}</tr>";

        return new HtmlResult($"<table>{string.Join("\n", rows)}</table>");
    }
}


class HtmlResult : IResult
{
    private readonly string _html;

    public HtmlResult(string html)
    {
        _html = html;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.ContentType = MediaTypeNames.Text.Html;
        httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(_html);
        return httpContext.Response.WriteAsync(_html);
    }
}