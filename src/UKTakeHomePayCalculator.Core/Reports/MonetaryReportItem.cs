using System.Text;
using UKTakeHomePayCalculator.Core.Models;

namespace UKTakeHomePayCalculator.Core.Reports;

public class MonetaryReportItem : IMonetaryReportItem
{
    private string _name;
    private MonetaryValue _value;
    private List<IMonetaryReportItem>? _items;

    public string Name => _name;
    public MonetaryValue Value
    {
        get
        {
            if (_items is null || _items.Count < 1)
                return _value;

            var value = MonetaryValue.Zero;

            foreach (var item in _items)
            {
                value += item.Value;
            }

            return value;
        }
    }
    public List<IMonetaryReportItem>? Items
    {
        get
        {
            if (_items != null) 
                return new List<IMonetaryReportItem>(_items);

            return null;
        }
    }

    public MonetaryReportItem(string name, MonetaryValue value)
    {
        _name = name;
        _value = value;
    }

    public MonetaryReportItem(string name, params MonetaryReportItem[] items)
    {
        _name = name;
        _items = new List<IMonetaryReportItem>(items);
    }

    public override string ToString()
    {
        if (_items != null)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(Name);

            foreach (var item in _items)
            {
                stringBuilder.Append('\n');

                var itemString = item.ToString();

                var updatedItemString = itemString?.Replace("\n", "\n\t");

                stringBuilder.Append("\t").Append(updatedItemString);
            }

            return stringBuilder.ToString();
        }
        
        return $"{_name} = {_value}";
    }
}