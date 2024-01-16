using FluentAssertions;
using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Tests.ModelsTests
{
    public class MonetaryFrequencyTests
    {
        public static TheoryData<MonetaryFrequency, int, string> MonetaryFrequency_TestData =>
            new()
            {
                { MonetaryFrequency.Week, 7, "Week" },
                { MonetaryFrequency.Month, 30, "Month" },
                { MonetaryFrequency.Year, 365, "Year" },

                { MonetaryFrequency.Weeks(1), 7, "Week" },
                { MonetaryFrequency.Weeks(2), 14, "2 Weeks" },
                { MonetaryFrequency.Weeks(4), 28, "4 Weeks" },

                { MonetaryFrequency.Months(1), 30, "Month" },
                { MonetaryFrequency.Months(2), 60, "2 Months" },
                { MonetaryFrequency.Months(3), 90, "3 Months" },
            };

        [Theory]
        [MemberData(nameof(MonetaryFrequency_TestData))]
        public void ShouldConvertMonetaryFrequencyToInt(MonetaryFrequency monetaryFrequency, int expectedResult, string stringValue)
        {
            var actualResult = (int)monetaryFrequency;

            actualResult.Should().Be(expectedResult, stringValue);
        }

        [Theory]
        [MemberData(nameof(MonetaryFrequency_TestData))]
        public void ShouldConvertMonetaryFrequencyToString(MonetaryFrequency monetaryFrequency, int numberOfDays, string expectedResult)
        {
            var actualResult = monetaryFrequency.ToString();

            actualResult.Should().Be(expectedResult, "NumberOfDays = " + numberOfDays);
        }

    }
}