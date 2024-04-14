using System.Text.Json.Serialization;
using CraftyClientNet.Models.Permissions;
using RestSharp;

namespace CraftyClientNet.Endpoints.Users;

public static class GetUserPermissions
{
    public record Request(int UserId);

    public class Handler(Request user) : ICraftyRequestHandler<Response>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/users/{id}/permissions").AddUrlSegment("id", user.UserId);
    }

    public record Response(ServerPermissions Permissions, Dictionary<string, int> Counters, Dictionary<string, int> Limits);
}