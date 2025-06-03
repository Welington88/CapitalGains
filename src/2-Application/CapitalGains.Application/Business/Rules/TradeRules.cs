using CapitalGains.Domain.Entities;
using CapitalGains.Domain.Enum;

namespace CapitalGains.Application.Business.Rules
{
    public static class TradeRules
    {

        /// <summary>
        ///     Calcula o preço médio ponderado de uma lista de operações de compra.
        /// </summary>
        public static float WeightedAveragePrice(List<Operation> weightedAverageList)
        {
            var listStockMultiplication = weightedAverageList
                .Where(price => price.OperationType.Equals(TypeOperation.buy) && price.Quantity > 0)
                .Select(price => price.Quantity * price.UnitCost)
                .ToList();

            var totalQuantity = weightedAverageList.Sum(s => s.Quantity);
            if (totalQuantity == 0) return 0;

            var weightedAverageStock = listStockMultiplication.Sum() / totalQuantity;
            return (float)Math.Round(weightedAverageStock, 2);
        }

        /// <summary>
        ///     Reprocessa a lista de média ponderada após uma venda, ajustando as quantidades das operações restantes.
        /// </summary>
        public static void SellReprocessWeightedAverageList(List<Operation> weightedAverageList, Operation stock)
        {
            var countStockSell = stock.Quantity;

            foreach (var stockPrice in weightedAverageList)
            {
                if (countStockSell > 0 && stockPrice.Quantity >= countStockSell)
                {
                    stockPrice.Quantity -= countStockSell;
                    countStockSell -= stockPrice.Quantity;
                }
            }
        }
    }
}