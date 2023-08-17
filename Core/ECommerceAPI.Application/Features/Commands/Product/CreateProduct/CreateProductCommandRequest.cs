using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
{
    public string Name { get; set; }
    public float Price { get; set; }
    public string Stock { get; set; }
}