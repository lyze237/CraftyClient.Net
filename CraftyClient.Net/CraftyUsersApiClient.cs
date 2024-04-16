using CraftyClientNet.Endpoints.Users;

namespace CraftyClientNet;

public partial class CraftyApiClient
{
    public async Task<GetUsers.Response[]> GetUsers() =>
        await ExecuteAsync(new GetUsers.Handler());

    public async Task<CreateUser.Response> CreateUser(CreateUser.Request request) =>
        await ExecuteAsync(new CreateUser.Handler(request));
    
    public async Task<GetUser.Response> GetUser(GetUser.Request request) =>
        await ExecuteAsync(new GetUser.Handler(request));
    
    public async Task<DeleteUser.Response> DeleteUser(DeleteUser.Request request) =>
        await ExecuteAsync(new DeleteUser.Handler(request));

    public async Task<GetUserPermissions.Response> GetUserPermissions(GetUserPermissions.Request request) =>
        await ExecuteAsync(new GetUserPermissions.Handler(request));
}