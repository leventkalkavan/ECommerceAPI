using ECommerceAPI.Application.Repositories.Product;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages;

public class
    GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
{
    private readonly IProductReadRepository _productReadRepository;

    private readonly IConfiguration configuration;

    public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
    {
        _productReadRepository = productReadRepository;
        this.configuration = configuration;
    }

    public async Task<List<GetProductImagesQueryResponse>?> Handle(GetProductImagesQueryRequest request,
        CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
        return product?.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
        {
            Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
            FileName = p.FileName,
            Id = p.Id
        }).ToList();
    }
}