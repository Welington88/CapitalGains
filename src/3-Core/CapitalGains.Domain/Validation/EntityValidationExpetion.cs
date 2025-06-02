namespace CapitalGains.Domain.Validation;

public class EntityValidationExpetion : Exception
{
    public EntityValidationExpetion(string message) : base(message)
    {
        
    }
}