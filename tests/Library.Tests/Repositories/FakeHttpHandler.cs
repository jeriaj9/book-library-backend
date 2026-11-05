using System.Net;
using System.Text;

namespace Library.Tests.Repositories;

public class FakeHttpHandler : HttpMessageHandler
{
    private readonly HttpStatusCode _statusCode;
    private readonly string _content;

    public HttpRequestMessage? LastRequest { get; private set; }

    public FakeHttpHandler(HttpStatusCode statusCode, string content = "")
    {
        _statusCode = statusCode;
        _content = content;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        LastRequest = request;

        return Task.FromResult(new HttpResponseMessage(_statusCode)
        {
            Content = new StringContent(_content, Encoding.UTF8, "application/json")
        });
    }
}
