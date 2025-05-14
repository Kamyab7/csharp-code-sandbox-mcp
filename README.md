# C# Code Sandbox MCP

A C#–based Model Context Protocol (MCP) server that spins up disposable Docker containers to execute arbitrary C# scripts.

## Prerequisites

This project depends on the [dotnet-script](https://github.com/dotnet-script/dotnet-script) tool. Ensure it is installed globally on your system:

```bash
# Install dotnet-script globally
> dotnet tool install dotnet-script -g
```

## Building the Project

To build the project, navigate to the root directory and run:

```bash
> dotnet build
```

## Running the Server

To run the server, you need to configure the AI agent with the following settings:

```json
"mcp": {
    "servers": {
        "CSharpSandBox": {
            "type": "stdio",
            "command": "dotnet",
            "args": [
                "run",
                "--project",
                "{PROJECT-PATH}\\csharp-code-sandbox-mcp\\src\\MCPServer\\MCPServer.csproj",
                "--no-build"
            ]
        }
    }
}
```

Replace `{PROJECT-PATH}` with the absolute path to the project directory.

## Features

- Executes arbitrary C# scripts in isolated Docker containers.
- Provides a secure and disposable environment for script execution.

## Acknowledgments

Thanks to the project [node-code-sandbox-mcp](https://github.com/alfonsograziano/node-code-sandbox-mcp) by [alfonsograziano](https://github.com/alfonsograziano) for the inspiration behind this work.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.