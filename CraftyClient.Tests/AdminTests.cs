using CraftyClientNet.Models;
using CraftyClientNet.Models.Requests;
using MineStatLib;

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

        var user = await crafty.Api.CreateUser(new CreateUserModel(username, "coolpw123!", email,
            ServerCreationPermission: 2));
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
    public async Task CreateVanillaServer()
    {
        await using var crafty = new CraftyContainer();
        await crafty.Setup();

        var result = await crafty.Api.CreateServer(
            new StartJavaServerModel("Test", new JavaDownloadJarData("vanilla", "vanilla", "1.20.4", 2, 4, 25565, true))
            {
                Autostart = true
            });

        Assert.That(
            await crafty.Api.WaitForServerImport(result.NewServerUuid,
                new CancellationTokenSource(TimeSpan.FromSeconds(20)).Token), Is.True);

        await crafty.Api.SendActionToServer(result.NewServerUuid, ServerAction.StartServer);

        Assert.That(
            await crafty.Api.WaitForServerStart(result.NewServerUuid,
                new CancellationTokenSource(TimeSpan.FromSeconds(20)).Token), Is.True);

        var mineStat = new MineStat("localhost", (ushort)crafty.JavaPort);

        Assert.Multiple(() =>
        {
            Assert.That(mineStat.ServerUp, Is.True);
            Assert.That(mineStat.Motd, Is.EqualTo("A Minecraft Server"));
        });
    }

    [Test]
    public async Task CreateForgeServer()
    {
        await using var crafty = new CraftyContainer();
        await crafty.Setup();

        var result = await crafty.Api.CreateServer(
            new StartJavaServerModel("Test", new JavaDownloadJarData("modded", "forge", "1.20.4", 2, 4, 25565, true))
            {
                Autostart = true
            });

        Assert.That(
            await crafty.Api.WaitForServerImport(result.NewServerUuid,
                new CancellationTokenSource(TimeSpan.FromSeconds(20)).Token), Is.True);

        await crafty.Api.SendActionToServer(result.NewServerUuid, ServerAction.StartServer);

        Assert.That(
            await crafty.Api.WaitForServerStart(result.NewServerUuid,
                new CancellationTokenSource(TimeSpan.FromSeconds(20)).Token), Is.True);
    }

    private bool CompareObjects(object a, object b)
    {
        var bType = b.GetType();
        var correct = true;

        foreach (var aProperty in a.GetType().GetProperties())
        {
            var bProperty = bType.GetProperty(aProperty.Name);
            var aValue = aProperty.GetValue(a);
            var bValue = bProperty?.GetValue(b);

            if (!object.Equals(aValue, bValue))
            {
                Console.WriteLine($"[{aProperty.Name}] A {aValue} is different to B {bValue}");
                correct = false;
            }
        }

        return correct;
    }
}