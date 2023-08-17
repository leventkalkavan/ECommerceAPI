using ECommerceAPI.Application.Features.Commands.UpdateProduct;
using ECommerceAPI.Application.Repositories.Product;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceAPI.Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly ILogger<UpdateProductCommandHandler> _logger;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public UpdateProductCommandHandler(IProductReadRepository productReadRepository,
        IProductWriteRepository productWriteRepository, ILogger<UpdateProductCommandHandler> logger)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _logger = logger;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request,
        CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetByIdAsync(request.Id);
        product.Stock = request.Stock;
        product.Name = request.Name;
        product.Price = request.Price;
        await _productWriteRepository.SaveAsync();
        _logger.LogInformation("product updatess..");
        return new UpdateProductCommandResponse
        {
            IsSuccess = true
        };
    }
}