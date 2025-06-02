using System.Diagnostics;
namespace CapitalGains.EndToEnd;

public class StockServiceTestEndToEnd
{
    [Theory(DisplayName = nameof(Validate_CapitalGainsConsole_InputFile))]
    [Trait("EndToEnd", "Stock - Console")]
    [InlineData("./inputs/input.case#1.json")]
    [InlineData("./inputs/input.case#2.json")]
    [InlineData("./inputs/input.case#3.json")]
    [InlineData("./inputs/input.case#4.json")]
    [InlineData("./inputs/input.case#5.json")]
    [InlineData("./inputs/input.case#6.json")]
    [InlineData("./inputs/input.case#7.json")]
    [InlineData("./inputs/input.case#8.json")]
    [InlineData("./inputs/input.case#9.json")]
    [InlineData("./inputs/input.case#1+case#2.json")]
    public async Task Validate_CapitalGainsConsole_InputFile(string inputPath)
    {
        var projectPath = "../../../../../../src/1-Presentation/CapitalGains.Console/CapitalGains.Console.csproj";

        var processStartInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"run --project {projectPath}",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = processStartInfo };
        process.Start();

        var inputContent = await File.ReadAllTextAsync(inputPath);
        await process.StandardInput.WriteAsync(inputContent);
        process.StandardInput.Close();

        var output = await process.StandardOutput.ReadToEndAsync();
        var error = await process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();
        string expectedOutput = "[{\"tax\":0.0}]";
        switch (inputPath)
        {
            case "./inputs/input.case#1.json":
                expectedOutput = "[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0}]";
                break;
            case "./inputs/input.case#2.json":
                expectedOutput = "[{\"tax\":0.0},{\"tax\":10000.0},{\"tax\":0.0}]";
                break;
            case "./inputs/input.case#3.json":
                expectedOutput = "[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":1000.0}]";
                break;
            case "./inputs/input.case#4.json":
                expectedOutput = "[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0}]";
                break;
            case "./inputs/input.case#5.json":
                expectedOutput = "[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":10000.0}]";
                break;
            case "./inputs/input.case#6.json":
                expectedOutput = "[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":3000.0}]";
                break;
            case "./inputs/input.case#7.json":
                expectedOutput = "[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":3000.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":3700.0},{\"tax\":0.0}]";
                break;
            case "./inputs/input.case#8.json":
                expectedOutput = "[{\"tax\":0.0},{\"tax\":80000.0},{\"tax\":0.0},{\"tax\":60000.0}]";
                break;
            case "./inputs/input.case#9.json":
                expectedOutput = "[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":1000.0},{\"tax\":2400.0}]";
                break;
            case "./inputs/input.case#1+case#2.json":
                expectedOutput = "[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0}] [{\"tax\":0.0},{\"tax\":10000.0},{\"tax\":0.0}]";
                break;
        }

        output.Replace("\r\n", " ").Replace("\n", " ")
            .Should().Contain(expectedOutput);
        error.Should().BeEmpty();
    }
}