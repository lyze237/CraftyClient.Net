using System.Text.Json.Serialization;
using CraftyClientNet.Converters;
using CraftyClientNet.Extensions;
using CraftyClientNet.Models;
using CraftyClientNet.Models.Permissions;
using RestSharp;

namespace CraftyClientNet.Endpoints.Server;

public static class GetServerLogs
{
    public record Request(string ServerId, bool File = false, bool Colors = false, bool Raw = false);
    
    public class Handler(Request request) : ICraftyRequestHandler<string[]>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/servers/{id}/logs")
                .AddUrlSegment("id", request.ServerId)
                .AddQueryParameter("file", request.File.ToString().ToLower())
                .AddQueryParameter("colors", request.Colors.ToString().ToLower())
                .AddQueryParameter("raw", request.Raw.ToString().ToLower());
    }
}