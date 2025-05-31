using MediatR;

namespace WaterShop.Goods.Application.Utils;

public interface ICommand<out TResponse> : IRequest<TResponse>
{

}
