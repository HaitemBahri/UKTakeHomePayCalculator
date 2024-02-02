using System.Collections;
using System.Text;

namespace UKTakeHomePayCalculator.Core.Models;

public class MonetaryResult : IEnumerable<MonetaryResultItem>
{
    private readonly List<MonetaryResultItem> _items;

    public MonetaryResult()
    {
        _items = new List<MonetaryResultItem>();
    }

    public int Count => _items.Count;

    public void Add(MonetaryResultItem item)
    {
        _items.Add(item);
    }

    public void Clear()
    {
        _items.Clear();
    }

    public bool Contains(MonetaryResultItem item)
    {
        return _items.Contains(item);
    }

    public IEnumerator<MonetaryResultItem> GetEnumerator()
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
