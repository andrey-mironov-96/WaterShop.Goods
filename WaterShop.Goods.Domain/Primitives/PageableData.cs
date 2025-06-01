using WaterShop.Goods.Domain.Primitives.Filters;

namespace WaterShop.Goods.Domain.Primitives;

public class PageableData<TEntity>
where TEntity : class
{
    private uint _total;
    private ushort _page;
    private ushort _pageSize;
    public PageableData(ushort page, ushort pageSize)
    {
        Page = page;
        PageSize = pageSize;
        Data = new List<TEntity>();
    }

    public uint Total
    {
        get => _total;
        set
        {
            _total = value;
            NormalizePage();
        }
    }

    public ushort Page
    {
        get => _page;
        set
        {
            _page = value;
            NormalizePage();
        }
    }

    public ushort PageSize
    {
        get => _pageSize;
        set
        {
            _pageSize = value;
            NormalizePage();
        }
    }

    public IEnumerable<TEntity> Data { get; set; }

    public Filter? Filter { get; set; }

    public int GetSkipped() => (Page - 1) * PageSize;

    private void NormalizePage()
    {
        if (Total > 0 && PageSize > 0 && Page > 0)
        {
            uint lastPage = Total / PageSize;
            if (Total % PageSize > 0)
            {
                lastPage++;
            }

            if (Page > lastPage)
            {
                Page = (ushort)lastPage;
            }
        }
    }

}
