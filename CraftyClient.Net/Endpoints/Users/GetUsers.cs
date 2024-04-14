using RestSharp;

namespace CraftyClientNet.Endpoints.Users;

public class GetUsers
{
    public class Handler() : ICraftyRequestHandler<Response[]>
    {
        public RestRequest GenerateRequest() =>
            new RestRequest("api/v2/users");
    }

    public record Response(
        int UserId,
        string Created,
        string Username,
        bool Enabled,
        bool Superuser,
        string Lang
    );
}