namespace CapitalGains.domain.stocks.interfaces;

public interface IServiceOperation
{
    Task<string> ProcessListStocks(string inputStocks);
}
