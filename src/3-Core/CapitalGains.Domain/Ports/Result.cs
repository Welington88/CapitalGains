using CapitalGains.Domain.Validation;
using Newtonsoft.Json;

namespace CapitalGains.Domain.Ports;

public class Result
{
    public Result(decimal tax)
    {
        Tax = tax;
        this.Validate();
    }

    [JsonProperty("tax")]
    public decimal? Tax { get; set; }

    private void Validate(){
        if (Tax is null || Tax < 0)
            throw new EntityValidationExpetion($"{nameof(Tax)} should not be less than zero or null");    
    }
}
