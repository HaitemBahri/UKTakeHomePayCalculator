using FluentAssertions;
using UKTakeHomePayCalculator.Core.Helpers;
using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Tests.HelpersTests;

public class MonetaryValueExtensionsTests
{
    public static TheoryData<MonetaryValue, MonetaryValue, string> ShouldConvertUsingExtensionMethodsTheoryData => new()
    {
        {new MonetaryValue(4500m, MonetaryFrequency.Week), 4500m.Week(), "Week Value"},
        {new MonetaryValue(40.55m, MonetaryFrequency.Month), 40.55m.Month(), "Month Value"},
        {new MonetaryValue(12550m, MonetaryFrequency.Year), 12550m.Year(), "Year Value" },

        {new MonetaryValue(38000m, MonetaryFrequency.Week), 38000m.Every(MonetaryFrequency.Week), "Week Value using Every() Method"},
        {new MonetaryValue(3.10m, MonetaryFrequency.Month), 3.10m.Every(MonetaryFrequency.Month), "Month Value using Every() Method"},
        {new MonetaryValue(0.75m, MonetaryFrequency.Year), 0.75m.Every(MonetaryFrequency.Year), "Year Value using Every() Method" },
    };

    [Theory]
    [MemberData(nameof(ShouldConvertUsingExtensionMethodsTheoryData))]

    public void ShouldConvertUsingExtensionMethods(MonetaryValue expectedResult, MonetaryValue actualResult, string testDataName)
    {
        actualResult.Should().Be(expectedResult, testDataName);
    }
}