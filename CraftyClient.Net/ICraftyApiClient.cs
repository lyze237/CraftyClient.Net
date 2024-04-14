using CraftyClientNet.Endpoints;
using CraftyClientNet.Endpoints.Roles;
using CraftyClientNet.Endpoints.Server;
using CraftyClientNet.Endpoints.Users;
using CraftyClientNet.Models;
using CraftyClientNet.Models.Permissions;
using CraftyClientNet.Models.Requests;
using CraftyClientNet.Models.Responses;
using RestSharp;

namespace CraftyClientNet;

public interface ICraftyApiClient : IDisposable
{
    Task<GetRoles.Response> GetRoles();
    Task<GetRole.Response> GetRole(GetRole.Request role);
    Task<CreateRole.Response> CreateRole(CreateRole.Request role);
    
    Task<StartServerResponse> CreateServer(StartServerModel server);
    Task<bool> WaitForServerImport(string serverId, CancellationToken cancellationToken);
    Task<bool> WaitForServerStart(string serverId, CancellationToken cancellationToken);
    Task<GetServerStats.Response> GetServerStats(GetServerStats.Request server);
    
    Task<SendActionToServer.Response> SendActionToServer(SendActionToServer.Request action);

    Task<GetUsers.Response[]> GetUsers();
    Task<CreateUser.Response> CreateUser(CreateUser.Request user);
    Task<GetUser.Response> GetUser(GetUser.Request user);
    Task<DeleteUser.Response> DeleteUser(DeleteUser.Request user);
    Task<GetUserPermissions.Response> GetUserPermissions(GetUserPermissions.Request user);
    // Task ModifyUser();
}