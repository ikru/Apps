using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview
{
    public interface IProductService
    {
        IQueryable<JToken> All { get; }
        JObject GetById(int id);
    }
}
