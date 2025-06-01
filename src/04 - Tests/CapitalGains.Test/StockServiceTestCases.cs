using CapitalGains.domain.stocks.service;

namespace CapitalGains.Test;

public class StockServiceTestCases
{
    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseOne))]
    [Trait("Domain","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}]")]
    public void InstantiateServiceOperationCaseOne(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = operationServiceStock.processListStocks(inputJsonStocks);

        Assert.NotNull(operationServiceStock);
        Assert.IsType<string>(resultListStocks);
        Assert.Equal("[{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00}]", resultListStocks);
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseTwo))]
    [Trait("Domain","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}]")]
    public void InstantiateServiceOperationCaseTwo(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = operationServiceStock.processListStocks(inputJsonStocks);

        Assert.NotNull(operationServiceStock);
        Assert.IsType<string>(resultListStocks);
        Assert.Equal("[{\"tax\":0.00},{\"tax\":10000.00},{\"tax\":0.00}]",resultListStocks);
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseThree))]
    [Trait("Domain","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 3000}]")]
    public void InstantiateServiceOperationCaseThree(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = operationServiceStock.processListStocks(inputJsonStocks);

        Assert.NotNull(operationServiceStock);
        Assert.IsType<string>(resultListStocks);
        Assert.Equal("[{\"tax\":0.00},{\"tax\":0.00},{\"tax\":1000.00}]", resultListStocks);
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseFour))]
    [Trait("Domain","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 10000}]")]
    public void InstantiateServiceOperationCaseFour(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = operationServiceStock.processListStocks(inputJsonStocks);

        Assert.NotNull(operationServiceStock);
        Assert.IsType<string>(resultListStocks);
        Assert.Equal("[{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00}]", resultListStocks);
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseFive))]
    [Trait("Domain","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\":5000}]")]
    public void InstantiateServiceOperationCaseFive(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = operationServiceStock.processListStocks(inputJsonStocks);

        Assert.NotNull(operationServiceStock);
        Assert.IsType<string>(resultListStocks);
        Assert.Equal("[{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":10000.00}]",resultListStocks);
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseSix))]
    [Trait("Domain","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":2.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000}, {\"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\": 1000}]")]
    public void InstantiateServiceOperationCaseSix(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = operationServiceStock.processListStocks(inputJsonStocks);

        Assert.NotNull(operationServiceStock);
        Assert.IsType<string>(resultListStocks);
        Assert.Equal("[{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":3000.00}]", resultListStocks);
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseSeven))]
    [Trait("Domain","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":2.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000}, {\"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\": 1000}, {\"operation\":\"buy\", \"unit-cost\":20.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":30.00, \"quantity\": 4350}, {\"operation\":\"sell\", \"unit-cost\":30.00, \"quantity\": 650}]")]
    public void InstantiateServiceOperationCaseSeven(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = operationServiceStock.processListStocks(inputJsonStocks);

        Assert.NotNull(operationServiceStock);
        Assert.IsType<string>(resultListStocks);
        Assert.Equal("[{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":3000.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":3700.00},{\"tax\":0.00}]", resultListStocks);
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseEight))]
    [Trait("Domain","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":50.00, \"quantity\": 10000}, {\"operation\":\"buy\", \"unit-cost\":20.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":50.00, \"quantity\": 10000}]")]
    public void InstantiateServiceOperationCaseEight(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = operationServiceStock.processListStocks(inputJsonStocks);

        Assert.NotNull(operationServiceStock);
        Assert.IsType<string>(resultListStocks);
        Assert.Equal("[{\"tax\":0.00},{\"tax\":80000.00},{\"tax\":0.00},{\"tax\":60000.00}]", resultListStocks);
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseNine))]
    [Trait("Domain","Stock - Service")]
    [InlineData("[{ \"operation\": \"buy\",  \"unit-cost\": 5000.00,  \"quantity\": 10 },{ \"operation\": \"sell\", \"unit-cost\": 4000.00,  \"quantity\": 5 },{ \"operation\": \"buy\",  \"unit-cost\": 15000.00, \"quantity\": 5 },{ \"operation\": \"buy\",  \"unit-cost\": 4000.00,  \"quantity\": 2 },{ \"operation\": \"buy\",  \"unit-cost\": 23000.00, \"quantity\": 2 },{ \"operation\": \"sell\", \"unit-cost\": 20000.00, \"quantity\": 1 },{ \"operation\": \"sell\", \"unit-cost\": 12000.00, \"quantity\": 10 },{ \"operation\": \"sell\", \"unit-cost\": 15000.00, \"quantity\": 3 }]")]
    public void InstantiateServiceOperationCaseNine(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = operationServiceStock.processListStocks(inputJsonStocks);

        Assert.NotNull(operationServiceStock);
        Assert.IsType<string>(resultListStocks);
        Assert.Equal("[{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00},{\"tax\":1000.00},{\"tax\":2400.00}]", resultListStocks);
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseOneToCaseTwo))]
    [Trait("Domain","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}] [{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}]")]
    public void InstantiateServiceOperationCaseOneToCaseTwo(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = operationServiceStock.processListStocks(inputJsonStocks);

        Assert.NotNull(operationServiceStock);
        Assert.IsType<string>(resultListStocks);
        Assert.Equal("[{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00}] [{\"tax\":0.00},{\"tax\":10000.00},{\"tax\":0.00}]", resultListStocks);
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseOneToCaseThree))]
    [Trait("Domain","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}] [{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 3000}]")]
    public void InstantiateServiceOperationCaseOneToCaseThree(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = operationServiceStock.processListStocks(inputJsonStocks);

        Assert.NotNull(operationServiceStock);
        Assert.IsType<string>(resultListStocks);
        Assert.Equal("[{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00}] [{\"tax\":0.00},{\"tax\":0.00},{\"tax\":1000.00}]", resultListStocks);
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseOneToCaseFour))]
    [Trait("Domain","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}] [{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 10000}]")]
    public void InstantiateServiceOperationCaseOneToCaseFour(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = operationServiceStock.processListStocks(inputJsonStocks);

        Assert.NotNull(operationServiceStock);
        Assert.IsType<string>(resultListStocks);
        Assert.Equal("[{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00}] [{\"tax\":0.00},{\"tax\":0.00},{\"tax\":0.00}]", resultListStocks);
    }

    [Theory(DisplayName = nameof(ThrowWhenSellGreaterThanReserveOfBought))]
    [Trait("Domain","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 150}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}] [{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 10000}]")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 15000}, {\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}]")]
    public void ThrowWhenSellGreaterThanReserveOfBought(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        Action action = 
                () => operationServiceStock.processListStocks(inputJsonStocks);

        var exception =  Assert.Throws<Exception>(action);
        Assert.Equal("number of shares for sell greater than the balance in wallet", exception.Message);
    }

    [Theory(DisplayName = nameof(ThrowWhenTypeIsEmptyOrNull))]
    [Trait("Domain","Stock - Service")]
    [InlineData("")]
    public void ThrowWhenTypeIsEmptyOrNull(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        Action action = 
                () => operationServiceStock.processListStocks(inputJsonStocks);

        var exception =  Assert.Throws<Exception>(action);
        Assert.Equal("input value cannot be empty or null", exception.Message);
    }
}