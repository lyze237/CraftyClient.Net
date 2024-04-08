using CraftyClientNet.Models.Requests;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet;

public partial class CraftyApiClient
{
    public Task<BasicUserResponse[]> GetUsers() => 
        ExecuteAsync<BasicUserResponse[]>(new RestRequest("api/v2/users"));

    public Task<CreateUserResponse> CreateUser(CreateUserModel user) =>
        ExecuteAsync<CreateUserResponse>(new RestRequest("api/v2/users", Method.Post)
            .AddJsonBody(user));

    public Task<ExtendedUserResponse> GetUser(int userId) =>
        ExecuteAsync<ExtendedUserResponse>(new RestRequest("api/v2/users/{id}").AddUrlSegment("id", userId));

    public Task DeleteUser(int userId) => 
        ExecuteAsync(new RestRequest("api/v2/users/{id}", Method.Delete).AddUrlSegment("id", userId));

    public Task<GetUserPermissionsResponse> GetUserPermissions(int userId) =>
        ExecuteAsync<GetUserPermissionsResponse>(new RestRequest("api/v2/users/{id}/permissions").AddUrlSegment("id", userId));
}