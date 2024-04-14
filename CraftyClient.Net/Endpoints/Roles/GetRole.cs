using RestSharp;

namespace CraftyClientNet.Endpoints.Roles;

public static class GetRole
{
    public record Request(int RoleId);
    
    public class Handler(Request role) : ICraftyRequestHandler<Response>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/roles/{id}")
                .AddUrlSegment("id", role.RoleId);
    }

    public record Response(int RoleId, string Created, string LastUpdated, string RoleName);
}