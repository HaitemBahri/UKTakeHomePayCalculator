using FluentAssertions;
using UKTakeHomePayCalculator.Core.Helpers;
using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Tests.ModelsTests;

public class MonetaryRuleItemTests
{
    public static TheoryData<MonetaryValue, MonetaryValue, decimal, string> ShouldReturnCorrectStringTheoryData => new()
    {
        {MonetaryValue.Zero, 3500m.Year(), 0.08m, "[From 0.00/Week - To 3,500.00/Year] %8.00"},
        {1200m.Year(), 3500m.Year(), 0.12m, "[From 1,200.00/Year - To 3,500.00/Year] %12.00"},
        {450000m.Year(), MonetaryValue.PositiveInfinity, 0.02m, "[From 450,000.00/Year - To +Infinity] %2.00"},
    };

    [Theory]
    [MemberData(nameof(ShouldReturnCorrectStringTheoryData))]
    public void ShouldReturnCorrectString(MonetaryValue fromValue, MonetaryValue toValue, decimal percentage, string actualResult)
    {
        var monetaryRuleItem = new MonetaryRuleItem(fromValue, toValue, percentage);

        var expectedResult = monetaryRuleItem.ToString();

        actualResult.Should().Be(expectedResult); 
    }
}
