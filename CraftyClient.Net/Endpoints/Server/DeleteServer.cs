using System.Text.Json.Serialization;
using CraftyClientNet.Converters;
using CraftyClientNet.Extensions;
using CraftyClientNet.Models;
using CraftyClientNet.Models.Permissions;
using RestSharp;

namespace CraftyClientNet.Endpoints.Server;

public static class DeleteServer
{
    public record Request(string ServerId);
    
    public class Handler(Request request) : ICraftyRequestHandler<Response>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/servers/{id}", Method.Delete)
                .AddUrlSegment("id", request.ServerId);
    }

    public record Response;
}