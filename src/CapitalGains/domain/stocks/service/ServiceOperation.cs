using CapitalGains.domain.stocks.entity;
using CapitalGains.domain.stocks.enums;
using CapitalGains.domain.stocks.interfaces;
using Newtonsoft.Json;

namespace CapitalGains.domain.stocks.service;

public class ServiceOperation : IServiceOperation
{
    public string processListStocks(string inputJsonStocks)
    {
        if (string.IsNullOrEmpty(inputJsonStocks.Trim()))
        {
            throw new Exception("input value cannot be empty or null")!;
        }

        var listResultConvertJsonToStocks = convertJsonToObject(inputJsonStocks);
        var listweightedAveragePrice = new List<Operation>();
        float weightedAveragePriceResult = 0;
        int quantityOfStocksBought = 0;
        decimal taxValueResult = 0;
        float financialLossStock = 0;
        var listTaxValueResult = new List<List<Result>>();

        foreach (var listSimpleStocks in listResultConvertJsonToStocks)
        {
            var subListTaxValueResult = new List<Result>();
            foreach (var stock in listSimpleStocks)
            {
                if(stock.OperationType.Equals(TypeOperation.buy))
                {
                    listweightedAveragePrice.Add(stock);
                    quantityOfStocksBought += stock.Quantity;
                    weightedAveragePriceResult = weightedAveragePrice(listweightedAveragePrice);
                    taxValueResult = 0;
                } else if(stock.OperationType.Equals(TypeOperation.sell))
                {
                    if (stock.Quantity > quantityOfStocksBought)
                    {
                        throw new Exception("number of shares for sell greater than the balance in wallet");
                    }
                    listweightedAveragePrice = sellReprocessWeightedAverageList(listweightedAveragePrice,stock);
                    quantityOfStocksBought -= stock.Quantity;
                    taxValueResult = calculateSalesTax(stock, quantityOfStocksBought, weightedAveragePriceResult, ref financialLossStock);
                }
                subListTaxValueResult.Add(new Result(Math.Round(taxValueResult,2)));
            }
            listTaxValueResult.Add(subListTaxValueResult);
        }
        var listTaxValueString = convertObjectToJson(listTaxValueResult);
        return listTaxValueString;
    }

    private float weightedAveragePrice(List<Operation> listweightedAveragePrice)
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

private decimal calculateSalesTax(Operation stock, int quantityOfStocksBought, float weightedAveragePriceResult, ref float financialLossStock)
{
    const float minimumValueToPayTax = 20000.00f;
    const float taxPercentageFinal = 0.20f;
    var totalValueOfTheOperation = (stock.UnitCost * stock.Quantity);
    var gainOrLossFinancialResult = (stock.UnitCost - weightedAveragePriceResult) * stock.Quantity;

    // Se venda isenta, não consome/prejudica o prejuízo acumulado
    if (totalValueOfTheOperation <= minimumValueToPayTax)
    {
        // Se prejuízo, acumula normalmente
        if (gainOrLossFinancialResult < 0)
            financialLossStock += gainOrLossFinancialResult;
        return 0;
    }

    // Só aqui compensa prejuízo acumulado
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

    private List<Operation> sellReprocessWeightedAverageList(List<Operation> listweightedAveragePrice, Operation stock)
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

    private List<List<Operation>> convertJsonToObject(string inputJsonStocks)
    {
        var resultConvertJsonToObject = new List<List<Operation>>();
        int checkListOrManyLists = inputJsonStocks.Split(new char[]{ '[' }).Length - 1;

        if (checkListOrManyLists <= 1)
        {
            var resultConvertJsonToObjectSimple = JsonConvert.DeserializeObject<List<Operation>>(inputJsonStocks);
            resultConvertJsonToObject.Add(resultConvertJsonToObjectSimple);
        } else {
            inputJsonStocks = convertDataList(inputJsonStocks);
            resultConvertJsonToObject = JsonConvert.DeserializeObject<List<List<Operation>>>(inputJsonStocks);
        }

        return resultConvertJsonToObject;
    }

    private string convertObjectToJson(List<List<Result>> listTaxValueResult)
    {
        var jsonResult = string.Empty;
        
        if (listTaxValueResult.Count() <= 1)
        {
            var listTaxValueResultSimple = listTaxValueResult.FirstOrDefault().ToList();
            jsonResult = JsonConvert.SerializeObject(listTaxValueResultSimple, Formatting.None);    
        } else {
            var jsonResultReplace = JsonConvert.SerializeObject(listTaxValueResult, Formatting.None);    
            var jsonResultRemoveFirstCharacter = jsonResultReplace.Replace(",["," [");   
            jsonResult = jsonResultRemoveFirstCharacter.Substring(1, jsonResultRemoveFirstCharacter.Length-2);    
        }

        return jsonResult.Replace(".0}",".00}");
    }

    private string convertDataList(string inputJsonStocks)
    {
        var replaceInputJsonStocks = inputJsonStocks.Replace("[","--[");
        var splitInputJsonStocks = replaceInputJsonStocks.Split("--");
        var joinInputJsonStocks = string.Join(",",splitInputJsonStocks);
        var removeFirstCharacterInputJsonStocks = joinInputJsonStocks.Substring(1);
        var resultConvertInputJsonStocks = string.Concat("[",removeFirstCharacterInputJsonStocks,"]");

        return resultConvertInputJsonStocks;
    }
}
