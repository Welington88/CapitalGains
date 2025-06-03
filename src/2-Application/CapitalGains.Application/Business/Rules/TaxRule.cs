using CapitalGains.Domain.Entities;

namespace CapitalGains.Application.Business.Rules
{
    public static class TaxRule
    {
        /// <summary>
        ///     Calcula o imposto sobre vendas de ações, considerando a média ponderada e possíveis perdas financeiras.
        /// </summary>
        public static decimal CalculateSalesTax(Operation stock, float weightedAveragePriceResult, ref float financialLossStock)
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
    }
}