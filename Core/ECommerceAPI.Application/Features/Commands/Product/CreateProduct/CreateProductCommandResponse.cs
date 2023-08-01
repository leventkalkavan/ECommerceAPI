using MediatR;

namespace ECommerceAPI.Application.Features.Commands.Product.CreateProduct;

public class CreateProductCommandResponse : Domain.Entities.Product, IRequest<CreateProductCommandRequest>
{
    public bool IsSuccess { get; set; }
}