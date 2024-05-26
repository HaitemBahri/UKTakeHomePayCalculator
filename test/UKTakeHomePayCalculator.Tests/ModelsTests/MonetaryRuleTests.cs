using FluentAssertions;
using UKTakeHomePayCalculator.Core.Helpers;
using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Tests.ModelsTests;

public class MonetaryRuleTests
{
    public static TheoryData<IEnumerable<MonetaryRuleItem>, int, string> ShouldReturnCorrectStringTheoryData => new()
    {
        {   
            new List<MonetaryRuleItem> ()
            {   
                new (0m.Week(), 0.10m),
                new (60000m.Year(), 0.01m),
                new (3000m.Year(), 0.02m),
            },

        3,

        "From 0.00 @ %10.00\n" +
            "From 3,000.00/Year @ %2.00\n" +
            "From 60,000.00/Year @ %1.00" 
        }
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
