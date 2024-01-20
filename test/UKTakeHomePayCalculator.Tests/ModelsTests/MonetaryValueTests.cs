using System.Security.AccessControl;
using FluentAssertions;
using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Tests;

public class MonetaryValueTests
{
    #region Extensions
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

    #endregion

    #region Math
    public static TheoryData<MonetaryValue, MonetaryValue, MonetaryValue, string> ShouldAddTwoMonetaryValuesTheoryData => new()
    {
        {450m.Month(), 1200m.Month(), 1650m.Month(), "Positive + Positive"},
        {450m.Week(), -1200m.Week(), -750m.Week(), "Positive + Negative"},
        {-450m.Year(), -1200m.Year(), -1650m.Year(), "Negative + Negative" },
        {MonetaryValue.PositiveInfinity, 200m.Year(), MonetaryValue.PositiveInfinity, "Positive Infinity + Positive" },
        {-4300m.Week(), MonetaryValue.PositiveInfinity, MonetaryValue.PositiveInfinity, "Positive Infinity + Negative" },
        {MonetaryValue.NegativeInfinity, 200m.Year(), MonetaryValue.NegativeInfinity, "Negative Infinity + Positive" },
        {-4300m.Week(), MonetaryValue.NegativeInfinity, MonetaryValue.NegativeInfinity, "Negative Infinity + Negative" },
    };

    [Theory]
    [MemberData(nameof(ShouldAddTwoMonetaryValuesTheoryData))]
    public void ShouldAddTwoMonetaryValues(MonetaryValue value1, MonetaryValue value2, MonetaryValue expectedResult, string testDataName)
    {
        var actualResult = value1 + value2;

        actualResult.Should().Be(expectedResult, testDataName);
    }

    public static TheoryData<MonetaryValue, MonetaryValue, MonetaryValue, string> ShouldSubtractTwoMonetaryValuesTheoryData => new()
    {
        {1450m.Month(), 1200m.Month(), 250m.Month(), "Positive - Positive"},
        {450m.Year(), -1200m.Year(), 1650m.Year(), "Positive - Negative"},
        {-450m.Month(), -1200m.Month(), 750m.Month(), "Negative - Negative" },
        {-900m.Week(), 9200m.Week(), -10100m.Week(), "Negative - Positive" },
        {MonetaryValue.PositiveInfinity, 200m.Year(), MonetaryValue.PositiveInfinity, "Positive Infinity - Positive" },
        {-4300m.Week(), MonetaryValue.PositiveInfinity, MonetaryValue.NegativeInfinity, "Negative - Positive Infinity" },
        {MonetaryValue.NegativeInfinity, 200m.Year(), MonetaryValue.NegativeInfinity, "Negative Infinity - Positive" },
        {-4300m.Week(), MonetaryValue.NegativeInfinity, MonetaryValue.PositiveInfinity, "Negative - Negative Infinity" },
    };

    [Theory]
    [MemberData(nameof(ShouldSubtractTwoMonetaryValuesTheoryData))]
    public void ShouldSubtractTwoMonetaryValues(MonetaryValue value1, MonetaryValue value2, MonetaryValue expectedResult, string testDataName)
    {
        var actualResult = value1 - value2;

        actualResult.Should().Be(expectedResult, testDataName);
    }

    public static TheoryData<MonetaryValue, MonetaryValue, string> ShouldNegateValue_WhenPrecededWithNegativeSignTheoryData => new()
    {
        {1450.47m.Month(), -1450.47m.Month(), "Positive value"},
        {-560m.Year(), 560m.Year(), "Negative value"},
        {MonetaryValue.PositiveInfinity, MonetaryValue.NegativeInfinity, "Positive Infinity"},
        {MonetaryValue.NegativeInfinity, MonetaryValue.PositiveInfinity, "Negative Infinity"},
    };

    [Theory]
    [MemberData(nameof(ShouldNegateValue_WhenPrecededWithNegativeSignTheoryData))]
    public void ShouldNegateValue_WhenPrecededWithNegativeSign(MonetaryValue value, MonetaryValue expectedResult, string testDataName)
    {
        var actualResult = -value;

        actualResult.Should().Be(expectedResult, testDataName);
    }

