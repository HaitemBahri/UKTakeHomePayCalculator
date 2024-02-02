using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Core.Models;

public class MonetaryResultItem
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
        return $"Rule = {Rule}, Result = {Result}";
    }
}
