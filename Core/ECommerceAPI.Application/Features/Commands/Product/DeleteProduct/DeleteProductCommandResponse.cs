using MediatR;

namespace ECommerceAPI.Application.Features.Product.Commands.DeleteProduct;

public class DeleteProductCommandResponse : IRequest<DeleteProductCommandRequest>
{
    public bool IsSuccess { get; set; }
}