    public static TheoryData<MonetaryValue, decimal, MonetaryValue, string> ShouldMultiplyWithDecimalTheoryData => new()
    {
        {55m.Month(), 6.9m, 379.5m.Month(), "Positive multiplier"},
        {69000m.Year(), -0.4m, -27600m.Year(), "Negative multiplier"},
        {MonetaryValue.PositiveInfinity, 0.4m, MonetaryValue.PositiveInfinity, "Positive Infinity * Positive"},
        {MonetaryValue.PositiveInfinity, -0.4m, MonetaryValue.NegativeInfinity, "Positive Infinity * Negative"},
        {MonetaryValue.NegativeInfinity, 0.4m, MonetaryValue.NegativeInfinity, "Positive Infinity * Positive"},
        {MonetaryValue.NegativeInfinity, -0.4m, MonetaryValue.PositiveInfinity, "Positive Infinity * Negative"},
    };

    [Theory]
    [MemberData(nameof(ShouldMultiplyWithDecimalTheoryData))]
    public void ShouldMultiplyWithDecimal(MonetaryValue value, decimal multiplier, MonetaryValue expectedResult, string testDataName)
    {
        var actualResult = value * multiplier;

        actualResult.Should().Be(expectedResult, testDataName);
    }

    public static TheoryData<MonetaryValue, decimal, MonetaryValue, string> ShouldDivideByDecimalTheoryData => new()
    {
        {55m.Month(), 6.9m, 7.97m.Month(), "Positive Divider"},
        {69000m.Year(), -0.4m, -172500m.Year(), "Negative Divider"},
        {MonetaryValue.PositiveInfinity, 0.4m, MonetaryValue.PositiveInfinity, "Positive Infinity / Positive"},
        {MonetaryValue.PositiveInfinity, -0.4m, MonetaryValue.NegativeInfinity, "Positive Infinity / Negative"},
        {MonetaryValue.NegativeInfinity, 0.4m, MonetaryValue.NegativeInfinity, "Positive Infinity / Positive"},
        {MonetaryValue.NegativeInfinity, -0.4m, MonetaryValue.PositiveInfinity, "Positive Infinity / Negative"},
    };

    [Theory]
    [MemberData(nameof(ShouldDivideByDecimalTheoryData))]
    public void ShouldDivideByDecimal(MonetaryValue value, decimal divider, MonetaryValue expectedResult, string testDataName)
    {
        var actualResult = value / divider;

        actualResult.Should().Be(expectedResult, testDataName);
    }

    [Fact]
    public void ShouldThrowDivideByZeroExceptionWhenDividingByZero()
    {
        var _sut = 4599.39903m.Year();
        var divider = 0;

        Func<MonetaryValue> func = () => _sut / divider;

        func.Should().Throw<DivideByZeroException>();
    }


    #endregion

    #region Comparison 

    public static TheoryData<MonetaryValue, MonetaryValue, string> ShouldOneValueBeGreaterThanOtherValueTheoryData => new()
    {
        {900m.Week(), 63m.Week(), "Positive - Different Values and Same Frequency"},
        {-290m.Month(), -1800m.Month(), "Negative - Different Values and Same Frequency"},
        {2500m.Month(), 5000m.Year(), "Different Frequencies"},
        {MonetaryValue.PositiveInfinity, 4700m.Week(), "Positive Infinity > Positive Value"},
        {MonetaryValue.PositiveInfinity, -9700m.Year(), "Positive Infinity > Negative Value"},
        {-9700m.Week(), MonetaryValue.NegativeInfinity, "Positive Infinity > Negative Value"},
        {30m.Year(), MonetaryValue.NegativeInfinity, "Positive Value > Negative Infinity"},
        {-190m.Week(), MonetaryValue.NegativeInfinity, "Negative Value > Negative Infinity"},
        {MonetaryValue.PositiveInfinity, MonetaryValue.NegativeInfinity, "Positive Infinity > Negative Infinity"},
    };

