
using CapitalGains.CrossCutting.AppServiceConfig;
using CapitalGains.Application.Commands;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Text;

namespace CapitalGains.Console;

public static class Program
{
    private static ServiceProvider _serviceProvider = null!;
    private static IMediator _mediator => _serviceProvider.GetRequiredService<IMediator>();
    private static async Task Main(string[] args)
    {
        DependeciesInjection();

        var inputJson = new StringBuilder();
        string line;
        while ((line = System.Console.ReadLine()) != null && line != "")
        {
            inputJson.AppendLine(line.Trim());
        }
        
        if (!string.IsNullOrWhiteSpace(inputJson.ToString()))
        {
            var resultOutputDateStocks = await _mediator.Send(new ProcessStocksCommand(inputJson.ToString().Trim()));
            System.Console.WriteLine();
            System.Console.WriteLine(resultOutputDateStocks);
        }
    }

    private static void DependeciesInjection()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private static void ConfigureServices(ServiceCollection _services)
    {
        _services.AddInfrastructure();
    }
}