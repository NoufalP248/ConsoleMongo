using MongoDB.Bson;
using MongoDB.Driver;
using System;
using EasyNetQ;
using System.Reactive.Linq;

namespace ConsoleAppWithMongo
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Rabbitmq Connection & bus Setting
            string rabbitmqConnectionString = "host=localhost;username=guest;password=guest;timeout=60";
            var bus = RabbitHutch.CreateBus(rabbitmqConnectionString);
            //Rabbitmq Connection & bus Setting


            //Factory Method
            DateTime now = DateTime.Now;
            Int32 day = Convert.ToInt32(now.DayOfWeek);
            var discount = FactoryDiscountCreator.GetDiscount(day); //Factory
            //Factory Method



            //Gets all data
            var _db = MongoDBConnection._database;
            var _collection = _db.GetCollection<Product>("products");
            var allData = _collection.Find(new BsonDocument()).ToList();
            //Gets all data



            //Publish all data to rabbitmq
            bus.PubSub.Publish(allData);
           


            //Subscribe message from rabbitmq
            var subscribeMessage = bus.PubSub.SubscribeAsync<Product>(
                                       "my_subscription_id", msg => Console.WriteLine(msg.Name));
            //Subscribe message from rabbitmq



            //Gets filtered data
            var filter = Builders<Product>.Filter.Eq("Name", "rice");
            //var filter = Builders<Product>.Filter.Eq("Name", subscribeMessage);
            var filterData = _collection.Find(filter).FirstOrDefault();
            //Gets filtered data



            //Data assign & disount calculation
            Product ObjProduct = new Product();
            ObjProduct.Id = filterData.Id;
            ObjProduct.Name = filterData.Name;
            ObjProduct.EntryDate = filterData.EntryDate;
            ObjProduct.Price = filterData.Price;
            ObjProduct.discountPrice = ObjProduct.Price - (Convert.ToDecimal(discount.discount));
            //Data assign & disount calculation

            //Publish message to rabbitmq
            bus.PubSub.Publish(ObjProduct);
        }
    }
}
