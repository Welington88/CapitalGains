namespace CapitalGains.Domain.Business.Service;

public interface IServiceOperation
{
    Task<string> ProcessListStocks(string inputStocks);
}
