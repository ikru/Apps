using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Interview.Controllers
{
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private ICartService cartService;
        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var all = this.cartService.All;
            return Ok(all);
        }

        [HttpPost]
        public IActionResult Post([FromBody] int[] ids)
        {
            this.cartService.Create(ids);
            return Ok();
        }
    }
}
