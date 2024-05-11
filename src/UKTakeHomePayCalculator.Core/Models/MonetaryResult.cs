using System.Collections;
using System.Text;

namespace UKTakeHomePayCalculator.Core.Models;

public class MonetaryResult : IEnumerable<MonetaryResultItem>
{
    private readonly List<MonetaryResultItem> _items = new();

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
    
    public static bool operator==(MonetaryResult value1, MonetaryResult value2)
    {
        return !value1._items.Except(value2._items).Any();
    }
    
    public static bool operator!=(MonetaryResult value1, MonetaryResult value2)
    {
        return !(value1 == value2);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        
        if (obj is not MonetaryResult)
            return false;
        
        return this == (MonetaryResult)obj;
    }
}
