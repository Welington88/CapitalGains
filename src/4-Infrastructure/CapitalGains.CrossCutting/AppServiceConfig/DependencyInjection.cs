using Microsoft.Extensions.DependencyInjection;
using CapitalGains.domain.stocks.interfaces;
using MediatR;
using CapitalGains.Application.Commands;
using CapitalGains.domain.stocks.service;
using System.Reflection;

namespace BackEnd.CrossCutting.AppServiceConfig;

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