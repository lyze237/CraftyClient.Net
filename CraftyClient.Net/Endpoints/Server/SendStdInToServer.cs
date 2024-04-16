using System.Text.Json.Serialization;
using CraftyClientNet.Converters;
using CraftyClientNet.Extensions;
using CraftyClientNet.Models;
using CraftyClientNet.Models.Permissions;
using RestSharp;

namespace CraftyClientNet.Endpoints.Server;

public static class SendStdInToServer
{
    public record Request(string ServerId, string Message);
    
    public class Handler(Request request) : ICraftyRequestHandler<Response>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/servers/{id}/stdin", Method.Post)
                .AddUrlSegment("id", request.ServerId)
                .AddBody(request.Message);
    }

    public record Response;
}