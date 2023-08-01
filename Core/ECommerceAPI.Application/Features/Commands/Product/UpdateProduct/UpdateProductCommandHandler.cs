using ECommerceAPI.Application.Features.Commands.UpdateProduct;
using ECommerceAPI.Application.Repositories.Product;
using MediatR;

namespace ECommerceAPI.Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest,UpdateProductCommandResponse>
{ 
    readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;

    public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
        product.Stock = request.Stock;
        product.Name = request.Name;
        product.Price = request.Price;
        await _productWriteRepository.SaveAsync();
        return new()
        {
            IsSuccess = true
        };
    }
}