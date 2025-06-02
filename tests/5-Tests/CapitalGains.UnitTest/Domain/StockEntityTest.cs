using CapitalGains.Domain.Entities;
using CapitalGains.Domain.Enum;
using CapitalGains.Domain.Validation;

namespace CapitalGains.UnitTest.Domain;

public class StockEntityTest
{
    [Fact(DisplayName = nameof(InstantiateEntityOperation))]
    [Trait("Domain", "Stock - Entity")]
    public void InstantiateEntityOperation()
    {
        var numRandom = new Random();
        var valueUnitCost = numRandom.Next(0, int.MaxValue);
        var valueQuantity = numRandom.Next(0, int.MaxValue);
        var validateObject = new
        {
            OperationType = TypeOperation.buy,
            UnitCost = (float)valueUnitCost,
            Quantity = valueQuantity
        };

        var operationStock = new Operation(validateObject.OperationType,
                                    validateObject.UnitCost,
                                    validateObject.Quantity
                                );

        operationStock.Should().NotBeNull();
        operationStock.OperationType.Should().Be(validateObject.OperationType);
        operationStock.UnitCost.Should().Be(validateObject.UnitCost);
        operationStock.Quantity.Should().Be(validateObject.Quantity);
    }

    [Fact(DisplayName = nameof(ThrowWhenTypeIsEmptyOrNull))]
    [Trait("Domain","Stock - Entity")]
    public void ThrowWhenTypeIsEmptyOrNull()
    {
        var numRandom = new Random();
        var valueUnitCost = numRandom.Next(0,int.MaxValue);
        var valueQuantity = numRandom.Next(0,int.MaxValue);

        var validateObject = new {
            OperationType = TypeOperation.valuenull,
            UnitCost = (float) valueUnitCost,
            Quantity  = valueQuantity
        };

        Action action = 
                () => new Operation(validateObject.OperationType,
                                    validateObject.UnitCost,
                                    validateObject.Quantity
                                );

        action.Should().Throw<EntityValidationExpetion>()
            .WithMessage("OperationType should not be empty or null");
    }

    [Fact(DisplayName = nameof(ThrowWhenTypeIsEmptyOrNullUnityCost))]
    [Trait("Domain","Stock - Entity")]
    public void ThrowWhenTypeIsEmptyOrNullUnityCost()
    {
        var numRandom = new Random();
        var valueUnitCost = numRandom.Next(int.MinValue,0);
        var valueQuantity = numRandom.Next(0,int.MaxValue);

        var validateObject = new {
            OperationType = TypeOperation.buy,
            UnitCost = (float) valueUnitCost,
            Quantity  = valueQuantity
        };

        Action action = 
                () => new Operation(validateObject.OperationType,
                                    validateObject.UnitCost,
                                    validateObject.Quantity
                                );

        action.Should().Throw<EntityValidationExpetion>()
            .WithMessage("UnitCost should not be less than zero or null");
    }

    [Fact(DisplayName = nameof(ThrowWhenTypeIsEmptyOrNullQuantity))]
    [Trait("Domain","Stock - Entity")]
    public void ThrowWhenTypeIsEmptyOrNullQuantity()
    {
        var numRandom = new Random();
        var valueUnitCost = numRandom.Next(0,int.MaxValue);
        var valueQuantity = numRandom.Next(int.MinValue,0);

        var validateObject = new {
            OperationType = TypeOperation.sell,
            UnitCost = (float) valueUnitCost,
            Quantity  = valueQuantity
        };

        Action action = 
                () => new Operation(validateObject.OperationType,
                                    validateObject.UnitCost,
                                    validateObject.Quantity
                                );
        action.Should().Throw<EntityValidationExpetion>()
            .WithMessage("Quantity should not be less than zero or null");               
    }
}