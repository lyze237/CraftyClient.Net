using CraftyClientNet.Endpoints.Users;

namespace CraftyClientTests;

[Parallelizable(ParallelScope.All)]
public class UserTests : CraftyTest
{
    [Test]
    [TestCase("test", "securepassword123!", "test1@example.com")]
    [TestCase("test2", "securepassword123!", "test2@example.com")]
    [TestCase("test3", "securepassword123!", "test3@example.com")]
    public async Task CreateUser(string username, string password, string email)
    {
        await using var scope = await TestScope.Create();

        var response = await scope.Crafty.Api.ExecuteAsync(new CreateUser.Handler(new CreateUser.Request(username, password, email)));
        Assert.That(response.UserId, Is.Not.Zero);
    }

    [Test]
    public async Task GetUsers()
    {
        await using var scope = await TestScope.Create();

        var user = await scope.Crafty.Api.ExecuteAsync(new CreateUser.Handler(new CreateUser.Request("user", "coolpw123!", "user@example.com")));

        var users = await scope.Crafty.Api.GetUsers();

        Assert.That(users, Has.Length.EqualTo(2));
    }

    [Test]
    public async Task GetUser()
    {
        await using var scope = await TestScope.Create();

        var user = await scope.Crafty.Api.ExecuteAsync(new CreateUser.Handler(new CreateUser.Request("user", "coolpw123!", "user@example.com")));

        var userResponse = await scope.Crafty.Api.GetUser(new GetUser.Request(user.UserId));

        Assert.That(userResponse.Username, Is.EqualTo("user"));
    }

    [Test]
    public async Task DeleteUser()
    {
        await using var scope = await TestScope.Create();

        var users = await scope.Crafty.Api.GetUsers();
        Assert.That(users, Has.Length.EqualTo(1));
        
        var user = await scope.Crafty.Api.ExecuteAsync(new CreateUser.Handler(new CreateUser.Request("user", "coolpw123!", "user@example.com")));
        users = await scope.Crafty.Api.GetUsers();
        Assert.That(users, Has.Length.EqualTo(2));
        
        await scope.Crafty.Api.DeleteUser(new DeleteUser.Request(user.UserId));
        users = await scope.Crafty.Api.GetUsers();
        Assert.That(users, Has.Length.EqualTo(1));
    }
}