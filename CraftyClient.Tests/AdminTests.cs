using CraftyClientNet.Models;
using CraftyClientNet.Models.Requests;

namespace CraftyClientTests;

[Parallelizable(ParallelScope.Self)]
public class AdminTests
{
    [Test]
    public async Task LoginTest()
    {
        await using var crafty = new CraftyContainer();
        await crafty.Setup();
        
        Assert.DoesNotThrowAsync(async () => await crafty.Api.GetRoles());
    }

    [Test]
    public async Task CreateRoleTest()
    {
        await using var crafty = new CraftyContainer();
        await crafty.Setup();
        
        var createRoleResponse = await crafty.Api.CreateRole("Test");
        Assert.That(createRoleResponse.RoleId, Is.EqualTo(1));

        var rolesResponse = await crafty.Api.GetRoles();
        Assert.That(rolesResponse.Length, Is.EqualTo(1));
        Assert.That(rolesResponse.First().RoleName, Is.EqualTo("test"));

        var roleResponse = await crafty.Api.GetRole(rolesResponse.First().RoleId);
        Assert.That(rolesResponse.First(), Is.EqualTo(roleResponse));
    }

    [Test]
    public async Task UserTest()
    {
        await using var crafty = new CraftyContainer();
        await crafty.Setup();

        var username = "lyze237";
        var email = "cool@example.com";
        
        var user = await crafty.Api.CreateUser(new CreateUserModel(username, "coolpw123!", email, ServerCreationPermission: 2));
        var users = await crafty.Api.GetUsers();
        
        Assert.That(users.Select(u => u.UserId).ToArray(), Does.Contain(user.UserId));
        Assert.That(users.Select(u => u.Username).ToArray(), Does.Contain(username));
        
        var extendedUser = await crafty.Api.GetUser(user.UserId);
        Assert.That(extendedUser.Username, Is.EqualTo(username));
        Assert.That(extendedUser.Email, Is.EqualTo(email));

        var userPermissions = await crafty.Api.GetUserPermissions(user.UserId);
        
        await crafty.Api.DeleteUser(user.UserId);
        users = await crafty.Api.GetUsers();
        Assert.That(users.Select(u => u.UserId).ToArray(), Does.Not.Contain(user.UserId));
        
        Console.WriteLine(users);
    }

    [Test]
    public async Task CreateServer()
    {
        await using var crafty = new CraftyContainer();
        await crafty.Setup();
        
        var result = await crafty.Api.CreateServer(
            new StartJavaServerModel("Test", new JavaDownloadJarData("vanilla", "vanilla", "1.20.4", 1024, 1024 * 4, 25565, true)) 
            {
                Autostart = true
            });

        Console.WriteLine("Before Start:");
        await crafty.Api.GetServerStats(result.NewServerUuid);
        Console.WriteLine("Starting Server:");
        await crafty.Api.SendActionToServer(result.NewServerUuid, ServerAction.StartServer);
        Console.WriteLine("After Start:");
        await crafty.Api.GetServerStats(result.NewServerUuid);

        Console.WriteLine("Waiting for 60 Seconds");
        await Task.Delay(60 * 1000);

        Console.WriteLine("Before Start:");
        await crafty.Api.GetServerStats(result.NewServerUuid);
        Console.WriteLine("Starting Server:");
        await crafty.Api.SendActionToServer(result.NewServerUuid, ServerAction.StartServer);
        Console.WriteLine("After Start:");
        await crafty.Api.GetServerStats(result.NewServerUuid);
        
        Console.WriteLine("Waiting for 60 Seconds");
        await Task.Delay(60 * 1000);
        Console.WriteLine("Final check:");
        await crafty.Api.GetServerStats(result.NewServerUuid);
    }
}