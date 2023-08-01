using ECommerceAPI.Application.Features.Product.Commands.UpdateProduct;
using ECommerceAPI.Domain.Entities;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.UpdateProduct;

public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public string Stock { get; set; }
}