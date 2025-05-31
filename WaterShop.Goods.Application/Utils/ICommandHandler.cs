using MediatR;

namespace WaterShop.Goods.Application.Utils;

public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{

}

