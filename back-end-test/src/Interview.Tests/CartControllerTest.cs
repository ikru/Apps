using Interview.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Interview.Tests
{
    public class CartControllerTest
    {
        private CartController controller = new CartController(new CartService());

        [Fact]
        public void TestGet()
        {
            var response = controller.Get() as ObjectResult;
            Assert.True(response.StatusCode == 200);

            var value = response.Value as IQueryable<Cart>;
            Assert.True(value.Count() == 0);
        }
        [Fact]
        public void TestPost()
        {
            var postResponse = controller.Post(new int[] { 1, 2, 3 }) as StatusCodeResult;
            Assert.True(postResponse.StatusCode == 200);

            var getResponse = controller.Get() as ObjectResult;
            Assert.True(getResponse.StatusCode == 200);

            var value = getResponse.Value as IQueryable<Cart>;
            foreach (var cart in value)
                Assert.True(cart.CartTotal == cart.ProductIds.Count());
            Assert.True(value.Count() == 1);
        }
        [Fact]
        public void Test1000Carts()
        {
            var tasks = new List<Task>();
            var cartsCount = 1000;
            for (int i = 0; i < cartsCount; i++)
            {
                tasks.Add(Task.Run(() => {
                    var postResponse = controller.Post(new int[] { 1, 2, 3 }) as StatusCodeResult;
                    Assert.True(postResponse.StatusCode == 200);
                }));
            }
            for (int i = 0; i < cartsCount; i++)
            {
                tasks.Add(Task.Run(() => {
                    var getResponse = controller.Get() as ObjectResult;
                    Assert.True(getResponse.StatusCode == 200);
                }));
            }
            Task.WaitAll(tasks.ToArray());

            var finalGetResponse = controller.Get() as ObjectResult;
            Assert.True(finalGetResponse.StatusCode == 200);
            var value = finalGetResponse.Value as IQueryable<Cart>;
            Assert.True(value.Count() == cartsCount);
        }
    }
}
