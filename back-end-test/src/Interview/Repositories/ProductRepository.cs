using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Interview
{
    public class ProductRepository : IProductRepository
    {
        private ReaderWriterLockSlim fileLock = new ReaderWriterLockSlim();
        private string fileName;

        public ProductRepository(IConfiguration config)
        {
            this.fileName = config.GetValue<string>("Repository:Product:FilePath");
            
            Console.WriteLine("Data file: " + this.fileName);
        }
        public ProductRepository(string fileName)
        {
            this.fileName = fileName;
        
            Console.WriteLine("Data file: " + this.fileName);
        }

        public IQueryable<JToken> All
        {
            get
            {
                var jArray = ReadFromFile();
                return jArray.AsQueryable();
            }
        }

        public JObject GetById(int id)
        {
            var jArray = ReadFromFile();
            return jArray.FirstOrDefault(o => o.Value<int>("id") == id) as JObject;
        }

        public void Insert(JObject entity)
        {
            ModifyFile((jArray) => jArray.Add(entity));
        }

        public void Update(JObject entity)
        {
            var id = entity.Value<int>("id");
            ModifyFile((jArray) => {
                foreach (var foundEntity in jArray.Where(o => o.Value<int>("id") == id).ToList())
                    foundEntity.Replace(entity);
            });
        }
        public void Delete(int id)
        {
            ModifyFile((jArray) => {
                foreach (var foundEntity in jArray.Where(o => o.Value<int>("id") == id).ToList())
                    jArray.Remove(foundEntity);
            });
        }
        private JArray ReadFromFile()
        {
            fileLock.EnterReadLock();
            try
            {
                using (var file = File.OpenText(fileName))
                using (var reader = new JsonTextReader(file))
                {
                    return(JArray)JToken.ReadFrom(reader);
                }
            }
            finally
            {
                fileLock.ExitReadLock();
            }
        }
        private void ModifyFile(Action<JArray> modifyArray)
        {
            fileLock.EnterWriteLock();
            try
            {
                JArray jArray;
                using (var file = File.OpenText(fileName))
                using (var reader = new JsonTextReader(file))
                {
                    jArray = (JArray)JToken.ReadFrom(reader);
                }
                modifyArray(jArray);
                using (var file = File.CreateText(fileName))
                using (var writer = new JsonTextWriter(file))
                {
                    jArray.WriteTo(writer);
                }
            }
            finally
            {
                fileLock.ExitWriteLock();
            }
        }

    }
}
