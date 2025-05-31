using WaterShop.Goods.Domain.Primitives;

namespace WaterShop.Goods.Domain.Errors
{
    public record InvalidPageableDataError : Error
    {
        public InvalidPageableDataError() : base("IPD", "Got incorrect pageable data")
        {
        }
    }
}
