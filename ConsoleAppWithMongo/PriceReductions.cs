using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWithMongo
{
    public class PriceReductions
    {
        public ObjectId Id { get; set; }
        public int DayOfWeek { get; set; }
        public decimal Reduction { get; set; }
    }
}
