using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Interview.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var allProducts = this.productService.All;
            return Ok(allProducts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = this.productService.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
    }
}
