using System.Text.Json.Serialization;
using CraftyClientNet.Converters;
using CraftyClientNet.Extensions;
using CraftyClientNet.Models;
using CraftyClientNet.Models.Permissions;
using RestSharp;

namespace CraftyClientNet.Endpoints.Server;

public static class GetServerPublicData
{
    public record Request(string ServerId);

    public class Handler(Request server) : ICraftyRequestHandler<Response>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/servers/{id}/public")
                .AddUrlSegment("id", server.ServerId);
    }

    public record Response(
        string ServerId,
        DateTime Created,
        string ServerName,
        string Type);
}