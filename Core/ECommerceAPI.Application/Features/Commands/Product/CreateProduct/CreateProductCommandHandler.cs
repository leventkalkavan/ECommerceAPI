using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.Application.Repositories.Product;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductHubService _productHubService;
    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService)
    {
        _productWriteRepository = productWriteRepository;
        _productHubService = productHubService;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request,
        CancellationToken cancellationToken)
    {
        await _productWriteRepository.AddAsync(new Domain.Entities.Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        });
        await _productWriteRepository.SaveAsync();
        await _productHubService.ProductAddedMessageAsync($"{request.Name} urunu eklendi");
        return new CreateProductCommandResponse
        {
            IsSuccess = true
        };
    }
}