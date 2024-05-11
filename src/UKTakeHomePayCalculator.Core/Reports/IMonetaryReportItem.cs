using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Core.Reports;

public interface IMonetaryReportItem
{
    public string Name { get; }
    public MonetaryValue Value { get; }
    public List<IMonetaryReportItem>? Items { get; }
}