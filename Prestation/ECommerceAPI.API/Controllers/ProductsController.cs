using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.ProductRepository;
using ECommerceAPI.Application.RequestParameters;
using ECommerceAPI.Application.ViewModels.Products;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
           var res = await _productReadRepository.GetByIdAsync(id,false);
            return Ok(res);
        }
            
        [HttpPost]
        public async Task<IActionResult> Add(VM_CreateProduct product)
        {
            if (ModelState.IsValid)
            {
                await _productWriteRepository.AddAsync(new()
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Stock = product.Stock
                    }
                );
                await _productWriteRepository.SaveAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Update(VM_UpdateProduct model)
        {
            var product = await _productReadRepository.GetByIdAsync(model.Id, true);

            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;
            _productWriteRepository.Update(product);
            await _productWriteRepository.SaveAsync();

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
           await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            //wwwroot/resource/product-images
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            Random r = new();
            foreach (IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath, $"{r.Next()}{Path.GetExtension(file.FileName)}");

                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            return Ok();
        }
    }
}
