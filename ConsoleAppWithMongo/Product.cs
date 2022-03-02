using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWithMongo
{
    public class Product
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal Price { get; set; }
        public decimal discountPrice { get; set; }

    }
}
