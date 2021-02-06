using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Entities
{
    public class SessionManager
    {

        private static IMongoClient _client = null;
        private static IMongoDatabase _database = null;

        public static IMongoClient GetMongoClient()
        {
            if (_client == null)
            {
                _client = new MongoClient("mongodb://localhost:27017");

                return _client;
            }


            return _client;
        }

        public static IMongoDatabase GetMongoDB()
        {
            if (_database == null)
            {
                _database = GetMongoClient().GetDatabase("cvecara");
            }

            return _database;
        }
    }
}

