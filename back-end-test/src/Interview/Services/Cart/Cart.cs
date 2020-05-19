using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview
{
    public class Cart
    {
        public Cart(Guid id, int[] productIds) {
            this.Id = id;
            this.ProductIds = productIds ?? new int[] { };
        }
        public Guid Id { get; private set; }
        public int[] ProductIds { get; private set; }
        public int CartTotal { get { return this.ProductIds.Count(); } }

    }
}
