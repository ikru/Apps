using Interview.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using Xunit;

namespace Interview.Tests
{
    public class ProductControllerTest
    {
        private ProductController controller =
            new ProductController(new ProductService(new ProductRepository("./Data/products.json")));

        [Fact]
        public void TestGet()
        {
            var response = controller.Get() as ObjectResult;
            Assert.True(response.StatusCode == 200);

            string value = response.Value.ToString();
            var jValue = JArray.Parse(value);
            Assert.True(jValue.Count == 9);
        }
        [Fact]
        public void TestGetById()
        {
            var response = controller.Get(1) as ObjectResult;
            Assert.True(response.StatusCode == 200);

            dynamic value = response.Value;
            Assert.True(value.id == 1);
            Assert.True(value.name == "banana");
        }
        [Fact]
        public void TestGetById404()
        {
            var response = controller.Get(11) as StatusCodeResult;
            Assert.True(response.StatusCode == 404);
        }
    }
}
