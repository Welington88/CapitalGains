using AutoBogus;
using CapitalGains.Application.Commands;
using CapitalGains.Application.UseCases.ProcessStocks;
using CapitalGains.Domain.Business.Service;
using CapitalGains.Domain.Entities;
using CapitalGains.Domain.Enum;
using Moq;
using Newtonsoft.Json;

namespace CapitalGains.IntegrationTests.Aplication;

public class ProcessStocksCommandTest
{
    [Fact(DisplayName = nameof(Constructor_AssignsValue))]
    [Trait("Integration", "Stock - Service")]
    public void Constructor_AssignsValue()
    {
        var input = new AutoFaker<string>().Generate();

        var command = new ProcessStocksCommand(input);

        command.ReadInputStocks.Should().Be(input);
    }

    [Fact(DisplayName = nameof(ReadInputStocks_CanBeSet))]
    [Trait("Integration", "Stock - Service")]
    public void ReadInputStocks_CanBeSet()
    {
        var command = new ProcessStocksCommand(null);
        var newValue = new AutoFaker<string>().Generate();

        command.ReadInputStocks = newValue;

        command.ReadInputStocks.Should().Be(newValue);
    }

    [Theory(DisplayName = nameof(Constructor_AllowsNull))]
    [Trait("Integration", "Stock - Service")]
    [InlineData(null)]
    public void Constructor_AllowsNull(string? input)
    {
        var command = new ProcessStocksCommand(input);

        command.ReadInputStocks.Should().BeNull();
    }

    [Fact(DisplayName = nameof(Handle_ReturnsServiceResult))]
    [Trait("Integration", "Stock - Service")]
    public async Task Handle_ReturnsServiceResult()
    {
        var quantity = new Random().Next(1, 10000);
        var unitCost = new Random().Next(1, 10000);
        var operationType = TypeOperation.buy;

        var _inputOperation = new Operation(operationType, unitCost, quantity);

        _inputOperation.OperationType = TypeOperation.buy;
        var input = JsonConvert.SerializeObject(new List<Operation> { _inputOperation });

        var expected = "[{\"tax\":0.0}]";
        var mockService = new Mock<IServiceOperation>();
        mockService.Setup(s => s.ProcessListStocks(input)).ReturnsAsync(expected);

        var handler = new ProcessStocksHandle(mockService.Object);
        var command = new ProcessStocksCommand(input);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().Be(expected);
        mockService.Verify(s => s.ProcessListStocks(input), Times.Once);
    }

    [Fact(DisplayName = nameof(Handle_ThrowsIfServiceThrows))]
    [Trait("Integration", "Stock - Service")]
    public async Task Handle_ThrowsIfServiceThrows()
    {
        var input = "[{\"operation\":\"buy\",\"unit-cost\":10.0,\"quantity\":100}]";
        var mockService = new Mock<IServiceOperation>();
        mockService.Setup(s => s.ProcessListStocks(input)).ThrowsAsync(new Exception("erro"));

        var handler = new ProcessStocksHandle(mockService.Object);
        var command = new ProcessStocksCommand(input);

        handler.Should().NotBeNull();
        command.Should().NotBeNull();

        await FluentActions.Invoking(() => handler.Handle(command, CancellationToken.None))
                .Should().ThrowAsync<Exception>();
    }
}