using System.Text;
using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Core.Reports;

public class MonetaryReport : IMonetaryReport
{
    private List<IMonetaryReportItem>? _items;

    public void AddMonetaryReportItem(IMonetaryReportItem monetaryReportItem)
    {
        if (_items is null)
            _items = new List<IMonetaryReportItem>();

        _items.Add(monetaryReportItem);
    }

    public MonetaryValue GetNetTotal()
    {
        if (_items is null)
            return MonetaryValue.Zero;

        MonetaryValue total = MonetaryValue.Zero;

        foreach (var item in _items)
        {
            total += item.Value;
        }

        return total.ConvertTo(_items[0].Value.Frequency);
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        
        if (_items != null)
        {
            foreach (var item in _items)
            {
                stringBuilder.Append('\n');

                var itemString = item.ToString();
                
                stringBuilder.Append(itemString);
            }
        }

        stringBuilder.Append($"\n\nNet Value = {GetNetTotal()}");
        
        return stringBuilder.ToString();
    }
}