using Newtonsoft.Json.Linq;
using System.Linq;

namespace Interview
{
    public interface IProductRepository
    {
        IQueryable<JToken> All { get; }
        JObject GetById(int id);
        void Insert(JObject entity);
        void Update(JObject entity);
        void Delete(int id);
    }
}