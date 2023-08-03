using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Application.Features.Commands.Product.CreateProduct;
using ECommerceAPI.Application.Features.Commands.ProductImageFile.DeleteProductImage;
using ECommerceAPI.Application.Features.Commands.ProductImageFile.UploadImageFile;
using ECommerceAPI.Application.Features.Commands.UpdateProduct;
using ECommerceAPI.Application.Features.Product.Commands.DeleteProduct;
using ECommerceAPI.Application.Features.Product.Commands.UpdateProduct;
using ECommerceAPI.Application.Features.Product.Queries.GetAllProducts;
using ECommerceAPI.Application.Features.Queries.Product.GetAllProducts;
using ECommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using ECommerceAPI.Application.Repositories.File;
using ECommerceAPI.Application.Repositories.InvoiceFile;
using ECommerceAPI.Application.Repositories.Product;
using ECommerceAPI.Application.Repositories.ProductImageFile;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IProductImageFileReadRepository _productImageFileReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        readonly IStorageService _storageService;
        readonly IConfiguration configuration;
        readonly IMediator _mediator;
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IStorageService storageService, IConfiguration configuration,  IMediator mediator)
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
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsQueryRequest req)
        {
            GetAllProductsQueryResponse res = await _mediator.Send(req);
            return Ok(res);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest req)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(req);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateProductCommandRequest req)
        {
            CreateProductCommandResponse res = await _mediator.Send(req);
            return StatusCode((int)HttpStatusCode.Created);
        }

         [HttpPut]
         public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest req)
         {
             UpdateProductCommandResponse res = await _mediator.Send(req);
             return StatusCode((int)HttpStatusCode.OK);
         }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteProductCommandRequest req)
        {
            DeleteProductCommandResponse res = await _mediator.Send(req);
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFolder([FromQuery] UploadImageFileCommandRequest req)
        {
            req.Files = Request.Form.Files;
            UploadImageFileCommandResponse res = await _mediator.Send(req);
            return Ok();
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest req)
        {
            List<GetProductImagesQueryResponse> response = await _mediator.Send(req);
            return Ok(response);
        }
        
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute]DeleteProductImageCommandRequest req)
        {
            DeleteProductImageCommandResponse res = await _mediator.Send(req);
            return Ok();
        }

    }
}