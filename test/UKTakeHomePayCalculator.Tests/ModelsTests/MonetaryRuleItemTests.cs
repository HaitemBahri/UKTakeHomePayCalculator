using FluentAssertions;
using UKTakeHomePayCalculator.Core.Helpers;
using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Tests.ModelsTests;

public class MonetaryRuleItemTests
{
    public static TheoryData<MonetaryValue, decimal, string> ShouldReturnCorrectStringTheoryData => new()
    {
        {MonetaryValue.Zero, 0.08m, "From 0.00 @ %8.00"},
        {3500m.Year(), 0.12m, "From 3,500.00/Year @ %12.00"},
        {490.87m.Week(), 0.11m, "From 490.87/Week @ %11.00"},
    };

    [Theory]
    [MemberData(nameof(ShouldReturnCorrectStringTheoryData))]
    public void ShouldReturnCorrectString(MonetaryValue fromValue, decimal percentage, string actualResult)
    {
        var monetaryRuleItem = new MonetaryRuleItem(fromValue, percentage);

        var expectedResult = monetaryRuleItem.ToString();

        actualResult.Should().Be(expectedResult); 
    }

    public static TheoryData<MonetaryRuleItem, MonetaryRuleItem> ShouldBeEqualTheoryData => new()
    {
        { new(MonetaryValue.Zero, 0.02m), new(MonetaryValue.Zero, 0.02m) },
        { new(780m.Month(), 0.01m), new(780m.Month(), 0.01m) },
        { new(1780.99m.Month(), 0.12m), new(1780.99m.Month(), 0.12m) }
    };

    [Theory]
    [MemberData(nameof(ShouldBeEqualTheoryData))]
    public void ShouldBeEqual(MonetaryRuleItem value1, MonetaryRuleItem value2)
    {
        value1.Should().Be(value2);
    }

    public static TheoryData<MonetaryRuleItem, MonetaryRuleItem> ShouldBeGreaterThanTheoryData => new()
    {
        {new(MonetaryValue.Zero, 0.02m), new(6280m.Year(), 0.01m) },
        {new(5000.5m.Year(), 0.02m), new(14000.5m.Year(), 0.05m) },
    };

    [Theory]
    [MemberData(nameof(ShouldBeGreaterThanTheoryData))]
    public void ShouldBeGreaterThan(MonetaryRuleItem value1, MonetaryRuleItem value2)
    {
        value2.Should().BeGreaterThan(value1);
    }

    public static TheoryData<MonetaryRuleItem, MonetaryRuleItem> ShouldBeLessThanTheoryData => new()
    {
        {new(6280m.Month(), 0.01m), new(MonetaryValue.Zero, 0.02m) },
        {new(14000.5m.Year(), 0.05m), new(5000.5m.Year(), 0.02m) },
    };

    [Theory]
    [MemberData(nameof(ShouldBeLessThanTheoryData))]
    public void ShouldBeLessThan(MonetaryRuleItem value1, MonetaryRuleItem value2)
    {
        value2.Should().BeLessThan(value1);
    }

    [Theory]
    [MemberData(nameof(ShouldBeEqualTheoryData))]
    [MemberData(nameof(ShouldBeGreaterThanTheoryData))]
    public void ShouldBeGreaterThanOrEqual(MonetaryRuleItem value1, MonetaryRuleItem value2)
    {
        value2.Should().BeGreaterOrEqualTo(value1);
    }
    
    [Theory]
    [MemberData(nameof(ShouldBeEqualTheoryData))]
    [MemberData(nameof(ShouldBeLessThanTheoryData))]
    public void ShouldBeLessThanOrEqual(MonetaryRuleItem value1, MonetaryRuleItem value2)
    {
        value2.Should().BeLessOrEqualTo(value1);
    }
}
