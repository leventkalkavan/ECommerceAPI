using System.Net;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage;
using ECommerceAPI.Application.Features.Commands.ProductImageFile.UploadImageFile;
using ECommerceAPI.Application.Features.Commands.UpdateProduct;
using ECommerceAPI.Application.Features.Product.Commands.DeleteProduct;
using ECommerceAPI.Application.Features.Product.Queries.GetAllProducts;
using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Application.Repositories.InvoiceFile;
using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Application.Repositories.ProductImageFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Admin")]
public class ProductsController : ControllerBase
{
    private readonly IFileReadRepository _fileReadRepository;
    private readonly IFileWriteRepository _fileWriteRepository;
    private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
    private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
    private readonly ILogger<ProductsController> _logger;
    private readonly IMediator _mediator;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IStorageService _storageService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration configuration;

    public ProductsController(IProductWriteRepository productWriteRepository,
        IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment,
        IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository,
        IProductImageFileReadRepository productImageFileReadRepository,
        IProductImageFileWriteRepository productImageFileWriteRepository,
        IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository,
        IStorageService storageService, IConfiguration configuration, IMediator mediator,
        ILogger<ProductsController> logger)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _webHostEnvironment = webHostEnvironment;
        _fileWriteRepository = fileWriteRepository;
        _fileReadRepository = fileReadRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _invoiceFileReadRepository = invoiceFileReadRepository;
        _invoiceFileWriteRepository = invoiceFileWriteRepository;
        _storageService = storageService;
        this.configuration = configuration;
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllProductsQueryRequest req)
    {
        _logger.LogInformation("geldigelldigeldi");
        var res = await _mediator.Send(req);
        return Ok(res);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest req)
    {
        var response = await _mediator.Send(req);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProductCommandRequest req)
    {
        var res = await _mediator.Send(req);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest req)
    {
        var res = await _mediator.Send(req);
        return StatusCode((int)HttpStatusCode.OK);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest req)
    {
        var res = await _mediator.Send(req);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UploadFolder([FromQuery] UploadImageFileCommandRequest req)
    {
        req.Files = Request.Form.Files;
        var res = await _mediator.Send(req);
        return Ok();
    }

    [HttpGet("[action]/{Id}")]
    public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest req)
    {
        var response = await _mediator.Send(req);
        return Ok(response);
    }

    [HttpDelete("[action]/{Id}")]
    public async Task<IActionResult> DeleteProductImage([FromRoute] DeleteProductImageCommandRequest req)
    {
        var res = await _mediator.Send(req);
        return Ok();
    }
}