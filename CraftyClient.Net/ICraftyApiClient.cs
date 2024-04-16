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
    Task<GetRoles.Response[]> GetRoles();
    Task<GetRole.Response> GetRole(GetRole.Request request);
    Task<CreateRole.Response> CreateRole(CreateRole.Request request);
    Task<int[]> GetARolesUsers(GetARolesUsers.Request request);
    Task<int> DeleteRole(DeleteRole.Request request);
    
    
    Task<GetServers.Response[]> GetServers();
    Task<StartServerResponse> CreateServer(StartServerModel request);
    Task<bool> WaitForServerImport(string serverId, CancellationToken cancellationToken);
    Task<bool> WaitForServerStart(string serverId, CancellationToken cancellationToken);
    Task<GetServerStats.Response> GetServerStats(GetServerStats.Request request);
    Task<SendActionToServer.Response> SendActionToServer(SendActionToServer.Request request);
    Task<DeleteServer.Response> DeleteServer(DeleteServer.Request request);
    Task<SendStdInToServer.Response> SendStdInToServer(SendStdInToServer.Request request);
    Task<string[]> GetServerLogs(GetServerLogs.Request request);

    Task<GetServerPublicData.Response> GetServerPublicData(GetServerPublicData.Request request);

    Task<GetUsers.Response[]> GetUsers();
    Task<CreateUser.Response> CreateUser(CreateUser.Request request);
    Task<GetUser.Response> GetUser(GetUser.Request request);
    Task<DeleteUser.Response> DeleteUser(DeleteUser.Request request);
    Task<GetUserPermissions.Response> GetUserPermissions(GetUserPermissions.Request request);
    // Task ModifyUser();
}