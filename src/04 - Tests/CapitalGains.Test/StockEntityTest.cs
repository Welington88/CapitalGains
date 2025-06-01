using CapitalGains.domain.stocks.entity;
using CapitalGains.domain.stocks.enums;
using CapitalGains.domain.stocks.exceptions;

namespace CapitalGains.Test;

public class StockEntityTest
{
    [Fact(DisplayName = nameof(InstantiateEntityOperation))]
    [Trait("Domain","Stock - Entity")]
    public void InstantiateEntityOperation()
    {
        var numRandom = new Random();
        var valueUnitCost = numRandom.Next(0,int.MaxValue);
        var valueQuantity = numRandom.Next(0,int.MaxValue);
        var validateObject = new {
            OperationType = TypeOperation.buy,
            UnitCost = (float)valueUnitCost,
            Quantity  = valueQuantity
        };

        var operationStock = new Operation(validateObject.OperationType,
                                    validateObject.UnitCost,
                                    validateObject.Quantity
                                );

        Assert.NotNull(operationStock);
        Assert.Equal(validateObject.OperationType, operationStock.OperationType);
        Assert.Equal(validateObject.UnitCost, operationStock.UnitCost);
        Assert.Equal(validateObject.Quantity, operationStock.Quantity);
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

        var exception =  Assert.Throws<EntityValidationExpetion>(action);
        Assert.Equal("OperationType should not be empty or null", exception.Message);
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

        var exception =  Assert.Throws<EntityValidationExpetion>(action);
        Assert.Equal("UnitCost should not be less than zero or null", exception.Message);
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
                                
        var exception =  Assert.Throws<EntityValidationExpetion>(action);
        Assert.Equal("Quantity should not be less than zero or null", exception.Message);
    }
}