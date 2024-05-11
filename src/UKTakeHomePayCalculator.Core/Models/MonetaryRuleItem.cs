using UKTakeHomePayCalculator.Core.Reports;

namespace UKTakeHomePayCalculator.Core.Models;

public class MonetaryRuleItem
{
    public MonetaryValue FromValue { get; }
    public MonetaryValue ToValue { get; }
    public decimal Percentage { get; }

    public MonetaryRuleItem(MonetaryValue fromValue, MonetaryValue toValue, decimal percentage)
    {
        FromValue = fromValue;
        ToValue = toValue;
        Percentage = percentage;
    }

    public override string ToString()
    {
        return $"[From {FromValue} - To {ToValue}] %{Percentage * 100:N2}";
    }
    
    public static bool operator==(MonetaryRuleItem value1, MonetaryRuleItem value2)
    {
        return value1.FromValue == value2.FromValue &&
               value1.ToValue == value2.ToValue &&
               value1.Percentage == value2.Percentage;
    }
    
    public static bool operator!=(MonetaryRuleItem value1, MonetaryRuleItem value2)
    {
        return !(value1 == value2);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        
        if (obj is not MonetaryRuleItem)
            return false;
        
        return this == (MonetaryRuleItem)obj;
    }
}
