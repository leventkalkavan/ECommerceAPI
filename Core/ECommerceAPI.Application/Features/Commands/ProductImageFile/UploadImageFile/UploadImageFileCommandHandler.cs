using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Application.Repositories.ProductImageFile;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ProductImageFile.UploadImageFile;

public class
    UploadImageFileCommandHandler : IRequestHandler<UploadImageFileCommandRequest, UploadImageFileCommandResponse>
{
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IStorageService _storageService;

    public UploadImageFileCommandHandler(IProductReadRepository productReadRepository, IStorageService storageService,
        IProductImageFileWriteRepository productImageFileWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _storageService = storageService;
        _productImageFileWriteRepository = productImageFileWriteRepository;
    }

    public async Task<UploadImageFileCommandResponse> Handle(UploadImageFileCommandRequest? request,
        CancellationToken cancellationToken)
    {
        var result = await _storageService.UploadAsync("photo-images", request.Files);


        var product = await _productReadRepository.GetByIdAsync(request.Id);

        await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.ProductImageFile
        {
            FileName = r.fileName,
            Path = r.pathOrContainerName,
            Storage = _storageService.StorageName,
            Products = new List<Domain.Entities.Product> { product }
        }).ToList());

        await _productImageFileWriteRepository.SaveAsync();
        return new UploadImageFileCommandResponse();
    }
}