using CraftyClientNet.Models.Permissions;
using RestSharp;

namespace CraftyClientNet.Endpoints.Roles;

public static class CreateRole
{
    public record Request(string Name, (string ServerId, ServerPermissions Permissions)[]? Servers = null);
    
    public class Handler(Request request) : ICraftyRequestHandler<Response>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/roles", Method.Post)
                .AddJsonBody(new
                {
                    request.Name,
                    servers = request.Servers ?? []
                });
    }

    public record Response(int RoleId);
}