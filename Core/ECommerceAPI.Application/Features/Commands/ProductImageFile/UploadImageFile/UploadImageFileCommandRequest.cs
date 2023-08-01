using MediatR;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFile.UploadImageFile;

public class UploadImageFileCommandRequest : IRequest<UploadImageFileCommandResponse>
{
    public string Id { get; set; }
    public IFormFileCollection? Files { get; set; }
}