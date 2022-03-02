using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWithMongo
{
    public class FactoryDiscountCreator
    {
        public static Discount GetDiscount(int day)
        {
            if (day <= 5)
                return new MondayToFriday();
            else if (day > 5 && day <= 6)
                return new Saturday();
            else
                return new Sunday();
        }
    }

    public abstract class Discount
    {
        internal decimal discount;
        internal int DayOfWeek;
        internal decimal returnDiscount;
        public decimal Getdiscount()
        {
            return discount;
        }
        

    }
    public class MondayToFriday : Discount
    {
        public MondayToFriday()
        {
            var _db = MongoDBConnection._database;
            var _collection = _db.GetCollection<PriceReductions>("PriceReductions");
            var documents = _collection.Find(new BsonDocument()).ToList();
            foreach (var data in documents)
            {
                DayOfWeek = data.DayOfWeek;
                if (DayOfWeek <= 5)
                {
                    discount = data.Reduction;
                }
            }
            returnDiscount = discount;
        }
    }
    public class Saturday : Discount
    { 
        public Saturday()
        {
            var _db = MongoDBConnection._database;
            var _collection = _db.GetCollection<PriceReductions>("PriceReductions");
            var documents = _collection.Find(new BsonDocument()).ToList();
            foreach(var data in documents)
            {
                DayOfWeek = data.DayOfWeek;
                if (DayOfWeek > 5 && DayOfWeek <= 6)
                {
                    discount = data.Reduction;
                }
            }
            returnDiscount = discount;
        }
    }
    public class Sunday : Discount
    {
        public Sunday()
        {
            var _db = MongoDBConnection._database;
            var _collection = _db.GetCollection<PriceReductions>("PriceReductions");
            var documents = _collection.Find(new BsonDocument()).ToList();
            foreach (var data in documents)
            {
                DayOfWeek = data.DayOfWeek;
                if (DayOfWeek == 7)
                {
                    discount = data.Reduction;
                }
            }
            returnDiscount = discount;
        }
    }
}
