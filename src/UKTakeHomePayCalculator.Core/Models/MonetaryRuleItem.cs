using UKTakeHomePayCalculator.Core.Reports;

namespace UKTakeHomePayCalculator.Core.Models;

public class MonetaryRuleItem : IComparable<MonetaryRuleItem>
{
    public MonetaryValue FromValue { get; }
    public decimal Percentage { get; }

    public MonetaryRuleItem(MonetaryValue fromValue, decimal percentage)
    {
        FromValue = fromValue;
        Percentage = percentage;
    }

    public override string ToString()
    {
        return $"From {FromValue} @ %{Percentage * 100:N2}";
    }
    
    public static bool operator==(MonetaryRuleItem value1, MonetaryRuleItem value2)
    {
        return value1.FromValue == value2.FromValue;
    }
    
    public static bool operator!=(MonetaryRuleItem value1, MonetaryRuleItem value2)
    {
        return !(value1 == value2);
    }

    public static bool operator >(MonetaryRuleItem value1, MonetaryRuleItem value2)
    {
        if (value1.FromValue > value2.FromValue)
            return true;

        return false;
    }
    
    public static bool operator >=(MonetaryRuleItem value1, MonetaryRuleItem value2)
    {
        if (value1.FromValue >= value2.FromValue)
            return true;

        return false;    
    }
    
    public static bool operator <(MonetaryRuleItem value1, MonetaryRuleItem value2)
    {
        if (value1.FromValue < value2.FromValue)
            return true;

        return false;    
    }
    
    public static bool operator <=(MonetaryRuleItem value1, MonetaryRuleItem value2)
    {
        if (value1.FromValue <= value2.FromValue)
            return true;

        return false;
    }

    public int CompareTo(MonetaryRuleItem? other)
    {
        if (this < other)
            return -1;

        if (this == other)
            return 0;

        return 1;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        
        if (obj is not MonetaryRuleItem)
            return false;
        
        return this == (MonetaryRuleItem)obj;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
