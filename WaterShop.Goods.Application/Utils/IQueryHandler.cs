using MediatR;

namespace WaterShop.Goods.Application.Utils;

public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQuery<TResponse>
{ }
