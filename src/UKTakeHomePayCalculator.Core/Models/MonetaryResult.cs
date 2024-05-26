using System.Collections;
using System.Text;

namespace UKTakeHomePayCalculator.Core.Models;

public class MonetaryResult : IList<MonetaryResultItem>
{
    private readonly IList<MonetaryResultItem> _items = new List<MonetaryResultItem>();

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
    
    public IEnumerator<MonetaryResultItem> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

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

    public void CopyTo(MonetaryResultItem[] array, int arrayIndex)
    {
        _items.CopyTo(array, arrayIndex);
    }

    public bool Remove(MonetaryResultItem item)
    {
        return _items.Remove(item);
    }

    public int Count => _items.Count;
    public bool IsReadOnly => _items.IsReadOnly;
    public int IndexOf(MonetaryResultItem item)
    {
        return _items.IndexOf(item);
    }

    public void Insert(int index, MonetaryResultItem item)
    {
        _items.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        _items.RemoveAt(index);
    }

    public MonetaryResultItem this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }
    
}
