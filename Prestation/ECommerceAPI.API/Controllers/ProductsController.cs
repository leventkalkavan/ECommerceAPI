using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using ECommerceAPI.Application.Repositories.ProductRepository;
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

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_productReadRepository.GetAll(false));
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
    }
}
