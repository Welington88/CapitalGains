using CapitalGains.domain.stocks.service;

namespace CapitalGains.Test;

public class StockServiceTestCases
{
    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseOneAsync))]
    [Trait("Integration", "Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}]")]
    public async Task InstantiateServiceOperationCaseOneAsync(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = await operationServiceStock.ProcessListStocks(inputJsonStocks);

        operationServiceStock.Should().NotBeNull();
        resultListStocks.Should().BeOfType<string>();
        resultListStocks.Should().Be("[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0}]");
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseTwoAsync))]
    [Trait("Integration", "Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}]")]
    public async Task InstantiateServiceOperationCaseTwoAsync(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = await operationServiceStock.ProcessListStocks(inputJsonStocks);

        operationServiceStock.Should().NotBeNull();
        resultListStocks.Should().BeOfType<string>();
        resultListStocks.Should().Be("[{\"tax\":0.0},{\"tax\":10000.0},{\"tax\":0.0}]");
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseThreeAsync))]
    [Trait("Integration","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 3000}]")]
    public async Task InstantiateServiceOperationCaseThreeAsync(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = await operationServiceStock.ProcessListStocks(inputJsonStocks);

        operationServiceStock.Should().NotBeNull();
        resultListStocks.Should().BeOfType<string>();
        resultListStocks.Should().Be("[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":1000.0}]");
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseFourAsync))]
    [Trait("Integration","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 10000}]")]
    public async Task InstantiateServiceOperationCaseFourAsync(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = await operationServiceStock.ProcessListStocks(inputJsonStocks);

        operationServiceStock.Should().NotBeNull();
        resultListStocks.Should().BeOfType<string>();
        resultListStocks.Should().NotBeOfType<char>();
        resultListStocks.Should().Be("[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0}]");
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseFiveAsync))]
    [Trait("Integration","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\":5000}]")]
    public async Task InstantiateServiceOperationCaseFiveAsync(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = await operationServiceStock.ProcessListStocks(inputJsonStocks);

        operationServiceStock.Should().NotBeNull();
        resultListStocks.Should().BeOfType<string>();
        resultListStocks.Should().Be("[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":10000.0}]");
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseSixAsync))]
    [Trait("Integration","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":2.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000}, {\"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\": 1000}]")]
    public async Task InstantiateServiceOperationCaseSixAsync(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = await operationServiceStock.ProcessListStocks(inputJsonStocks);

        operationServiceStock.Should().NotBeNull();
        resultListStocks.Should().BeOfType<string>();
        resultListStocks.Should().Be("[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":3000.0}]");
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseSevenAsync))]
    [Trait("Integration","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":2.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000}, {\"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\": 1000}, {\"operation\":\"buy\", \"unit-cost\":20.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":30.00, \"quantity\": 4350}, {\"operation\":\"sell\", \"unit-cost\":30.00, \"quantity\": 650}]")]
    public async Task InstantiateServiceOperationCaseSevenAsync(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = await operationServiceStock.ProcessListStocks(inputJsonStocks);

        operationServiceStock.Should().NotBeNull();
        resultListStocks.Should().BeOfType<string>();
        resultListStocks.Should().NotBeOfType<char>();
        resultListStocks.Should().Be("[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":3000.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":3700.0},{\"tax\":0.0}]");
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseEightAsync))]
    [Trait("Integration","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":50.00, \"quantity\": 10000}, {\"operation\":\"buy\", \"unit-cost\":20.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":50.00, \"quantity\": 10000}]")]
    public async Task InstantiateServiceOperationCaseEightAsync(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = await operationServiceStock.ProcessListStocks(inputJsonStocks);

        operationServiceStock.Should().NotBeNull();
        resultListStocks.Should().BeOfType<string>();
        resultListStocks.Should().NotBeOfType<char>();
        resultListStocks.Should().Be("[{\"tax\":0.0},{\"tax\":80000.0},{\"tax\":0.0},{\"tax\":60000.0}]");
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseNineAsync))]
    [Trait("Integration", "Stock - Service")]
    [InlineData("[{ \"operation\": \"buy\",  \"unit-cost\": 5000.00,  \"quantity\": 10 },{ \"operation\": \"sell\", \"unit-cost\": 4000.00,  \"quantity\": 5 },{ \"operation\": \"buy\",  \"unit-cost\": 15000.00, \"quantity\": 5 },{ \"operation\": \"buy\",  \"unit-cost\": 4000.00,  \"quantity\": 2 },{ \"operation\": \"buy\",  \"unit-cost\": 23000.00, \"quantity\": 2 },{ \"operation\": \"sell\", \"unit-cost\": 20000.00, \"quantity\": 1 },{ \"operation\": \"sell\", \"unit-cost\": 12000.00, \"quantity\": 10 },{ \"operation\": \"sell\", \"unit-cost\": 15000.00, \"quantity\": 3 }]")]
    public async Task InstantiateServiceOperationCaseNineAsync(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = await operationServiceStock.ProcessListStocks(inputJsonStocks);

        operationServiceStock.Should().NotBeNull();
        resultListStocks.Should().BeOfType<string>();
        resultListStocks.Should().NotBeOfType<char>();
        resultListStocks.Should().Be("[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0},{\"tax\":1000.0},{\"tax\":2400.0}]");
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseOneToCaseTwoAsync))]
    [Trait("Integration","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}]\n[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}]")]
    public async Task InstantiateServiceOperationCaseOneToCaseTwoAsync(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = await operationServiceStock.ProcessListStocks(inputJsonStocks);

        operationServiceStock.Should().NotBeNull();
        resultListStocks.Should().BeOfType<string>();
        resultListStocks.Replace("\r\n", " ").Replace("\n", " ")
            .Should().Be("[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0}] [{\"tax\":0.0},{\"tax\":10000.0},{\"tax\":0.0}]");
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseOneToCaseThreeAsync))]
    [Trait("Integration","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}]\n[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 3000}]")]
    public async Task InstantiateServiceOperationCaseOneToCaseThreeAsync(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = await operationServiceStock.ProcessListStocks(inputJsonStocks);

        operationServiceStock.Should().NotBeNull();
        resultListStocks.Should().BeOfType<string>();
        resultListStocks.Replace("\r\n", " ").Replace("\n", " ")
        .Should().Be("[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0}] [{\"tax\":0.0},{\"tax\":0.0},{\"tax\":1000.0}]");
    }

    [Theory(DisplayName = nameof(InstantiateServiceOperationCaseOneToCaseFourAsync))]
    [Trait("Integration","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}]\n[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 10000}]")]
    public async Task InstantiateServiceOperationCaseOneToCaseFourAsync(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        var resultListStocks = await operationServiceStock.ProcessListStocks(inputJsonStocks);

        operationServiceStock.Should().NotBeNull();
        resultListStocks.Should().BeOfType<string>();
        resultListStocks.Should().NotBeOfType<char>();
        resultListStocks.Replace("\r\n", " ").Replace("\n", " ")
            .Should().Be("[{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0}] [{\"tax\":0.0},{\"tax\":0.0},{\"tax\":0.0}]");
    }

    [Theory(DisplayName = nameof(ThrowWhenSellGreaterThanReserveOfBought))]
    [Trait("Integration","Stock - Service")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 150}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}]\n[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\": 5000}, {\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 10000}]")]
    [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000}, {\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 15000}, {\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}]")]
    public void ThrowWhenSellGreaterThanReserveOfBought(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        Action action = 
                () => operationServiceStock.ProcessListStocks(inputJsonStocks).GetAwaiter().GetResult() ;

        action.Should().Throw<InvalidOperationException>()
            .WithMessage("number of shares for sell greater than the balance in wallet");
    }

    [Theory(DisplayName = nameof(ThrowWhenTypeIsEmptyOrNull))]
    [Trait("Integration","Stock - Service")]
    [InlineData("")]
    public void ThrowWhenTypeIsEmptyOrNull(string inputJsonStocks)
    {
        var operationServiceStock = new ServiceOperation();
        Action action = 
                () => operationServiceStock.ProcessListStocks(inputJsonStocks).GetAwaiter().GetResult();

        action.Should().Throw<Exception>()
            .WithMessage("input value cannot be empty or null");
    }
}