using FluentAssertions;
using UKTakeHomePayCalculator.Core.Helpers;
using UKTakeHomePayCalculator.Core.Models;
using UKTakeHomePayCalculator.Core.Reports;

namespace UKTakeHomePayCalculator.Tests.ReportsTests;

public class MonetaryReportTests
{
    public static TheoryData<IMonetaryReportItem, IMonetaryReportItem, IMonetaryReportItem, MonetaryValue, string>
        MonetaryReportTestsTheoryData => new()
    {
        {
            new MonetaryReportItem("Basic Income", 95340.34m.Year()),
            
            new MonetaryReportItem("Tax", 
                new MonetaryReportItem("Tax @ 20%", -1214.5m.Year()),
                new MonetaryReportItem("Tax @ 40%", -124.5m.Year()),
                new MonetaryReportItem("Tax @ 47%", -2.4m.Year())),
            
            new MonetaryReportItem("National Insurance",
                new MonetaryReportItem("NI @ 10%", -724.5m.Year()),
                new MonetaryReportItem("NI @ 2%", -42.4m.Year())),
            
            93232.04m.Year(),
            
            "\nBasic Income = 95,340.34/Year" +
            "\nTax" +
            "\n\tTax @ 20% = -1,214.50/Year" +
            "\n\tTax @ 40% = -124.50/Year" +
            "\n\tTax @ 47% = -2.40/Year" +
            "\nNational Insurance" +
            "\n\tNI @ 10% = -724.50/Year" +
            "\n\tNI @ 2% = -42.40/Year" +
            "\n" +
            "\nNet Value = 93,232.04/Year"
        }
    };

    [Theory]
    [MemberData(nameof(MonetaryReportTestsTheoryData))]
    public void ShouldReturnCorrectTotalValue(IMonetaryReportItem item1, IMonetaryReportItem item2,
        IMonetaryReportItem item3, MonetaryValue expectedResult, string _)
    {
        var sut = new MonetaryReport();
        
        sut.AddMonetaryReportItem(item1);
        sut.AddMonetaryReportItem(item2);
        sut.AddMonetaryReportItem(item3);

        var actualResult = sut.GetNetTotal();

        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(MonetaryReportTestsTheoryData))]
    public void ShouldReturnCorrectString(IMonetaryReportItem item1, IMonetaryReportItem item2,
        IMonetaryReportItem item3, MonetaryValue _, string expectedResult)
    {
        var sut = new MonetaryReport();
        
        sut.AddMonetaryReportItem(item1);
        sut.AddMonetaryReportItem(item2);
        sut.AddMonetaryReportItem(item3);

        var actualResult = sut.ToString();

        actualResult.Should().Be(expectedResult);
    }
}