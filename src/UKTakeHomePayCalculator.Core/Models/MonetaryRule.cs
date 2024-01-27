using System.Collections;
using System.Text;

namespace UKTakeHomePayCalculator.Core.Models;

public class MonetaryRule : IEnumerable<MonetaryRuleItem>
{
    private readonly List<MonetaryRuleItem> _items;

    public MonetaryRule()
    {
        _items = new List<MonetaryRuleItem>();
    }

    public int Count => _items.Count;

    public void Add(MonetaryRuleItem item)
    {
        _items.Add(item);
    }

    public void Clear()
    {
        _items.Clear();
    }

    public bool Contains(MonetaryRuleItem item)
    {
        return _items.Contains(item);
    }

    public IEnumerator<MonetaryRuleItem> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        var result = new StringBuilder();

        foreach (var item in _items)
        {
            result.Append(item.ToString());
            result.Append("\n");
        }

        var stringResult = result.ToString().TrimEnd('\n');

        return stringResult;
    }
}
