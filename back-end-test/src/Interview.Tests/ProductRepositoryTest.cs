using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using Xunit;

namespace Interview.Tests
{
    public class JsonRepositoryTest
    {
        private IProductRepository repository = new ProductRepository("./Data/products.json");
        [Fact]
        public void TestAll()
        {
            var all = repository.All;
            Assert.True(all.Count() == 9);
        }
        [Fact]
        public void TestFind()
        {
            var entity = repository.GetById(1);
            Assert.True(entity.Value<int>("id") == 1);
        }
        [Fact]
        public void TestInsertDelete()
        {
            dynamic obj = new JObject();
            obj.id = 10;
            obj.name = "truck";
            obj.price = "1.20";
            obj.category = "toy";
            repository.Insert((JObject)obj);
            Assert.True(repository.All.Count() == 10);

            repository.Delete(10);
            Assert.True(repository.All.Count() == 9);
        }
        [Fact]
        public void TestUpdate()
        {
            dynamic obj = new JObject();
            obj.id = 1;
            obj.name = "truck";
            obj.price = "1.20";
            obj.category = "toy";
            repository.Update((JObject)obj);
            Assert.True(repository.GetById(1).Value<string>("name") == "truck");

            obj.id = 1;
            obj.name = "banana";
            obj.price = "0.35";
            obj.category = "fruit";
            repository.Update((JObject)obj);
            Assert.True(repository.GetById(1).Value<string>("category") == "fruit");

        }
    }
}
