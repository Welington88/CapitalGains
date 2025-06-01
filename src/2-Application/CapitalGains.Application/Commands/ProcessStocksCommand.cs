using MediatR;

namespace CapitalGains.Application.Commands;

public class ProcessStocksCommand : IRequest<string>
{
    public ProcessStocksCommand(string? readInputStocks)
        => ReadInputStocks = readInputStocks;

    public string? ReadInputStocks { get; set; }
}

