using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWithMongo
{
    public class MongoDBConnection
    {
        private static IMongoClient _client = new MongoClient();
       
        public static IMongoDatabase _database = _client.GetDatabase("inventory");

        public MongoDBConnection()
        {
            
        } 
    }
}
