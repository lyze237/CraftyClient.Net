using System.Text.Json.Serialization;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet.Endpoints.Users;

public static class GetUser
{
    public record Request(int UserId);

    public class Handler(Request user) : ICraftyRequestHandler<Response>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/users/{id}").AddUrlSegment("id", user.UserId);
    }

    public record Response(
        int UserId,
        string Created,
        DateTime LastLogin,
        string LastUpdate,
        string LastIp,
        string Username,
        string Email,
        bool Enabled,
        bool Superuser,
        string Lang,
        string SupportLogs,
        string ServerOrder,
        bool Preparing,
        bool Hints,
        Role[] Roles
    );
}