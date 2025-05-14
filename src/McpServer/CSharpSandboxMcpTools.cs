using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Diagnostics;

namespace McpServer;

[McpServerToolType]
internal sealed class CSharpSandboxMcpTools
{
    [McpServerTool, Description("""
        This method executes the provided C# script using the dotnet-script tool.
        It creates a unique file for the script, runs it,
        and returns the output or throws an exception if execution fails.
    """)]
    public async Task<string> ExecuteScriptAsync([Description("""
        The script to execute

        Example script:
            Console.WriteLine("Hello, world!");
        
            Note: There is no need to write a main function, just provide the C# code.
        """)] string script)
    {
        string fileName = await CreateScriptFileAsync(script);
        return await RunScriptAsync(fileName);
    }

    private async Task<string> CreateScriptFileAsync(string script)
    {
        // Generate a unique GUID for the filename
        string guid = Guid.NewGuid().ToString();
        string fileName = $"scripts/{guid}.csx";

        // Ensure the scripts directory exists
        Directory.CreateDirectory("scripts");

        // Write the script content to the file
        await File.WriteAllTextAsync(fileName, script);

        return fileName;
    }

    private async Task<string> RunScriptAsync(string fileName)
    {
        // Prepare the process to execute the script
        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"script {fileName}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using Process process = new Process { StartInfo = processStartInfo };
        process.Start();

        // Capture the output
        string output = await process.StandardOutput.ReadToEndAsync();
        string error = await process.StandardError.ReadToEndAsync();

        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new Exception($"Script execution failed: {error}");
        }

        return output;
    }
}