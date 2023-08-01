using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Application.Repositories.ProductImageFile;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFile.UploadImageFile;

public class UploadImageFileCommandHandler : IRequestHandler<UploadImageFileCommandRequest, UploadImageFileCommandResponse>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IStorageService _storageService;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

    public UploadImageFileCommandHandler(IProductReadRepository productReadRepository, IStorageService storageService,
        IProductImageFileWriteRepository productImageFileWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _storageService = storageService;
        _productImageFileWriteRepository = productImageFileWriteRepository;
    }

    public async Task<UploadImageFileCommandResponse> Handle(UploadImageFileCommandRequest? request, CancellationToken cancellationToken)
    {
        List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", request.Files);


        Domain.Entities.Product? product = await _productReadRepository.GetByIdAsync(request.Id);

        await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.ProductImageFile
        {
            FileName = r.fileName,
            Path = r.pathOrContainerName,
            Storage = _storageService.StorageName,
            Products = new List<Domain.Entities.Product>() { product }
        }).ToList());

        await _productImageFileWriteRepository.SaveAsync();
        return new();
    }
}