    [Theory]
    [MemberData(nameof(ShouldOneValueBeGreaterThanOtherValueTheoryData))]
    public void ShouldOneValueBeGreaterThanOtherValue(MonetaryValue value1, MonetaryValue value2, string testDataName)
    {
        //value1.Should().BeGreaterThan(value2, testDataName);
        Assert.True(value1 > value2);
    }

    [Theory]
    [MemberData(nameof(ShouldOneValueBeGreaterThanOtherValueTheoryData))]
    [MemberData(nameof(ShouldTwoMonetaryValuesBeEqualTheoryData))]
    public void ShouldOneValueBeGreaterThanOrEqualOtherValue(MonetaryValue value1, MonetaryValue value2, string testDataName)
    {
        //value1.Should().BeGreaterThanOrEqualTo(value2, testDataName);
        Assert.True(value1 >= value2);
    }

    public static TheoryData<MonetaryValue, MonetaryValue, string> ShouldOneValueBeLessThanOtherValueTheoryData => new()
    {
        {63m.Week(), 900m.Week(), "Positive - Different Values and Same Frequency"},
        {-1800m.Month(), -290m.Month(), "Negative - Different Values and Same Frequency"},
        {5000m.Year(), 2500m.Month(), "Different Frequencies"},
        {4700m.Week(), MonetaryValue.PositiveInfinity, "Positive Value < Positive Infinity"},
        {-9700m.Year(), MonetaryValue.PositiveInfinity, "Negative Value < Positive Infinity"},
        {MonetaryValue.NegativeInfinity, -9700m.Week(), "Negative Value < Positive Infinity"},
        {MonetaryValue.NegativeInfinity, 30m.Year(), "Negative Infinity < Positive Value"},
        {MonetaryValue.NegativeInfinity, -190m.Week(), "Negative Infinity < Negative Value"},
        {MonetaryValue.NegativeInfinity, MonetaryValue.PositiveInfinity, "Negative Infinity < Positive Infinity"},
    };

    [Theory]
    [MemberData(nameof(ShouldOneValueBeLessThanOtherValueTheoryData))]
    public void ShouldOneValueBeLessThanOtherValue(MonetaryValue value1, MonetaryValue value2, string testDataName)
    {
        value1.Should().BeLessThan(value2, testDataName);
    }

    [Theory]
    [MemberData(nameof(ShouldOneValueBeLessThanOtherValueTheoryData))]
    [MemberData(nameof(ShouldTwoMonetaryValuesBeEqualTheoryData))]
    public void ShouldOneValueBeLessThanOrEqualOtherValue(MonetaryValue value1, MonetaryValue value2, string testDataName)
    {
        value1.Should().BeLessThanOrEqualTo(value2, testDataName);
    }

    public static TheoryData<MonetaryValue, MonetaryValue, string> ShouldTwoMonetaryValuesBeEqualTheoryData => new()
    {
        {900m.Month(), 900m.Month(), "Same Value and Frequency"},
        {-900m.Month(), -210m.Week(), "Different Value and Different Frequency"},
        {900.68m.Year(), 900.68m.Year(), "Same Value with Decimal values"},
        {1320.557m.Week(), 1320.56m.Week(), "Rounding up"},
        {16.9243m.Month(), 16.92m.Month(), "Not Rounding up"},
        {3.135m.Week(), 3.14m.Week(), "Rounding [Odd] second decimal up with .5"},
        {3.145m.Month(), 3.14m.Month(), "Not Rounding [Even] second decimal up with .5"},
        {MonetaryValue.PositiveInfinity, MonetaryValue.PositiveInfinity, "Positive Infinity"},
        {MonetaryValue.NegativeInfinity, MonetaryValue.NegativeInfinity, "Negative Infinity"},
        {MonetaryValue.NegativeInfinity, -MonetaryValue.PositiveInfinity, "Negative Infinity"},
        {MonetaryValue.PositiveInfinity, -MonetaryValue.NegativeInfinity, "Positive Infinity"},
    };

    [Theory]
    [MemberData(nameof(ShouldTwoMonetaryValuesBeEqualTheoryData))]
    public void ShouldTwoMonetaryValuesBeEqual(MonetaryValue value1, MonetaryValue value2, string testDataName)
    {
        value1.Should().Be(value2, testDataName);
    }

