using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Core.Reports;

public interface IMonetaryReport
{
    public void AddMonetaryReportItem(IMonetaryReportItem monetaryReportItem);
    public MonetaryValue GetNetTotal();
}