using FluentAssertions;
using UKTakeHomePayCalculator.Core.Helpers;
using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Tests.ModelsTests;

public class MonetaryResultTests
{
    public static TheoryData<List<MonetaryResultItem>, int, string> ShouldReturnCorrectStringTheoryData => new()
    {
        {   new List<MonetaryResultItem>
            {
                new(new MonetaryRuleItem(MonetaryValue.Zero, 3500m.Year(), 0.08m), 120m.Week()),
                new(new MonetaryRuleItem(1200m.Year(), 3500m.Year(), 0.12m), 10500m.Year()),
                new(new MonetaryRuleItem(450000m.Year(), MonetaryValue.PositiveInfinity, 0.02m), 2890m.Month())
            },

            3,

            "Rule = [From 0.00/Week - To 3,500.00/Year] %8.00, Result = 120.00/Week\n"
             + "Rule = [From 1,200.00/Year - To 3,500.00/Year] %12.00, Result = 10,500.00/Year\n"
             + "Rule = [From 450,000.00/Year - To +Infinity] %2.00, Result = 2,890.00/Month"    }
    };

    [Theory]
    [MemberData(nameof(ShouldReturnCorrectStringTheoryData))]
    public void ShouldReturnCorrectCount(List<MonetaryResultItem> items, int expectedResult, string _)
    {
        var monetaryResult = new MonetaryResult();

        foreach (var item in items)
        {
            monetaryResult.Add(item);
        }

        var actualResult = monetaryResult.Count();

        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(ShouldReturnCorrectStringTheoryData))]
    public void ShouldReturnCorrectString(List<MonetaryResultItem> items, int _, string expectedResult)
    {
        var monetaryResult = new MonetaryResult();

        foreach (var item in items)
        {
            monetaryResult.Add(item);
        }

        var actualResult = monetaryResult.ToString();

        actualResult.Should().Be(expectedResult);
    }
}