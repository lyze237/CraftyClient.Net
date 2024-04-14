using RestSharp;

namespace CraftyClientNet.Endpoints.Roles;

public static class GetRoles
{
    public class Handler() : ICraftyRequestHandler<Response>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/roles", Method.Post);
    }

    public record Response(int RoleId, string Created, string LastUpdated, string RoleName);
}