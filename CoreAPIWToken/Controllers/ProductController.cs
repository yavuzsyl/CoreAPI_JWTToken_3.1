using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreAPIWToken.Domain.Models;
using CoreAPIWToken.Domain.Services;
using CoreAPIWToken.Extensions;
using CoreAPIWToken.Filters;
using CoreAPIWToken.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPIWToken.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        //[HttpGet,AuthorizeFilter(ClientApps = "neighborhood",Roles = "reis")]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var productListResponse = await productService.ListAsync();
            if (productListResponse.Success)
                return Ok(productListResponse.Result);
            else
                return BadRequest(productListResponse.Message);
        }

        [HttpGet("{id:int}")]
        //[HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var productResponse = await productService.FindEntityById(id);

            if (productResponse.Success) return Ok(productResponse.Result);
            else return BadRequest(productResponse.Message);

        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductResource model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var productResponse = await productService.AddEntityAsync(mapper.Map<Product>(model));
            if (productResponse.Success) return Ok(productResponse.Result);
            else return BadRequest(productResponse.Message);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductResource model, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var oldEntity = await productService.FindEntityById(id);
            if (oldEntity == null)
                return BadRequest($"Not found any product");

            var product = mapper.Map<Product>(model);
            product.Id = id;
            var productResponse = await productService.UpdateEntityAsync(product, id);
           
            if (productResponse.Success) return Ok(productResponse.Result);
            else return BadRequest(productResponse.Message);

        }
        //[HttpDelete("{id}")]
        [HttpDelete]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var removeResponse = await productService.RemoveEntityAsync(id);
            if (removeResponse.Success) return Ok(removeResponse.Result);
            else return BadRequest(removeResponse.Message);
        }
    }
}