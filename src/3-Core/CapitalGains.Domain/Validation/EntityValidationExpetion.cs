namespace CapitalGains.domain.stocks.exceptions;

public class EntityValidationExpetion : Exception
{
    public EntityValidationExpetion(string message) : base(message)
    {
        
    }
}