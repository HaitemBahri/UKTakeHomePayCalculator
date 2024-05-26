using System.Collections;
using System.Text;

namespace UKTakeHomePayCalculator.Core.Models;

public class MonetaryRule : IList<MonetaryRuleItem>
{
    private readonly IList<MonetaryRuleItem> _items = new List<MonetaryRuleItem>();
    public IList<MonetaryRuleItem> Items => _items.OrderBy(x => x.FromValue).ToList();

    public override string ToString()
    {
        var result = new StringBuilder();
    
        foreach (var item in Items)
        {
            result.Append(item.ToString());
            result.Append("\n");
        }
    
        var stringResult = result.ToString().TrimEnd('\n');
    
        return stringResult;
    }
    
    public IEnumerator<MonetaryRuleItem> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

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
        return Items.Contains(item);
    }

    public void CopyTo(MonetaryRuleItem[] array, int arrayIndex)
    {
        Items.CopyTo(array, arrayIndex);
    }

    public bool Remove(MonetaryRuleItem item)
    {
        return _items.Remove(item);
    }

    public int Count => Items.Count;
    public bool IsReadOnly => Items.IsReadOnly;
    public int IndexOf(MonetaryRuleItem item)
    {
        return Items.IndexOf(item);
    }

    public void Insert(int index, MonetaryRuleItem item)
    {
        Items.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        Items.RemoveAt(index);
    }

    public MonetaryRuleItem this[int index]
    {
        get => Items[index];
        set => Items[index] = value;
    }
}
