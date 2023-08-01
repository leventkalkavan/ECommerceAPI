using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage;

public class DeleteProductImageCommandRequest : IRequest<DeleteProductImageCommandResponse>
{
    public string Id { get; set; }
    [FromQuery]
    public string? ImageId { get; set; }
}