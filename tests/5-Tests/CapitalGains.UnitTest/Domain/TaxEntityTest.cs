using CapitalGains.Domain.Ports;
using CapitalGains.Domain.Validation;

namespace CapitalGains.UnitTest.Domain;

public class TaxEntityTest
{
    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain","Tax - Entity")]
    public void Instantiate()
    {
        var numRandom = new Random();
        decimal valueTax = numRandom.Next(0,int.MaxValue);
        var validateObject = new {
            Tax = (decimal?)valueTax
        };
        
        var resultTax = new Result(validateObject.Tax.Value);

        resultTax.Should().NotBeNull();
        resultTax.Tax.Should().Be(validateObject.Tax.Value);
    }

    [Fact(DisplayName = nameof(ThrowWhenTypeIsEmptyOrNull))]
    [Trait("Domain","Tax - Entity")]
    public void ThrowWhenTypeIsEmptyOrNull()
    {

        var numRandom = new Random();
        decimal valueTax = numRandom.Next(int.MinValue,0);
        var validateObject = new {
            tax = (decimal?)valueTax
        };

        Action action = 
                () => new Result(validateObject.tax.Value);
        
        action.Should().Throw<EntityValidationExpetion>()
            .WithMessage("Tax should not be less than zero or null");
    }
}