using FluentAssertions;
using UKTakeHomePayCalculator.Core.Helpers;
using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Tests.ModelsTests;

public class MonetaryResultItemTests
{
    public static TheoryData<MonetaryRuleItem, MonetaryValue, string> ShouldReturnCorrectStringTheoryData => new()
    {
        {new MonetaryRuleItem(MonetaryValue.Zero, 0.08m), 120m.Week(), "From 0.00 @ %8.00 = 120.00/Week"},
        {new MonetaryRuleItem(1200m.Year(), 0.12m), 10500m.Year(), "From 1,200.00/Year @ %12.00 = 10,500.00/Year"},
        {new MonetaryRuleItem(450000m.Year(), 0.02m), 2890m.Month(), "From 450,000.00/Year @ %2.00 = 2,890.00/Month"},
    };

    [Theory]
    [MemberData(nameof(ShouldReturnCorrectStringTheoryData))]
    public void ShouldReturnCorrectString(MonetaryRuleItem ruleItem, MonetaryValue result, string expectedResult)
    {
        var monetaryResultItem = new MonetaryResultItem(ruleItem, result);

        var actualResult = monetaryResultItem.ToString();

        actualResult.Should().Be(expectedResult);
    }
}
