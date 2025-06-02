using CapitalGains.Application.Commands;
using CapitalGains.Domain.Business.Service;
using MediatR;

namespace CapitalGains.Application.UseCases.ProcessStocks;

public class ProcessStocksHandle : IRequestHandler<ProcessStocksCommand, string>
{
    private readonly IServiceOperation _serviceOperation;

    public ProcessStocksHandle(IServiceOperation serviceOperation)
        => _serviceOperation = serviceOperation;
    
    public async Task<string> Handle(ProcessStocksCommand request, CancellationToken cancellationToken)
    {
       return await _serviceOperation.ProcessListStocks(request.ReadInputStocks!);
    }
}

