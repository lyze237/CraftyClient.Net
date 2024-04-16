using RestSharp;

namespace CraftyClientNet.Endpoints.Roles;

public static class DeleteRole
{
    public record Request(int RoleId);
    
    public class Handler(Request request) : ICraftyRequestHandler<int>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/roles/{id}", Method.Delete)
                .AddUrlSegment("id", request.RoleId);
    }
}