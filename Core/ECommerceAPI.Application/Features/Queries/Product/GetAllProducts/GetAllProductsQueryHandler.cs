using ECommerceAPI.Application.Features.Queries.Product.GetAllProducts;
using ECommerceAPI.Application.Repositories.Product;
using MediatR;

namespace ECommerceAPI.Application.Features.Product.Queries.GetAllProducts;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQueryRequest, GetAllProductsQueryResponse>
{
    readonly IProductReadRepository _productReadRepository;
    public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }
    public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var totalCount = _productReadRepository.GetAll(false).Count();
        var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size).Select(p => new
        {
            p.Id,
            p.Name,
            p.Stock,
            p.Price,
            p.CreatedDate,
            p.UpdatedDate
        }).ToList();

        return new()
        {
            Products = products,
            TotalCount = totalCount
        };
    }
}