using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Transport;
using Shouldly;

namespace McpServer.Tests;

public class Tests
{
    private StdioClientTransport _clientTransport;

    [SetUp]
    public void Setup()
    {
        var (command, arguments) = GetCommandAndArguments();

        _clientTransport = new StdioClientTransport(new()
        {
            Name = "CSharpSandbox",
            Command = command,
            Arguments = arguments,
        });   
    }

    [Test]
    public async Task CSharpSandboxToolsTest()
    {
        await using var mcpClient = await McpClientFactory.CreateAsync(_clientTransport);

        var tools = await mcpClient.ListToolsAsync();

        tools.Count.ShouldBe(1);
        tools.All(t => String.IsNullOrWhiteSpace(t.Name)).ShouldBeFalse();
        tools.All(t => String.IsNullOrWhiteSpace(t.Description)).ShouldBeFalse();
    }

    private static (string command, string[] arguments) GetCommandAndArguments()
    {
        return ("dotnet", ["run", "--project", "../../../../../src/MCPServer/MCPServer.csproj"]);
    }
}
