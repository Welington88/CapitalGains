using CapitalGains.domain.stocks.entity;
using CapitalGains.domain.stocks.exceptions;

namespace CapitalGains.Test;

public class TaxEntityTest
{
    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain","Tax - Entity")]
    public void Instantiate()
    {
        var numRandom = new Random();
        decimal valueTax = numRandom.Next(0,int.MaxValue);
        var validateObject = new {
            Tax = (decimal)valueTax
        };
        
        var resultTax = new Result(validateObject.Tax);

        Assert.NotNull(resultTax);
        Assert.Equal(validateObject.Tax, resultTax.Tax);
    }

    [Fact(DisplayName = nameof(ThrowWhenTypeIsEmptyOrNull))]
    [Trait("Domain","Tax - Entity")]
    public void ThrowWhenTypeIsEmptyOrNull()
    {

        var numRandom = new Random();
        decimal valueTax = numRandom.Next(int.MinValue,0);
        var validateObject = new {
            tax = (decimal)valueTax
        };

        Action action = 
                () => new Result(validateObject.tax);
                
        var exception =  Assert.Throws<EntityValidationExpetion>(action);
        Assert.Equal("Tax should not be less than zero or null", exception.Message);
    }
}