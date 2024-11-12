using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoSample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MongoSample.Infrastructure.DataLayer
{
    public class DBContext : IDBContext
    {
        private readonly IMongoDatabase _db;

        public DBContext(IOptions<MongoDBSettings> options)
        {
           
            var clent = new MongoClient(options.Value.ConnectionString);
            _db = clent.GetDatabase(options.Value.Database);

            // Check if the database connection is successful
            var pingTask = _db.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
            if (!pingTask.Wait(TimeSpan.FromSeconds(5))) // Adjust timeout as needed
            {
                throw new Exception("Database connection timed out.");
            }
            else if (pingTask.Result["ok"] != 1.0)
            {
                throw new Exception("Failed to connect to the database.");
            }
        }

        //Common method for Get the Collection Based on the Type from Database
        public IMongoCollection<T> GetCollection<T>()
        {
            Type targetType = typeof(T);

            switch (targetType)
            {
                case Type userCase when userCase == typeof(Product):
                    return (IMongoCollection<T>)_db.GetCollection<Product>("Products"); //Name of the Collection

                default:
                    return _db.GetCollection<T>(targetType.Name);
                    //If we did not define any Collection name in swith for particualr type, Collection will crate in Entity Name
            }
        }

        // Common method for inserting document
        public async Task<T> InsertDocument<T>(T payload)
        {
            await GetCollection<T>().InsertOneAsync(payload);

            return payload;
        }

        //Common method for updating the document
        public async Task<T> UpdateDocument<T>(T payload)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(payload.GetType().GetProperty("Id").GetValue(payload).ToString()));

            var updateResult = await GetCollection<T>().ReplaceOneAsync(filter, payload);

            return payload;
        }

        //Common method for delete the document
        public async Task<T> DeleteDocument<T>(T payload)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(payload.GetType().GetProperty("Id").GetValue(payload).ToString()));

            var updateResult = await GetCollection<T>().DeleteOneAsync(filter);

            return payload;
        }
    }
}
