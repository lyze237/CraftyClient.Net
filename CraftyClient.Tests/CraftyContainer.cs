using System.Text.Json;
using CraftyClientNet;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace CraftyClientTests;

public class CraftyContainer : IAsyncDisposable
{
    private IContainer container = null!;
    public ICraftyApiClient Api { get; private set; } = null!;

    public int JavaPort => container.GetMappedPublicPort(25565);

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task Setup()
    {
        const string passwordFile = "/crafty/app/config/default-creds.txt";

        container = new ContainerBuilder()
            .WithImage("arcadiatechnology/crafty-4:4.3.2")
            .WithPortBinding(8443, true)
            .WithPortBinding(25565, true)
            .WithWaitStrategy(Wait.ForUnixContainer()
                .UntilFileExists(passwordFile, FileSystem.Container)
                .UntilMessageIsLogged("Crafty has fully started and is now ready for use!")
                .UntilHttpRequestIsSucceeded(http => http.UsingTls().ForPort(8443).UsingHttpMessageHandler(
                    new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (_, _, _, _) => true
                    })))
            .WithAutoRemove(true)
            .Build();

        await container.StartAsync();

        var credentialsFile = await container.ReadFileAsync(passwordFile);
        var credentials = JsonSerializer.Deserialize<CraftyCredentials>(credentialsFile, JsonOptions);

        Assert.That(credentials, Is.Not.Null);
        Assert.That(credentials!.Username, Is.Not.Null);
        Assert.That(credentials.Password, Is.Not.Null);

        Api = new CraftyApiClient($"https://{container.Hostname}:{container.GetMappedPublicPort(8443)}",
            credentials.Username, credentials.Password, true)
        {
            LogRequests = true
        };
    }

    public async ValueTask DisposeAsync()
    {
        Api.Dispose();

        await container.StopAsync();
        await container.DisposeAsync();

        GC.SuppressFinalize(this);
    }
}

internal record CraftyCredentials(string Username, string Password);
