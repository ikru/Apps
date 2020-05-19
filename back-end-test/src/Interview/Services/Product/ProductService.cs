using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Interview
{
    public class ProductService : IProductService
    {
        private IProductRepository repository;
        public ProductService(IProductRepository repository) {
            this.repository = repository;
        }

        public IQueryable<JToken> All
        {
            get
            {
                return this.repository.All;
            }
        }

        public JObject GetById(int id)
        {
            return this.repository.GetById(id);
        }
    }
}
