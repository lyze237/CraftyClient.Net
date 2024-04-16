using CraftyClientNet.Endpoints.Roles;
using CraftyClientNet.Endpoints.Users;
using CraftyClientNet.Exceptions;

namespace CraftyClientTests;

[Parallelizable(ParallelScope.All)]
public class RoleTests : CraftyTest
{
    [Test]
    public async Task GetAllRoles()
    {
        await using var scope = await TestScope.Create();

        await scope.Crafty.Api.CreateRole(new CreateRole.Request("hello"));
        await scope.Crafty.Api.CreateRole(new CreateRole.Request("world"));

        var roles = await scope.Crafty.Api.GetRoles();
        Assert.That(roles, Has.Length.EqualTo(2));
    }

    [Test]
    public async Task GetRole()
    {
        await using var scope = await TestScope.Create();

        var role = await scope.Crafty.Api.CreateRole(new CreateRole.Request("hello"));

        var roleResponse = await scope.Crafty.Api.GetRole(new GetRole.Request(role.RoleId));
        Assert.That(roleResponse.RoleName, Is.EqualTo("hello"));
    }

    [Test]
    [TestCase("Test")]
    [TestCase("$%^&")]
    public async Task CreateRoles(string name)
    {
        await using var scope = await TestScope.Create();

        var response = await scope.Crafty.Api.CreateRole(new CreateRole.Request(name));
        Assert.That(response.RoleId, Is.Not.Zero);
    }

    [Test]
    [TestCase("")]
    public async Task CreateInvalidRoles(string name)
    {
        await using var scope = await TestScope.Create();

        Assert.CatchAsync<InvalidJsonException<CreateRole.Response>>(async () =>
            await scope.Crafty.Api.CreateRole(new CreateRole.Request(name)));
    }

    [Test]
    public async Task DeleteRole()
    {
        await using var scope = await TestScope.Create();

        var role = await scope.Crafty.Api.CreateRole(new CreateRole.Request("hewwo"));
        var deleteRole = await scope.Crafty.Api.DeleteRole(new DeleteRole.Request(role.RoleId));
        Assert.That(deleteRole, Is.EqualTo(role.RoleId));
    }

    [Test]
    public async Task GetARolesUsers()
    {
        await using var scope = await TestScope.Create();

        var managerRole = await scope.Crafty.Api.CreateRole(new CreateRole.Request("managerrole"));
        var userRole = await scope.Crafty.Api.CreateRole(new CreateRole.Request("userrole"));

        var managerUser = await scope.Crafty.Api.CreateUser(new CreateUser.Request("manageruser", "coolpw123!",
            "manager@example.com", Roles: [managerRole.RoleId]));
        var userUser = await scope.Crafty.Api.CreateUser(new CreateUser.Request("useruser", "coolpw123!",
            "user@example.com", Manager: managerUser.UserId, Roles: [userRole.RoleId]));

        var managerUsers = await scope.Crafty.Api.GetARolesUsers(new GetARolesUsers.Request(managerRole.RoleId));
        Assert.That(managerUsers[0], Is.EqualTo(managerUser.UserId));

        var userUsers = await scope.Crafty.Api.GetARolesUsers(new GetARolesUsers.Request(userRole.RoleId));
        Assert.That(userUsers[0], Is.EqualTo(userUser.UserId));
    }
}