using MediatR;

namespace WaterShop.Goods.Application.Utils;

public interface IQuery<out TResponse> : IRequest<TResponse>
{

}
