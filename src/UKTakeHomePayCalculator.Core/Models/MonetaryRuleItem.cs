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
}
