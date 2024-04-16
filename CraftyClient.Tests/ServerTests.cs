using System.ComponentModel.DataAnnotations;
using CraftyClientNet.Endpoints.Server;
using CraftyClientNet.Models.Requests;
using CraftyClientNet.Models.Responses;
using MineStatLib;

namespace CraftyClientTests;

public class ServerTests : CraftyTest
{
    [Test]
    public async Task GetServers()
    {
        await using var scope = await TestScope.Create();

        for (var i = 0; i < 5; i++)
        {
            await scope.Crafty.Api.CreateServer(
                new StartJavaServerModel("Test",
                    new JavaDownloadJarData("vanilla", "vanilla", "1.20.4", 2, 4, 25565, true))
                {
                    Autostart = true
                });
        }

        var response = await scope.Crafty.Api.GetServers();
        Assert.That(response, Has.Length.EqualTo(5));
        Assert.That(response[0].ServerName, Is.EqualTo("Test"));
    }

    [Test]
    public async Task DeleteServer()
    {
        await using var scope = await TestScope.Create();

        var server = await SpinUpServer(scope.Crafty,
            new StartJavaServerModel("Test", new JavaDownloadJarData("vanilla", "vanilla", "1.20.4", 2, 4, 25565, true))
            {
                Autostart = true
            });

        var response = await scope.Crafty.Api.GetServers();
        Assert.That(response, Has.Length.EqualTo(1));

        await scope.Crafty.Api.DeleteServer(new DeleteServer.Request(server.NewServerUuid));

        response = await scope.Crafty.Api.GetServers();
        Assert.That(response, Has.Length.EqualTo(0));
    }

    [Test]
    public async Task SendStdInToServer()
    {
        await using var scope = await TestScope.Create();

        var server = await SpinUpServer(scope.Crafty,
            new StartJavaServerModel("Test", new JavaDownloadJarData("vanilla", "vanilla", "1.20.4", 2, 4, 25565, true))
            {
                Autostart = true
            });

        await scope.Crafty.Api.WaitForServerDone(server.NewServerUuid, new CancellationTokenSource(TimeSpan.FromSeconds(20)).Token);
        
        await scope.Crafty.Api.SendStdInToServer(new SendStdInToServer.Request(server.NewServerUuid, "say kajwlhdkjawhdawjdhakjwhgdkjhawgdka"));

        await Task.Delay(1000);
        
        var logs = await scope.Crafty.Api.GetServerLogs(new GetServerLogs.Request(server.NewServerUuid));
        
        Assert.That(logs.Any(line => line.Contains("kajwlhdkjawhdawjdhakjwhgdkjhawgdka")));
    }

    [Test]
    public async Task SpinUpVanillaServer()
    {
        await using var scope = await TestScope.Create();

        var server = await SpinUpServer(scope.Crafty,
            new StartJavaServerModel("Test", new JavaDownloadJarData("vanilla", "vanilla", "1.20.4", 2, 4, 25565, true))
            {
                Autostart = true
            });

        await scope.Crafty.Api.WaitForServerDone(server.NewServerUuid, new CancellationTokenSource(TimeSpan.FromSeconds(20)).Token);
        
        var mineStat = new MineStat("localhost", (ushort)scope.Crafty.JavaPort);

        Assert.Multiple(() =>
        {
            Assert.That(mineStat.ServerUp, Is.True);
            Assert.That(mineStat.Motd, Is.EqualTo("\"A Minecraft Server\""));
        });
    }


    [Test]
    public async Task SpinUpForgeServer()
    {
        await using var scope = await TestScope.Create();

        await SpinUpServer(scope.Crafty,
            new StartJavaServerModel("Test", new JavaDownloadJarData("modded", "forge", "1.20.4", 2, 4, 25565, true))
            {
                Autostart = true
            });
    }

    private async Task<StartServerResponse> SpinUpServer(CraftyContainer crafty, StartServerModel command)
    {
        var result = await crafty.Api.CreateServer(command);

        Assert.That(
            await crafty.Api.WaitForServerImport(result.NewServerUuid,
                new CancellationTokenSource(TimeSpan.FromSeconds(20)).Token), Is.True);

        await crafty.Api.SendActionToServer(new SendActionToServer.Request(result.NewServerUuid,
            SendActionToServer.ServerAction.StartServer));

        Assert.That(
            await crafty.Api.WaitForServerStart(result.NewServerUuid,
                new CancellationTokenSource(TimeSpan.FromSeconds(20)).Token), Is.True);

        return result;
    }
}