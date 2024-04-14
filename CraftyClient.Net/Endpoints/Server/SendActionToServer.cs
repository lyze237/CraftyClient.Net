using CraftyClientNet.Extensions;
using CraftyClientNet.Models;
using CraftyClientNet.Models.Permissions;
using RestSharp;

namespace CraftyClientNet.Endpoints.Server;

public static class SendActionToServer
{
    public record Request(string ServerId, ServerAction Action);
    
    public enum ServerAction
    {
        CloneServer, StartServer, StopServer, RestartServer, KillServer, BackupServer, UpdateExecutable
    }

    public class Handler(Request action) : ICraftyRequestHandler<Response>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/servers/{id}/action/{action}", Method.Post)
                .AddUrlSegment("id", action.ServerId)
                .AddUrlSegment("action", action.Action.To_snake_case());
    }

    public record Response();
}