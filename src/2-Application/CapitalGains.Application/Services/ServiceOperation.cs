using CapitalGains.domain.stocks.entity;
using CapitalGains.domain.stocks.enums;
using CapitalGains.domain.stocks.interfaces;
using Newtonsoft.Json;

namespace CapitalGains.domain.stocks.service;

public class ServiceOperation : IServiceOperation
{
    public async Task<string> ProcessListStocks(string inputStocks)
    {
        if (string.IsNullOrWhiteSpace(inputStocks))
            throw new Exception("input value cannot be empty or null")!;

        var lines = inputStocks
            .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Trim())
            .Where(line => line.StartsWith('[') && line.EndsWith(']'))
            .ToList();

        var outputs = new List<string>();

        foreach (var line in lines)
        {
            var listSimpleStocks = JsonConvert.DeserializeObject<List<Operation>>(line);
            var listweightedAveragePrice = new List<Operation>();
            float weightedAveragePriceResult = 0;
            int quantityOfStocksBought = 0;
            decimal taxValueResult = 0;
            float financialLossStock = 0;
            var subListTaxValueResult = new List<Result>();

            foreach (var stock in listSimpleStocks!)
            {
                if (stock.OperationType.Equals(TypeOperation.buy))
                {
                    listweightedAveragePrice.Add(stock);
                    quantityOfStocksBought += stock.Quantity;
                    weightedAveragePriceResult = weightedAveragePrice(listweightedAveragePrice);
                    taxValueResult = 0;
                }
                else if (stock.OperationType.Equals(TypeOperation.sell))
                {
                    if (stock.Quantity > quantityOfStocksBought)
                        throw new InvalidOperationException("number of shares for sell greater than the balance in wallet");
                    listweightedAveragePrice = sellReprocessWeightedAverageList(listweightedAveragePrice, stock);
                    quantityOfStocksBought -= stock.Quantity;
                    taxValueResult = calculateSalesTax(stock, weightedAveragePriceResult, ref financialLossStock);
                }
                subListTaxValueResult.Add(new Result(Math.Round(taxValueResult, 2)));
            }
            outputs.Add(JsonConvert.SerializeObject(subListTaxValueResult, Formatting.None).Replace(".0}", ".00}"));
        }

        return await Task.FromResult(string.Join(Environment.NewLine, outputs));
    }

    private static float weightedAveragePrice(List<Operation> listweightedAveragePrice)
    {
        var listStockMultiplication = new List<float>();
        foreach (var price in listweightedAveragePrice)
        {
            if (price.OperationType.Equals(TypeOperation.buy) && price.Quantity > 0)
            {
                var resultStockMultiplication = (price.Quantity * price.UnitCost);
                listStockMultiplication.Add(resultStockMultiplication);
            }
        }

        var weightedAverageStock = listStockMultiplication.Sum(s => s) / listweightedAveragePrice.Sum(s => s.Quantity);

        return (float) Math.Round(weightedAverageStock,2);
    }

    private static decimal calculateSalesTax(Operation stock, float weightedAveragePriceResult, ref float financialLossStock)
    {
        const float minimumValueToPayTax = 20000.00f;
        const float taxPercentageFinal = 0.20f;
        var totalValueOfTheOperation = (stock.UnitCost * stock.Quantity);
        var gainOrLossFinancialResult = (stock.UnitCost - weightedAveragePriceResult) * stock.Quantity;

        if (totalValueOfTheOperation <= minimumValueToPayTax)
        {
            if (gainOrLossFinancialResult < 0)
                financialLossStock += gainOrLossFinancialResult;
            return 0;
        }
        
        if (gainOrLossFinancialResult < 0)
            {
                financialLossStock += gainOrLossFinancialResult;
                return 0;
            }
            else
            {
                if (financialLossStock < 0)
                {
                    var compensable = Math.Min(-financialLossStock, gainOrLossFinancialResult);
                    gainOrLossFinancialResult -= compensable;
                    financialLossStock += compensable;
                }
                if (financialLossStock > 0) financialLossStock = 0;
            }

        return (decimal)(gainOrLossFinancialResult * taxPercentageFinal);
    }

    private static List<Operation> sellReprocessWeightedAverageList(List<Operation> listweightedAveragePrice, Operation stock)
    {
        var countStocklSell = stock.Quantity;
    
        foreach (var stockPrice in listweightedAveragePrice)
        {
            var newStockPrice = stockPrice;
            if (countStocklSell > 0 && newStockPrice.Quantity >= countStocklSell)
            {
                newStockPrice.Quantity -= countStocklSell;
                countStocklSell -= newStockPrice.Quantity;
            }
        }

        return listweightedAveragePrice;
    }
}
