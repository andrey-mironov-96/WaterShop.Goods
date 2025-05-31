using WaterShop.Goods.Domain.Primitives;

namespace WaterShop.Goods.Domain.Errors;

public record CriticalError : Error
{
    public CriticalError() : base("Alarm.CE", "Something went wrong.")
    {
    }

}
