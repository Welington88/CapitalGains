using CapitalGains.domain.stocks.enums;
using CapitalGains.domain.stocks.exceptions;
using Newtonsoft.Json;

namespace CapitalGains.domain.stocks.entity;

public class Operation
{
    public Operation(TypeOperation operation, float unitcost, int quantity)
    {
        this.OperationType = operation;
        this.UnitCost = unitcost;
        this.Quantity = quantity;
        this.validate();
    }
    
    [JsonProperty("operation")]
    public TypeOperation OperationType { get; set; }
    
    [JsonProperty("unit-cost")]
    public float UnitCost { get; set; }
    
    [JsonProperty("quantity")]
    public int Quantity { get;  set; }

    private void validate(){
        if (string.IsNullOrWhiteSpace(OperationType.ToString()) || OperationType < 0)
            throw new EntityValidationExpetion($"{nameof(OperationType)} should not be empty or null");
        
        if (UnitCost < 0)
            throw new EntityValidationExpetion($"{nameof(UnitCost)} should not be less than zero or null");
        
        if (Quantity < 0)
            throw new EntityValidationExpetion($"{nameof(Quantity)} should not be less than zero or null");
    }
}