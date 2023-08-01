using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages;

public class GetProductImagesQueryRequest : IRequest<List<GetProductImagesQueryResponse>>
{
    public string Id { get; set; }
}