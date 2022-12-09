using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Service.IServices;
using System.Collections;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("GetProducts")]
        public IEnumerable<Product> GetProducts()
        {
            return productService.GetAll();
        }

        [HttpGet("GetProduct")]
        public Product GetProduct(int productId)
        {
            return productService.Get(productId);
        }
    }
}
