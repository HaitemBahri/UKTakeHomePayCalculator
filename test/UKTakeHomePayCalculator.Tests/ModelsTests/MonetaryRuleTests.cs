using FluentAssertions;
using UKTakeHomePayCalculator.Core.Helpers;
using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Tests.ModelsTests;

public class MonetaryRuleTests
{
    public static TheoryData<IEnumerable<MonetaryRuleItem>, int, string> ShouldReturnCorrectStringTheoryData => new()
    {
        {   new List<MonetaryRuleItem> ()
            {   new MonetaryRuleItem(0m.Week(), 1200m.Year(), 0.10m),
                new MonetaryRuleItem(3000m.Year(), 4500m.Year(), 0.02m),
                new MonetaryRuleItem(60000m.Year(), MonetaryValue.PositiveInfinity, 0.01m)},

        3,

         "[From 0.00/Week - To 1,200.00/Year] %10.00\n" +
         "[From 3,000.00/Year - To 4,500.00/Year] %2.00\n" +
         "[From 60,000.00/Year - To +Infinity] %1.00" }
    };

    [Theory]
    [MemberData(nameof(ShouldReturnCorrectStringTheoryData))]
    public void ShouldReturnCorrectCount(IEnumerable<MonetaryRuleItem> items, int expectedResult, string _)
    {
        var monetaryRule = new MonetaryRule();

        foreach (var item in items)
        {
            monetaryRule.Add(item);
        }

        var actualResultCount = monetaryRule.Count();

        actualResultCount.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(ShouldReturnCorrectStringTheoryData))]
    public void ShouldReturnCorrectString(IEnumerable<MonetaryRuleItem> items, int _, string expectedResult)
    {
        var monetaryRule = new MonetaryRule();

        foreach (var item in items)
        {
            monetaryRule.Add(item);
        }

        var actualResultString = monetaryRule.ToString();

        actualResultString.Should().Be(expectedResult);
    }
}