    public static TheoryData<MonetaryValue, MonetaryValue, string> ShouldTwoMonetaryValuesNotBeEqualTheoryData => new()
    {
        {900m.Month(), 63m.Month(), "Different Value but Same Frequency"},
        {-900m.Month(), -900m.Week(), "Same Value but Different Frequency"},
        {MonetaryValue.PositiveInfinity, MonetaryValue.NegativeInfinity, "Different Frequencies"},
    };

    [Theory]
    [MemberData(nameof(ShouldTwoMonetaryValuesNotBeEqualTheoryData))]
    public void ShouldTwoMonetaryValuesNotBeEqual(MonetaryValue value1, MonetaryValue value2, string testDataName)
    {
        value1.Should().NotBe(value2, testDataName);
    }

    #endregion

    #region Others

    public static TheoryData<MonetaryValue, MonetaryFrequency, MonetaryValue, string> ShouldConvertToDifferentFrequencyTheoryData => new()
    {
        {61.887m.Week(), MonetaryFrequency.Month,  265.23m.Month(), "Week to Month"},
        {61.887m.Week(), MonetaryFrequency.Year,  3226.965m.Year(), "Week to Year"},

        {2395.6666m.Month(), MonetaryFrequency.Week,  558.99m.Week(), "Month to Week"},
        {2395.6666m.Month(), MonetaryFrequency.Year,  29147.27697m.Year(), "Month to Year"},

        {12299.51m.Year(), MonetaryFrequency.Week,  235.881m.Week(), "Year to Week"},
        {12299.51m.Year(), MonetaryFrequency.Month,  1010.9186m.Month(), "Year to Month"},

        {-61.887m.Week(), MonetaryFrequency.Month,  -265.23m.Month(), "Week to Month (Negative)"},
        {-61.887m.Week(), MonetaryFrequency.Year,  -3226.965m.Year(), "Week to Year (Negative)"},

        {-2395.6666m.Month(), MonetaryFrequency.Week,  -558.9887m.Week(), "Month to Week (Negative)"},
        {-2395.6666m.Month(), MonetaryFrequency.Year,  -29147.27697m.Year(), "Month to Year (Negative)"},

        {-12299.51m.Year(), MonetaryFrequency.Week,  -235.881m.Week(), "Year to Week (Negative)"},
        {-12299.51m.Year(), MonetaryFrequency.Month,  -1010.9186m.Month(), "Year to Month (Negative)"},

        {MonetaryValue.PositiveInfinity, MonetaryFrequency.Week,  MonetaryValue.PositiveInfinity, "Positive Infinity"},
        {MonetaryValue.NegativeInfinity, MonetaryFrequency.Month,  MonetaryValue.NegativeInfinity, "Negative Infinity"},
    };

    [Theory, MemberData(nameof(ShouldConvertToDifferentFrequencyTheoryData))]
    public void ShouldConvertToDifferentFrequency(MonetaryValue value, MonetaryFrequency frequency, MonetaryValue expectedResult, string testDataName)
    {
        var actualResult = value.ConvertTo(frequency);

        actualResult.Should().Be(expectedResult, testDataName);
    }

    public static TheoryData<MonetaryValue, string, string> ShouldReturnCorrectStringTheoryData => new()
    {
        {55m.Month(), "55.00/Month", "Positive, Zero Decimal"},
        {-155.99m.Week(), "-155.99/Week", "Negative, Non Zero Decimal"},
        {19500m.Year(), "19,500.00/Year", "Decimal Separator"},
        {-6919500.966m.Month(), "-6,919,500.97/Month", "Rounding Second Number"},
        {MonetaryValue.PositiveInfinity, "+Infinity", "Positive Infinity"},
        {MonetaryValue.NegativeInfinity, "-Infinity", "Negative Infinity"},
    };

    [Theory]
    [MemberData(nameof(ShouldReturnCorrectStringTheoryData))]
    public void ShouldReturnCorrectString(MonetaryValue value, string expectedResult, string testDataName)
    {
        var actualResult = value.ToString();

        actualResult.Should().Be(expectedResult, testDataName);
    }

    #endregion
}
