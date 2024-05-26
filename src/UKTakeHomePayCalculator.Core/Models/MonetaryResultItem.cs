using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Core.Models;

public class MonetaryResultItem : IComparable<MonetaryResultItem>
{
    public MonetaryRuleItem Rule { get; }
    public decimal RulePercentage { get => Rule.Percentage; }
    public MonetaryValue Result { get; private set; }

    public MonetaryResultItem(MonetaryRuleItem rule, MonetaryValue result)
    {
        Rule = rule;
        Result = result;
    }

    public void Negate()
    {
        Result = -Result;
    }

    public override string ToString()
    {
        return $"{Rule} = {Result}";
    }
    
    public static bool operator==(MonetaryResultItem value1, MonetaryResultItem value2)
    {
        return value1.Rule == value2.Rule &&
               value1.Result == value2.Result;
    }
    
    public static bool operator!=(MonetaryResultItem value1, MonetaryResultItem value2)
    {
        return !(value1 == value2);
    }
    
    public static bool operator >(MonetaryResultItem value1, MonetaryResultItem value2)
    {
        if (value1.Rule > value2.Rule)
            return true;

        return false;
    }
    
    public static bool operator >=(MonetaryResultItem value1, MonetaryResultItem value2)
    {
        if (value1.Rule >= value2.Rule)
            return true;

        return false;    
    }
    
    public static bool operator <(MonetaryResultItem value1, MonetaryResultItem value2)
    {
        if (value1.Rule < value2.Rule)
            return true;

        return false;    
    }
    
    public static bool operator <=(MonetaryResultItem value1, MonetaryResultItem value2)
    {
        if (value1.Rule <= value2.Rule)
            return true;

        return false;
    }

    public int CompareTo(MonetaryResultItem? other)
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
        
        if (obj is not MonetaryResultItem)
            return false;
        
        return this == (MonetaryResultItem)obj;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
