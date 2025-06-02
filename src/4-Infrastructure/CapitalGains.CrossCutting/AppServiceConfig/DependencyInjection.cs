using Microsoft.Extensions.DependencyInjection;
using MediatR;
using CapitalGains.Application.Commands;
using System.Reflection;
using CapitalGains.Application.Services;
using CapitalGains.Domain.Business.Service;

namespace CapitalGains.CrossCutting.AppServiceConfig;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection _services)
    {
        _services.AddScoped<IServiceOperation, ServiceOperation>();
        _services.AddMediatR(Assembly.GetExecutingAssembly());
        _services.AddMediatR(typeof(ProcessStocksCommand).Assembly);
   
        return _services;
    }
}