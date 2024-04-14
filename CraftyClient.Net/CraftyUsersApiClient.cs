using CraftyClientNet.Endpoints.Users;

namespace CraftyClientNet;

public partial class CraftyApiClient
{
    public async Task<GetUsers.Response[]> GetUsers() =>
        await ExecuteAsync(new GetUsers.Handler());

    public async Task<CreateUser.Response> CreateUser(CreateUser.Request user) =>
        await ExecuteAsync(new CreateUser.Handler(user));
    
    public async Task<GetUser.Response> GetUser(GetUser.Request user) =>
        await ExecuteAsync(new GetUser.Handler(user));
    
    public async Task<DeleteUser.Response> DeleteUser(DeleteUser.Request user) =>
        await ExecuteAsync(new DeleteUser.Handler(user));

    public async Task<GetUserPermissions.Response> GetUserPermissions(GetUserPermissions.Request user) =>
        await ExecuteAsync(new GetUserPermissions.Handler(user));
}