using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Interview
{
    public class CartService : ICartService
    {
        ConcurrentBag<Cart> carts = new ConcurrentBag<Cart>();
        public CartService() {
        }

        public IQueryable<Cart> All
        {
            get
            {
                return this.carts.AsQueryable<Cart>();
            }
        }

        public Guid Create(int[] ids)
        {
            var newGuid = Guid.NewGuid();
            this.carts.Add(new Cart(newGuid, ids));
            return newGuid;
        }

    }
}
