
using BackEnd.CrossCutting.AppServiceConfig;
using CapitalGains.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CapitalGains.Console;

public static class Program
{
    private static ServiceProvider _serviceProvider = null!;
    private static IMediator _mediator => _serviceProvider.GetRequiredService<IMediator>();
    private static async Task Main(string[] args)
    {
        DependeciesInjection();

        using var reader = new StreamReader(System.Console.OpenStandardInput());

        string inputJson = await reader.ReadToEndAsync();
        if (!string.IsNullOrWhiteSpace(inputJson))
        {
            var resultOutputDateStocks = await _mediator.Send(new ProcessStocksCommand(inputJson.Trim()));
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