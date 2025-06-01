using CapitalGains.domain.stocks.service;

using (var reader = new StreamReader(Console.OpenStandardInput()))
{
    string inputJson = await reader.ReadToEndAsync();
    if (!string.IsNullOrWhiteSpace(inputJson))
    {
        var _stockService = new ServiceOperation();
        var resultOutputDateStocks = _stockService.processListStocks(inputJson.Trim());
        Console.WriteLine();
        Console.WriteLine(resultOutputDateStocks);
    }
}