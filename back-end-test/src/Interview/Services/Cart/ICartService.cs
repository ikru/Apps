using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview
{
    public interface ICartService
    {
        IQueryable<Cart> All { get; }
        Guid Create(int[] ids);
    }
}
