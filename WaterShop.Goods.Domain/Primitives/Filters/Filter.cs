namespace WaterShop.Goods.Domain.Primitives.Filters;

public abstract class Filter
{
    public abstract List<FilterItem> Filters { get; set; }

    public void AddFilter(string label, string value)
    {
        Filters.Add(new(){Label = label, Value = value});
    }
}