using CapitalGains.Application.Business.Rules;
using CapitalGains.Application.Common;
using CapitalGains.Domain.Business.Service;
using CapitalGains.Domain.Entities;
using CapitalGains.Domain.Enum;
using CapitalGains.Domain.Ports;
using Newtonsoft.Json;

namespace CapitalGains.Application.Services;

public class ServiceOperation : IServiceOperation
{
    public async Task<string> ProcessListStocks(string inputStocks)
    {
        if (string.IsNullOrWhiteSpace(inputStocks))
            throw new Exception("input value cannot be empty or null")!;

        var inputData = inputStocks.Trim();
        var lines = JsonHelperExtensions.GetJsonLines(inputData);

        var outputs = new List<string>();

        foreach (var line in lines)
        {
            var operations = JsonHelperExtensions.DeserializeJsonToList<Operation>(line);
            var results = ProcessOperationResults(operations);
            outputs.Add(JsonConvert.SerializeObject(results, Formatting.None));
        }

        return await Task.FromResult(string.Join(Environment.NewLine, outputs));
    }
    
    /// <summary>
    ///     Processa os resultados das operações, calculando o imposto devido para cada operação.
    /// </summary>
    private static List<Result> ProcessOperationResults(List<Operation> operations)
    {
        var results = new List<Result>();
        var weightedAverageList = new List<Operation>();
        float weightedAveragePrice = 0;
        int quantityInWallet = 0;
        decimal taxValue = 0;
        float financialLoss = 0;

        foreach (var trade in operations)
        {
            if (trade.OperationType.Equals(TypeOperation.buy))
            {
                ProcessBuy(trade, weightedAverageList, ref quantityInWallet, ref weightedAveragePrice, ref taxValue);
            }
            else if (trade.OperationType.Equals(TypeOperation.sell))
            {
                ProcessSell(trade, weightedAverageList, ref quantityInWallet, ref weightedAveragePrice, ref taxValue, ref financialLoss);
            }
            results.Add(new Result(Math.Round(taxValue, 2)));
        }

        return results;
    }

    /// <summary>
    ///     Processa uma operação de compra, atualizando a lista de média ponderada, quantidade em carteira e preço médio ponderado.
    /// </summary>
    private static void ProcessBuy(Operation buyOperation, List<Operation> weightedAverageList, ref int quantityInWallet, ref float weightedAveragePrice, ref decimal taxValue)
    {
        weightedAverageList.Add(buyOperation);
        quantityInWallet += buyOperation.Quantity;
        weightedAveragePrice = TradeRules.WeightedAveragePrice(weightedAverageList);
        taxValue = 0;
    }

    /// <summary>
    ///     Processa uma operação de venda, atualizando a lista de média ponderada, quantidade em carteira e calculando o imposto devido.
    /// </summary>
    private static void ProcessSell(Operation sellOperation, List<Operation> weightedAverageList, ref int quantityInWallet, ref float weightedAveragePrice, ref decimal taxValue, ref float financialLoss)
    {
        if (sellOperation.Quantity > quantityInWallet)
            throw new InvalidOperationException("number of shares for sell greater than the balance in wallet");
        TradeRules.SellReprocessWeightedAverageList(weightedAverageList, sellOperation);
        quantityInWallet -= sellOperation.Quantity;
        taxValue = TaxRule.CalculateSalesTax(sellOperation, weightedAveragePrice, ref financialLoss);
    }
}