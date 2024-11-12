using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoSample.Infrastructure.DataLayer
{
    public interface IDBContext
    {
        IMongoCollection<T> GetCollection<T>();
        public Task<T> InsertDocument<T>(T payload);
        public Task<T> UpdateDocument<T>(T payload);
        public Task<T> DeleteDocument<T>(T payload);
    }
}
