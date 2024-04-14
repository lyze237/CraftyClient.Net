using System.Text.Json.Serialization;
using RestSharp;

namespace CraftyClientNet.Endpoints.Users;

public static class DeleteUser
{
    public record Request(int UserId);

    public class Handler(Request user) : ICraftyRequestHandler<Response>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/users/{id}", Method.Delete).AddUrlSegment("id", user.UserId);
    }
    
    public record Response;
}