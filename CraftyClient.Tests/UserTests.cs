using CraftyClientNet.Endpoints.Users;

namespace CraftyClientTests;

public class UserTests : CraftyTest
{
    [Test]
    [TestCase("test", "securepassword123!", "test1@example.com")]
    [TestCase("test2", "securepassword123!", "test2@example.com")]
    [TestCase("test3", "securepassword123!", "test3@example.com")]
    [Parallelizable(ParallelScope.All)]
    public async Task CreateUser(string username, string password, string email)
    {
        await using var scope = await TestScope.Create();

        var response = await scope.Crafty.Api.ExecuteAsync(new CreateUser.Handler(new CreateUser.Request(username, password, email)));
        Assert.That(response.UserId, Is.Not.Zero);
    }
}