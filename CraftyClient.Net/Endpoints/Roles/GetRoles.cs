using RestSharp;

namespace CraftyClientNet.Endpoints.Roles;

public static class GetRoles
{
    public class Handler : ICraftyRequestHandler<Response[]>
    {
        public RestRequest GenerateRequest() =>
            new("api/v2/roles");
    }

    public record Response(int RoleId, string Created, string LastUpdated, string RoleName, string? Manager);
}